# Controle de Gastos Residenciais - Back-end

Este projeto é o back-end de uma aplicação para controle de gastos residenciais.  
A API permite cadastrar pessoas, registrar receitas e despesas, consultar totais financeiros e aplicar regras de negócio relacionadas à idade da pessoa cadastrada.

O projeto foi desenvolvido em **C# com ASP.NET Core Web API**, utilizando **Entity Framework Core** e **SQLite** para persistência dos dados.

---

## Objetivo do projeto

O objetivo da aplicação é permitir o controle financeiro de pessoas de uma residência, possibilitando:

- Cadastro de pessoas.
- Exclusão de pessoas.
- Cadastro de transações financeiras.
- Listagem de pessoas e transações.
- Consulta de totais por pessoa.
- Consulta de totais gerais.
- Aplicação de regras de negócio para menores de idade.
- Persistência dos dados em banco SQLite.

---

## Tecnologias utilizadas

- C#
- ASP.NET Core Web API
- .NET 10
- Entity Framework Core
- SQLite
- REST Client para testes HTTP
- Git e GitHub

---

## Estrutura do back-end

```text
backend/
└── ControleGastos.API/
    ├── Controllers/
    │   ├── PessoasController.cs
    │   ├── TransacoesController.cs
    │   └── TotaisController.cs
    │
    ├── Data/
    │   ├── ControleGastosDbContext.cs
    │   └── Migrations/
    │
    ├── Dtos/
    │   ├── CriarPessoaDto.cs
    │   ├── CriarTransacaoDto.cs
    │   ├── TransacaoRespostaDto.cs
    │   ├── TotalPessoaDto.cs
    │   └── ResumoTotaisDto.cs
    │
    ├── Enums/
    │   └── TipoTransacao.cs
    │
    ├── Models/
    │   ├── Pessoa.cs
    │   └── Transacao.cs
    │
    ├── Program.cs
    ├── appsettings.json
    └── ControleGastos.API.csproj

Modelagem principal
Pessoa

A entidade Pessoa representa uma pessoa cadastrada no sistema.

Principais campos:

Id
Nome
DataNascimento
Idade
Transacoes

A idade não é armazenada diretamente no banco de dados.
Ela é calculada automaticamente a partir da data de nascimento.

Essa decisão evita que a idade fique desatualizada com o passar do tempo.

Transação

A entidade Transacao representa uma movimentação financeira vinculada a uma pessoa.

Principais campos:

Id
Descricao
Valor
Tipo
PessoaId
Pessoa

Cada transação pertence a uma pessoa cadastrada.

TipoTransacao

O tipo da transação foi definido com um enum:

public enum TipoTransacao
{
    Receita = 1,
    Despesa = 2
}

Essa escolha evita o uso de textos livres e limita os tipos possíveis de transação.

Regras de negócio implementadas
Pessoas
O nome da pessoa é obrigatório.
A data de nascimento não pode ser uma data futura.
A idade é calculada automaticamente.
Ao excluir uma pessoa, todas as transações vinculadas a ela também são excluídas.
Transações
A descrição da transação é obrigatória.
O valor da transação deve ser maior que zero.
O tipo da transação deve ser Receita ou Despesa.
A pessoa informada na transação precisa existir.
Pessoas menores de 18 anos podem cadastrar apenas despesas.
Pessoas maiores de idade podem cadastrar receitas e despesas.
Totais

A API calcula:

Total de receitas por pessoa.
Total de despesas por pessoa.
Saldo por pessoa.
Total geral de receitas.
Total geral de despesas.
Saldo líquido geral.
Banco de dados

O projeto utiliza SQLite como banco de dados local.

A configuração da conexão está no arquivo appsettings.json.

O banco é criado a partir das migrations do Entity Framework Core.   

Decisões técnicas
Uso de DTOs

Foram utilizados DTOs para separar os dados recebidos e retornados pela API.

DTOs de entrada:

CriarPessoaDto
CriarTransacaoDto

DTOs de saída:

TransacaoRespostaDto
TotalPessoaDto
ResumoTotaisDto

Essa separação evita expor diretamente toda a estrutura dos Models e deixa as respostas da API mais organizadas.

Idade calculada

A idade da pessoa é calculada automaticamente a partir da data de nascimento.

Essa abordagem evita armazenar uma idade fixa no banco de dados, já que a idade muda com o tempo.

Uso de decimal para valores financeiros

O tipo decimal foi utilizado para representar valores financeiros, pois é mais adequado para operações com dinheiro.

Uso de enum para tipo da transação

O tipo da transação foi definido como enum para restringir os valores aceitos pela aplicação.

Isso evita que sejam cadastrados tipos inválidos como textos livres.

Exclusão em cascata

O relacionamento entre Pessoa e Transação foi configurado para permitir que, ao excluir uma pessoa, suas transações relacionadas também sejam removidas.

Isso mantém a consistência dos dados.

Correção de ciclo JSON

Durante o desenvolvimento, foi identificado um problema de ciclo ao retornar entidades relacionadas diretamente.

Para resolver isso, foi criado o DTO TransacaoRespostaDto, retornando apenas os dados necessários da transação.

Testes realizados

Foram realizados testes manuais utilizando arquivo .http no VS Code.

Cenários testados:

Cadastro de pessoa maior de idade.
Cadastro de pessoa menor de idade.
Listagem de pessoas.
Busca de pessoa por Id.
Exclusão de pessoa.
Cadastro de receita para maior de idade.
Cadastro de despesa para maior de idade.
Bloqueio de receita para menor de idade.
Cadastro de despesa para menor de idade.
Validação de pessoa inexistente.
Validação de descrição vazia.
Validação de valor inválido.
Validação de tipo inválido.
Consulta de totais por pessoa.
Consulta de totais gerais.
Exclusão em cascata de transações.