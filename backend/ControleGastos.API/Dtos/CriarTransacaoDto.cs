using ControleGastos.API.Enums;

namespace ControleGastos.API.Dtos;
public class CriarTransacaoDto
{
    // Descrição da transação registrada pelo usuário.
    public string Descricao { get; set; } = string.Empty;

    // Valor em decimal por ser dinheiro cadastrado pelo usuário.
    public decimal Valor { get; set; }

    // Define o tipo de transação cadastrada.
    public TipoTransacao Tipo { get; set; }

    // Define qual pessoa Id está cadastrando a transação.
    public int PessoaId { get; set; }
}
