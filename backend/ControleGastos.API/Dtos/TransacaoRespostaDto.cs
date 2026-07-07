using ControleGastos.API.Enums;

namespace ControleGastos.API.Dtos;

public class TransacaoRespostaDto
{
    // Identificador único da transação.
    public int Id { get; set; }

    // Descrição da transação cadastrada.
    public string Descricao { get; set; } = string.Empty;

    // Valor financeiro da transação.
    public decimal Valor { get; set; }

    // Tipo da transação: Receita ou Despesa.
    public TipoTransacao Tipo { get; set; }

    // Identificador da pessoa relacionada à transação.
    public int PessoaId { get; set; }
}