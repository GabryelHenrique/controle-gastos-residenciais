using ControleGastos.API.Dtos;
using ControleGastos.API.Models;

namespace ControleGastos.API.Dtos;

public class ResumoTotaisDto
{
    // Lista com os totais calculados para cada pessoa.
    public List<TotalPessoaDto> Pessoas { get; set; } = new();

    // Soma geral das receitas de todas as pessoas.
    public decimal TotalGeralReceitas { get; set; }

    // Soma geral das despesas de todas as pessoas.
    public decimal TotalGeralDespesas { get; set; }

    // Saldo líquido geral: receitas totais menos despesas totais.
    public decimal SaldoLiquidoGeral { get; set; }

}