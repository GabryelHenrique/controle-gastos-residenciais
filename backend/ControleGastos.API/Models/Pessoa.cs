namespace ControleGastos.API.Models;

public class Pessoa
{
    // Aqui vamos informar ID que vai estar relacionado a pessoa - gerado pelo banco de dados com ID único.
    public int Id { get; set; }

    // Nome informado pelo usuário. Iniciado como string vazia para evitar valor nulo.
    public string Nome { get; set; } = string.Empty;

    // Referente a data de nascimento, faço uma determinação para aceitar apenas data, pois não há necessidade da hora em data de nascimento.
    public DateOnly DataNascimento { get; set; }

// Idade sendo calculada automaticamente a partir da DataNascimento.
// Não armazena no banco, assim evita que fique desatualizada com o tempo.
public int Idade
{   get
        {
            var hoje = DateOnly.FromDateTime(DateTime.Today);

            var idade = hoje.Year - DataNascimento.Year;

            if (DataNascimento > hoje.AddYears(-idade))
            {
                idade--;
            }
            return idade;
        

        }
    }
      
        // Listar transações relacionadas a pessoa, e vale ressaltar que pela lógica, uma pessoa pode ter mais de uma transação vinculada.
        public List<Transacao> Transacoes { get; set; } = new();
}

