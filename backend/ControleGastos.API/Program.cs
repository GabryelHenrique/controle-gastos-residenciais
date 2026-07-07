using ControleGastos.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Registra o DbContext para permitir o acesso ao banco SQLite.
builder.Services.AddDbContext<ControleGastosDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Libera o acesso do front-end React à API durante o desenvolvimento.
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

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

// Aplica a política de CORS para permitir requisições do front-end.
app.UseCors("PermitirFrontend");

// Mapeia as rotas definidas nos Controllers.
app.MapControllers();

app.Run();
