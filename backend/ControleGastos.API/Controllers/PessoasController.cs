using ControleGastos.API.Data;
using Microsoft.AspNetCore.Mvc;
using ControleGastos.API.Models;
using Microsoft.EntityFrameworkCore;
using ControleGastos.API.Dtos;

namespace ControleGastos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PessoasController : ControllerBase
{
    private readonly ControleGastosDbContext _context;

    public PessoasController(ControleGastosDbContext context)
    {
        _context = context;
    }

[HttpGet]
public async Task<ActionResult<List<Pessoa>>> ListarPessoas()
    {
        var pessoas = await _context.Pessoas.ToListAsync();
        
        return Ok(pessoas);
    }

[HttpPost]
public async Task<ActionResult<Pessoa>> CadastrarPessoa(CriarPessoaDto pessoaDto)
{
    if (string.IsNullOrWhiteSpace(pessoaDto.Nome))
    {
        return BadRequest("O nome da pessoa é obrigatório.");
    }

    if (pessoaDto.DataNascimento > DateOnly.FromDateTime(DateTime.Today))
    {
        return BadRequest("A data de nascimento não pode ser uma data futura.");
    }

    var pessoa = new Pessoa
    {
        Nome = pessoaDto.Nome,
        DataNascimento = pessoaDto.DataNascimento
    };

    _context.Pessoas.Add(pessoa);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(BuscarPessoaPorId), new { id = pessoa.Id }, pessoa);
}

[HttpDelete("{id}")]
public async Task<ActionResult> ExcluirPessoa(int id)
{
    var pessoa = await _context.Pessoas.FindAsync(id);

    if (pessoa == null)
    {
        return NotFound("Pessoa não encontrada.");
    }

    _context.Pessoas.Remove(pessoa);
    await _context.SaveChangesAsync();

    return NoContent();
}

[HttpGet("{id}")]
public async Task<ActionResult<Pessoa>> BuscarPessoaPorId(int id)
{
    var pessoa = await _context.Pessoas.FindAsync(id);

    if (pessoa == null)
    {
        return NotFound("Pessoa não encontrada.");
    }

    return Ok(pessoa);
}



}