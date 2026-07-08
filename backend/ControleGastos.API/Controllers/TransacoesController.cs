using ControleGastos.API.Data;
using ControleGastos.API.Dtos;
using ControleGastos.API.Enums;
using ControleGastos.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<ActionResult<List<TransacaoRespostaDto>>> ListarTransacoes()
    {
        var transacoes = await _context.Transacoes
            .Select(transacao => new TransacaoRespostaDto
            {
                Id = transacao.Id,
                Descricao = transacao.Descricao,
                Valor = transacao.Valor,
                Tipo = transacao.Tipo,
                PessoaId = transacao.PessoaId
            })
            .ToListAsync();

        return Ok(transacoes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TransacaoRespostaDto>> BuscarTransacaoPorId(int id)
    {
        // Buscar uma transação especifica para ver somente dados que são necessários.
        var transacao = await _context.Transacoes
            .Where(transacao => transacao.Id == id)
            .Select(transacao => new TransacaoRespostaDto
            {
                Id = transacao.Id,
                Descricao = transacao.Descricao,
                Valor = transacao.Valor,
                Tipo = transacao.Tipo,
                PessoaId = transacao.PessoaId
            })
            .FirstOrDefaultAsync();

        if (transacao == null)
        {
            return NotFound("Transação não encontrada.");
        }

        return Ok(transacao);
    }

    
    // Validar dados obrigatórios antes de criar transações.
    [HttpPost]
    public async Task<ActionResult<TransacaoRespostaDto>> CadastrarTransacao(CriarTransacaoDto transacaoDto)
    {
        if (string.IsNullOrWhiteSpace(transacaoDto.Descricao))
        {
            return BadRequest("A descrição da transação é obrigatória.");
        }

        if (transacaoDto.Valor <= 0)
        {
            return BadRequest("O valor da transação deve ser maior que zero.");
        }

        if (!Enum.IsDefined(typeof(TipoTransacao), transacaoDto.Tipo))
        {
            return BadRequest("O tipo da transação deve ser Receita - '1' ou Despesa - '2'.");
        }

        var pessoa = await _context.Pessoas.FindAsync(transacaoDto.PessoaId);

        if (pessoa == null)
        {
            return BadRequest("A pessoa informada não existe.");
        }

        // Ponto importante da regra de negócio, aqui vamos validar que menores de idade só podem cadastrar despesas.
        if (pessoa.Idade < 18 && transacaoDto.Tipo == TipoTransacao.Receita)
        {
            return BadRequest("Pessoas menores de idade podem cadastrar somente despesas.");
        }

        
        // Ponto importante que após testar e validar, entrava em looping, então tive que implementar essa parte para
        // evitar ciclo entre pessoa e transacoes. Criado o DTO de resposta.
        var transacao = new Transacao
        {
            Descricao = transacaoDto.Descricao.Trim(),
            Valor = transacaoDto.Valor,
            Tipo = transacaoDto.Tipo,
            PessoaId = transacaoDto.PessoaId
        };

        _context.Transacoes.Add(transacao);
        await _context.SaveChangesAsync();

        var transacaoResposta = new TransacaoRespostaDto
        {
            Id = transacao.Id,
            Descricao = transacao.Descricao,
            Valor = transacao.Valor,
            Tipo = transacao.Tipo,
            PessoaId = transacao.PessoaId
        };

        return CreatedAtAction(nameof(BuscarTransacaoPorId), new { id = transacao.Id }, transacaoResposta);
    }
}