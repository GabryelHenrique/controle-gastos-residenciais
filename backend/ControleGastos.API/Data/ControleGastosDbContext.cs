using ControleGastos.API.Data;
using ControleGastos.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.API.Data;

public class ControleGastosDbContext : DbContext
{
    public ControleGastosDbContext(DbContextOptions<ControleGastosDbContext> options)
        : base(options)
    {
        
    }
    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Transacao> Transacoes { get; set; }

}
