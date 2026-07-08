namespace ControleGastos.API.Dtos;


// Requisitos bases para geração de um cadastro de uma pessoa.
public class CriarPessoaDto
{
    public string Nome { get; set; } = string.Empty;

    public DateOnly DataNascimento { get; set; }
}
