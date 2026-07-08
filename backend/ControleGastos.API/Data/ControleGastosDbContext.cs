using ControleGastos.API.Data;
using ControleGastos.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.API.Data;


// Ponte feita entre a linguagem C# com o banco de dados utilizado.
public class ControleGastosDbContext : DbContext
{
    public ControleGastosDbContext(DbContextOptions<ControleGastosDbContext> options)
        : base(options)
    {
        
    }
    // Faz a representação da tabela de pessoas.
    public DbSet<Pessoa> Pessoas { get; set; }

    // Faz representação da tabela de transações.
    public DbSet<Transacao> Transacoes { get; set; }

}
