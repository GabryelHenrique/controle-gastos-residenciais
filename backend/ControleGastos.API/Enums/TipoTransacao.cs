//Nessa primeira parte organizei e defini onde está o código e foi definido para que o acesso seja publico, e possa ser usado durante o cadastro.
//Representa os tipos de transações permitidas.
//Utilizado enum para impedir valores inválidos.


namespace ControleGastos.API.Enums;

public enum TipoTransacao

{
    Receita = 1,
    Despesa = 2
}
