namespace ControleGastos.API.Dtos;
public class CriarPessoaDto
{
    // Nome informado pelo usuário no cadastro.
    public string Nome { get; set; } = string.Empty;

    // Data de nascimento usada para calcular a idade.
    public DateOnly DataNascimento { get; set; }
}
