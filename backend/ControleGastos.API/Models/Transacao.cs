using ControleGastos.API.Enums;

namespace ControleGastos.API.Models;

public class Transacao
{
    // Identificador único da transação.
    public int Id { get; set; }

    // Descrição na qual o usuário vai cadastrar a receita ou despesa e informar a origem.
    public string Descricao { get; set; } = string.Empty;

    // Valor financeiro referente à transação que usuário estará cadastrando.
    public decimal Valor { get; set; }

    // Tipo da transação, limitado a valores predefinidos em Enums: Receita ou Despesa.
    public TipoTransacao Tipo { get; set; }

    // Identificador da pessoa à qual esta transação pertence.
    public int PessoaId { get; set; }

    // Pessoa que vai ser relacionada a transação, apresenta vínculo entre as tabelas.
    public Pessoa? Pessoa { get; set;}

}