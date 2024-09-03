using minimal_api.Dominio.Entitidades;
using minimal_api.Dominio.DTOs;
using minimal_api.Dominio.Interfaces;
using minimal_api.Infraestrutura.Db;

namespace minimal_api.Dominio.Servicos
{
    public class AdministradorServicos : IAdministradorServicos
    {
        private readonly DbContexto _contexto;

        public AdministradorServicos(DbContexto contexto)
        {
            _contexto = contexto;
        }

        public Administrador? BuscaPorId(int Id)
        {
            return _contexto.Administradores.FirstOrDefault(v => v.Id == Id);
        }

        public Administrador Incluir(Administrador administrador)
        {
            _contexto.Administradores.Add(administrador);
            _contexto.SaveChanges();
            return administrador;
        }

        public List<Administrador> Todos(int? pagina)
    {
        int ItensPorPagina = 10;
        return _contexto.Administradores
                        .Skip((pagina.GetValueOrDefault(1) - 1) * ItensPorPagina)
                        .Take(ItensPorPagina)
                        .ToList();
    }

        public Administrador? Login(LoginDTO loginDTO)
        {
            return _contexto.Administradores
                .FirstOrDefault(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha);
        }
    }
}
