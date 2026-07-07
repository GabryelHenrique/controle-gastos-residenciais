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
    var pessoas = await _context.Pessoas
        .Include(p => p.Transacoes)
        .ToListAsync();

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