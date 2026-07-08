using ControleGastos.API.Data;
using ControleGastos.API.Dtos;
using ControleGastos.API.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TotaisController : ControllerBase
{
    private readonly ControleGastosDbContext _context;

    public TotaisController(ControleGastosDbContext context)
    {
        _context = context;
    }

[HttpGet]
public async Task<ActionResult<ResumoTotaisDto>> ConsultarTotais()
{
    // Carregar pessoas junto com suas transações permitindo calcular totais.
    var pessoas = await _context.Pessoas
        .Include(p => p.Transacoes)
        .ToListAsync();

    // Calcular receitas, despesas e saldo após calculos de cada pessoa.
    var totaisPorPessoa = pessoas.Select(pessoa =>
    {
        var totalReceitas = pessoa.Transacoes
            .Where(t => t.Tipo == TipoTransacao.Receita)
            .Sum(t => t.Valor);

        var totalDespesas = pessoa.Transacoes
            .Where(t => t.Tipo == TipoTransacao.Despesa)
            .Sum(t => t.Valor);

        return new TotalPessoaDto
        {
            PessoaId = pessoa.Id,
            Nome = pessoa.Nome,
            Idade = pessoa.Idade,
            TotalReceitas = totalReceitas,
            TotalDespesas = totalDespesas,
            Saldo = totalReceitas - totalDespesas
        };
    }).ToList();

    
    // Aqui entra um pensamento importante onde soma totais individuais para gerar o resumo da aplicação e pessoas com transações cadastradas.
    var totalGeralReceitas = totaisPorPessoa.Sum(p => p.TotalReceitas);
    var totalGeralDespesas = totaisPorPessoa.Sum(p => p.TotalDespesas);

    var resumo = new ResumoTotaisDto
    {
        Pessoas = totaisPorPessoa,
        TotalGeralReceitas = totalGeralReceitas,
        TotalGeralDespesas = totalGeralDespesas,
        SaldoLiquidoGeral = totalGeralReceitas - totalGeralDespesas
    };

    return Ok(resumo);
}
}