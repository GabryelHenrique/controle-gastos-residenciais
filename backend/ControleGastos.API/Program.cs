using ControleGastos.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Registra o DbContext para permitir o acesso ao banco SQLite.
builder.Services.AddDbContext<ControleGastosDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registra o uso de Controllers na API.
builder.Services.AddControllers();

// Registra a documentação OpenAPI.
builder.Services.AddOpenApi();

var app = builder.Build();

// Habilita a documentação da API em ambiente de desenvolvimento.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Mapeia as rotas definidas nos Controllers.
app.MapControllers();

app.Run();
