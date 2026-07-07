using ControleGastos.API.Dtos;
using ControleGastos.API.Models;

namespace ControleGastos.API.Dtos;

public class TotalPessoaDto
{
    // Identificador da pessoa.
    public int PessoaId { get; set; }

    // Nome da pessoa.
    public string Nome { get; set; } = string.Empty;

    // Idade calculada da pessoa.
    public int Idade { get; set; }

    // Soma de todas as receitas da pessoa.
    public decimal TotalReceitas { get; set; }

    // Soma de todas as despesas da pessoa.
    public decimal TotalDespesas { get; set; }

    // Saldo da pessoa: receitas menos despesas.
    public decimal Saldo { get; set; }
}