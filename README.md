# Controle de Gastos Residenciais

Projeto desenvolvido como desafio técnico para uma vaga de estágio.

A proposta é construir um sistema de controle de gastos residenciais, permitindo o cadastro de pessoas, cadastro de transações financeiras e consulta de totais por pessoa.

## Objetivo do projeto

Implementar uma aplicação com back-end em ASP.NET Core Web API e front-end em React com TypeScript.

O sistema deverá permitir:

- Cadastrar pessoas;
- Listar pessoas cadastradas;
- Excluir pessoas;
- Cadastrar transações financeiras;
- Listar transações;
- Calcular receitas, despesas e saldo por pessoa;
- Exibir o total geral de receitas, despesas e saldo líquido.

## Status do projeto

🚧 Em desenvolvimento.

Até o momento foi iniciada a estrutura do back-end, com a criação das primeiras entidades e organização inicial do projeto.

## Tecnologias utilizadas

### Back-end

- C#
- ASP.NET Core Web API
- Entity Framework Core
- SQLite

### Front-end

- React
- TypeScript

> Observação: o front-end ainda será desenvolvido em uma próxima etapa.

## Estrutura atual do projeto

```text
ControleGastosResidenciais/
│
├── backend/
│   └── ControleGastos.API/
│       ├── Data/
│       │   └── ControleGastosDbContext.cs
│       │
│       ├── Enums/
│       │   └── TipoTransacao.cs
│       │
│       ├── Models/
│       │   ├── Pessoa.cs
│       │   └── Transacao.cs
│       │
│       ├── Program.cs
│       ├── appsettings.json
│       └── ControleGastos.API.csproj
│
├── .gitignore
├── README.md
└── ControleGastos.slnx

Modelagem inicial
Pessoa

A entidade Pessoa representa uma pessoa cadastrada no sistema.

Campos definidos até o momento:

Id: identificador único da pessoa;
Nome: nome informado pelo usuário;
DataNascimento: data usada para calcular a idade;
Idade: propriedade calculada automaticamente;
Transacoes: lista de transações relacionadas à pessoa.

A idade não é armazenada diretamente no banco de dados. Ela é calculada a partir da data de nascimento, evitando que o cadastro fique desatualizado com o passar do tempo.

Transacao

A entidade Transacao representa uma receita ou despesa cadastrada para uma pessoa.

Campos definidos até o momento:

Id: identificador único da transação;
Descricao: descrição da origem da receita ou despesa;
Valor: valor financeiro da transação;
Tipo: tipo da transação;
PessoaId: identificador da pessoa relacionada;
Pessoa: propriedade de navegação para representar o vínculo com a pessoa.
TipoTransacao

Foi criado um enum chamado TipoTransacao para limitar os tipos possíveis de transação.

Valores definidos:

Receita = 1
Despesa = 2

Essa decisão evita o uso de textos livres como "receita", "Receitaa" ou "despesa", reduzindo erros no cadastro.

Regras de negócio previstas
Uma pessoa pode existir sem transações;
Uma transação deve estar relacionada a uma pessoa cadastrada;
Ao excluir uma pessoa, suas transações também deverão ser removidas;
Pessoas menores de 18 anos poderão cadastrar apenas despesas;
O saldo será calculado por pessoa usando:
Saldo = Total de receitas - Total de despesas
Decisões técnicas iniciais
Uso de DateOnly para data de nascimento

Foi escolhido DateOnly para armazenar apenas a data de nascimento, sem horário.

Como o sistema não precisa guardar hora para esse campo, essa escolha deixa o modelo mais adequado ao domínio.

Uso de decimal para valores financeiros

Na entidade Transacao, o campo Valor foi definido como decimal, pois valores financeiros precisam de maior precisão.

Uso de enum para o tipo da transação

O tipo da transação foi modelado como enum, pois existem apenas duas opções válidas:

Receita;
Despesa.

Isso ajuda a evitar valores inválidos no sistema.

Relacionamento entre Pessoa e Transacao

Foi definido um relacionamento em que:

Uma pessoa pode ter várias transações.
Uma transação pertence a uma pessoa.

Esse relacionamento será usado posteriormente para calcular os totais de receitas, despesas e saldo por pessoa.

...
