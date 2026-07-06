namespace ControleGastos.API.Models;

public class Pessoa
{
// Aqui vamos informar ID que vai estar relacionado a pessoa - gerado pelo banco de dados com ID único.
    public int Id { get; set; }

// Nome precisa iniciar como vazio, e não nulo. Aqui determinamos por meio da 'string.Empty'. 
    public string Nome { get; set; } = string.Empty;

// Referente a data de nascimento, faço uma determinação para aceitar apenas data, pois não há necessidade da hora em data de nascimento.
    public DateOnly DataNascimento { get; set; }

// Optei por usar uma calculadora de idade, 
// assim conseguimos manter o sistema atualizado imprimindo a idade correta, não ficando desatualizado e sendo necessário mudar manualmente.
 
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

}

