using minimal_api.Dominio.Entitidades;
using minimal_api.Dominio.Interfaces;
using minimal_api.Infraestrutura.Db;

namespace minimal_api.Dominio.Servicos
{
    public class VeiculoServicos : IVeiculoServicos
    {
        private readonly DbContexto _contexto;

        public VeiculoServicos(DbContexto contexto)
        {
            _contexto = contexto;
        }

        public void Apagar(Veiculo veiculo)
        {
            _contexto.Veiculos.Remove(veiculo);
            _contexto.SaveChanges();
        }

        public void Atualizar(Veiculo veiculo)
        {
            _contexto.Veiculos.Update(veiculo);
            _contexto.SaveChanges();
        }

        public Veiculo? BuscaPorId(int Id)
        {
            return _contexto.Veiculos.FirstOrDefault(v => v.Id == Id);
        }

        public void Incluir(Veiculo veiculo)
        {
            _contexto.Veiculos.Add(veiculo);
            _contexto.SaveChanges();
        }

        public List<Veiculo>Todos(int? id, string? marca, string? modelo)

        {
            var query = _contexto.Veiculos.AsQueryable();

            if (!string.IsNullOrEmpty(Nome))
            {
                query = query.Where(v => EF.Functions.Like(v.Nome.ToLower(), $"%{Nome.ToLower()}%"));
            }

            if (!string.IsNullOrEmpty(marca))
            {
                query = query.Where(v => EF.Functions.Like(v.Marca.ToLower(), $"%{marca.ToLower()}%"));
            }

            int ItensPorPagina = 10;
            return query.Skip((pagina - 1) * ItensPorPagina)
                        .Take(ItensPorPagina)
                        .ToList();
        }
    }
}
