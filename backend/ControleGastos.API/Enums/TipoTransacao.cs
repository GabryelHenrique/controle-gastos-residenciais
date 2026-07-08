// Representa os tipos de transações permitidas.
// Utilizado enum para impedir valores inválidos, como se alguem tentasse cadastrar uma transação do tipo 3, isso retornaria um erro.


namespace ControleGastos.API.Enums;

public enum TipoTransacao

{
    Receita = 1,
    Despesa = 2
}
