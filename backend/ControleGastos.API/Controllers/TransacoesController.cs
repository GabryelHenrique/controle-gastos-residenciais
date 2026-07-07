using ControleGastos.API.Data;
using Microsoft.AspNetCore.Mvc;
using ControleGastos.API.Models;
using Microsoft.EntityFrameworkCore;
using ControleGastos.API.Dtos;
using ControleGastos.API.Enums;



namespace ControleGastos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransacoesController : ControllerBase
{
    private readonly ControleGastosDbContext _context;

    public TransacoesController(ControleGastosDbContext context)
    {
        _context = context;
    
    }

[HttpGet]
public async Task<ActionResult<List<Transacao>>> ListarTransacoes()
{
    var transacoes = await _context.Transacoes.ToListAsync();

    return Ok(transacoes);
}

[HttpGet("{id}")]
public async Task<ActionResult<Transacao>> BuscarTransacaoPorId(int id)
    {
        var transacao = await _context.Transacoes.FindAsync(id);

        if (transacao == null)
        {
            return NotFound("Transação não encontrada.");
        }
        return Ok(transacao);
    }

[HttpPost]
public async Task<ActionResult<Transacao>> CadastrarTransacao(CriarTransacaoDto transacaoDto)
{
    if (string.IsNullOrWhiteSpace(transacaoDto.Descricao))
    {
        return BadRequest("A descrição da transação é obrigatória.");
    
    }
    
    if (transacaoDto.Valor <= 0)
    {
        return BadRequest("O valor da transação deve ser maior que zero.");   
    }

    var pessoa = await _context.Pessoas.FindAsync(transacaoDto.PessoaId);

    if (pessoa == null)
    {
        return BadRequest("A pessoa informada não existe.");   
    } 

    if (pessoa.Idade < 18 && transacaoDto.Tipo == TipoTransacao.Receita)
    {
        return BadRequest("Pessoas menores de idade podem cadastrar somente despesas."); 
    }  

    var transacao = new Transacao
    {
        Descricao = transacaoDto.Descricao,
        Valor = transacaoDto.Valor,
        Tipo = transacaoDto.Tipo,
        PessoaId = transacaoDto.PessoaId
    };

    _context.Transacoes.Add(transacao);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(BuscarTransacaoPorId), new { id = transacao.Id }, transacao);
}

}
