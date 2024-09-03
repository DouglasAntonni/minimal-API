using minimal_api.Dominio.Entitidades;
using minimal_api.Dominio.DTOs;

namespace minimal_api.Dominio.Interfaces
{
    public interface IAdministradorServicos
    {
        Administrador? Login(LoginDTO loginDTO);
        Administrador Incluir(Administrador administrador);
        Administrador? BuscaPorId(int Id);
        List<Administrador> Todos (int? pagina);
        
    }
}
