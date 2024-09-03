using Microsoft.EntityFrameworkCore;
using minimal_api.Infraestrutura.Db;
using minimal_api.Dominio.DTOs;
using minimal_api.Dominio.Interfaces;
using minimal_api.Dominio.Servicos;
using Microsoft.AspNetCore.Mvc;

#region builder
var builder = WebApplication.CreateBuilder(args);

// Registra os serviços na injeção de dependências
builder.Services.AddScoped<IAdministradorServicos, AdministradorServicos>();
builder.Services.AddScoped<IVeiculoServicos, VeiculoServicos>(); // Certifique-se de que o nome da interface e da classe estejam corretos.

// Adiciona serviços ao contêiner
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do contexto de banco de dados
builder.Services.AddDbContext<DbContexto>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao"));
});

var app = builder.Build();
#endregion

#region App
// Configuração do pipeline de requisição HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();
#endregion

#region Home
app.MapGet("/", () => Results.Json(new { Message = "Bem-vindo à API!" }).WithTags("Home"));
#endregion

#region Administradores
app.MapPost("/administradores/login", ([FromBody] LoginDTO loginDTO, IAdministradorServicos administradorServicos) =>
{
    var resultadoLogin = administradorServicos.Login(loginDTO);
    
    if (resultadoLogin == null)
        return Results.BadRequest("Login ou senha inválidos.");

    return Results.Ok(resultadoLogin);
})
.WithTags("Administradores");

app.MapGet("/administradores", ([FromQuery] int? pagina, IAdministradorServicos administradorServicos) =>
{
    var administradores = administradorServicos.Todos(pagina);
    return Results.Ok(administradores);
})
.WithTags("Administradores");
#endregion

#region Veiculos
app.MapGet("/veiculos", ([FromQuery] string nome, IVeiculoServicos veiculoServicos) =>
{
    var veiculos = veiculoServicos.BuscarPorNome(nome);
    return veiculos != null ? Results.Ok(veiculos) : Results.NotFound();
})
.WithTags("Veiculos");

app.MapGet("/veiculos/{id}", ([FromRoute] int id, IVeiculoServicos veiculoServicos) =>
{
    var veiculo = veiculoServicos.BuscarPorId(id);
    return veiculo != null ? Results.Ok(veiculo) : Results.NotFound();
})
.WithTags("Veiculos");

app.MapPost("/veiculos", ([FromBody] VeiculoDTO veiculoDTO, IVeiculoServicos veiculoServicos) =>
{
    veiculoServicos.Adicionar(veiculoDTO);
    return Results.Created($"/veiculos/{veiculoDTO.Id}", veiculoDTO);
})
.WithTags("Veiculos");
#endregion
