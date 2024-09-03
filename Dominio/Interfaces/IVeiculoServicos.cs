using minimal_api.Dominio.Entitidades;

namespace minimal_api.Dominio.Interfaces
{
    public interface IVeiculoServicos
    {
        List<Veiculo> Todos(int? pagina = 1, string? nome = null, string? marca = null);
        Veiculo? BuscaPorId(int Id);

        void Incluir(Veiculo veiculo);
        void Atualizar(Veiculo veiculo);
        void Apagar(Veiculo veiculo);
    }
}
