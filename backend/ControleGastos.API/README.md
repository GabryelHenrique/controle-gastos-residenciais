# Controle de Gastos Residenciais - Back-end

Este README documenta a API do projeto **Controle de Gastos Residenciais**.

O back-end foi desenvolvido com **C#**, **ASP.NET Core Web API**, **Entity Framework Core** e **SQLite**. Ele é responsável por receber as requisições do front-end, validar os dados, aplicar as regras de negócio, persistir as informações e retornar respostas em JSON.

---

## Objetivo da API

A API foi criada para controlar pessoas e transações financeiras de uma residência.

Com ela é possível:

- Cadastrar pessoas
- Listar pessoas cadastradas
- Buscar pessoa por Id
- Excluir pessoas
- Cadastrar receitas e despesas
- Listar transações
- Buscar transação por Id
- Consultar totais financeiros por pessoa
- Consultar totais gerais da residência
- Aplicar regras de negócio
- Persistir os dados em SQLite

---

## Tecnologias utilizadas

- C#
- ASP.NET Core Web API
- .NET 10
- Entity Framework Core
- SQLite
- REST Client para testes manuais
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
Organização da API

A estrutura do back-end foi separada por responsabilidade, evitando concentrar toda a lógica em um único arquivo.

Controllers/
→ Recebem as requisições HTTP e executam as ações da API.

Models/
→ Representam as entidades principais do sistema.

Dtos/
→ Definem os dados recebidos e retornados pela API.

Data/
→ Contém o DbContext, responsável pela comunicação com o banco.

Enums/
→ Contém valores fixos usados na aplicação, como Receita e Despesa.

Essa organização foi usada para deixar o código mais claro, legível e fácil de manter.

Models principais
Pessoa

Representa uma pessoa cadastrada no sistema.

Principais dados:

Id
Nome
DataNascimento
Idade calculada
Transações vinculadas

A idade não é salva diretamente no banco.
Ela é calculada a partir da data de nascimento, evitando que fique desatualizada com o passar do tempo.

Transacao

Representa uma movimentação financeira vinculada a uma pessoa.

Principais dados:

Id
Descrição
Valor
Tipo
PessoaId

Cada transação pertence a uma pessoa cadastrada.

DTOs

DTO significa Data Transfer Object.

No projeto, os DTOs foram usados para separar os dados recebidos e retornados pela API dos Models principais.

DTOs criados:

CriarPessoaDto
→ Dados usados para cadastrar uma pessoa.

CriarTransacaoDto
→ Dados usados para cadastrar uma transação.

TransacaoRespostaDto
→ Dados retornados ao listar ou buscar transações.

TotalPessoaDto
→ Dados financeiros calculados para cada pessoa.

ResumoTotaisDto
→ Resumo financeiro geral da aplicação.

O uso de DTOs também ajudou a evitar problemas de ciclo JSON entre entidades relacionadas.

Enum de transação

O tipo da transação foi definido com enum:

1 = Receita
2 = Despesa

Essa decisão evita valores livres e limita os tipos aceitos pela API.

Banco de dados

O banco utilizado foi o SQLite.

A comunicação entre a API e o banco é feita com Entity Framework Core.

A string de conexão está configurada no arquivo appsettings.json:

{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=controle_gastos.db"
  }
}

Tabelas principais:

Pessoas
Transacoes

A tabela de transações possui vínculo com a tabela de pessoas por meio do campo PessoaId.

Regras de negócio
Pessoas
O nome é obrigatório
A data de nascimento não pode ser futura
O Id é gerado automaticamente
A idade é calculada a partir da data de nascimento
Ao excluir uma pessoa, suas transações também são excluídas
Transações
A descrição é obrigatória
O valor deve ser maior que zero
O tipo deve ser válido
A pessoa vinculada precisa existir
Menores de idade só podem cadastrar despesas
Maiores de idade podem cadastrar receitas e despesas
Totais

A API calcula:

Total de receitas por pessoa
Total de despesas por pessoa
Saldo por pessoa
Total geral de receitas
Total geral de despesas
Saldo líquido geral

O saldo é calculado assim:

Saldo = Total de Receitas - Total de Despesas
Endpoints da API

URL base local:

http://localhost:5244
Pessoas
Listar pessoas
GET /api/pessoas

Lista todas as pessoas cadastradas.

Buscar pessoa por Id
GET /api/pessoas/{id}

Busca uma pessoa específica pelo Id.

Cadastrar pessoa
POST /api/pessoas

Exemplo de requisição:

{
  "nome": "Gabryel",
  "dataNascimento": "1997-02-01"
}

Possíveis validações:

Nome obrigatório
Data de nascimento não pode ser futura
Excluir pessoa
DELETE /api/pessoas/{id}

Exclui uma pessoa cadastrada.

Ao excluir uma pessoa, as transações vinculadas a ela também são removidas.

Transações
Listar transações
GET /api/transacoes

Lista todas as transações cadastradas.

Buscar transação por Id
GET /api/transacoes/{id}

Busca uma transação específica pelo Id.

Cadastrar transação
POST /api/transacoes

Exemplo de receita:

{
  "descricao": "Salário",
  "valor": 2500,
  "tipo": 1,
  "pessoaId": 1
}

Exemplo de despesa:

{
  "descricao": "Mercado",
  "valor": 300,
  "tipo": 2,
  "pessoaId": 1
}

Tipos aceitos:

1 = Receita
2 = Despesa

Possíveis validações:

Descrição obrigatória
Valor maior que zero
Tipo válido
Pessoa existente
Menor de idade não pode cadastrar receita
Totais
Consultar totais
GET /api/totais

Retorna os totais financeiros por pessoa e o resumo geral.

Exemplo de resposta:

{
  "pessoas": [
    {
      "pessoaId": 1,
      "nome": "Gabryel",
      "idade": 29,
      "totalReceitas": 2500,
      "totalDespesas": 300,
      "saldo": 2200
    }
  ],
  "totalGeralReceitas": 2500,
  "totalGeralDespesas": 300,
  "saldoLiquidoGeral": 2200
}
CORS

Durante o desenvolvimento, o front-end e o back-end rodam em portas diferentes:

Back-end: http://localhost:5244
Front-end: http://localhost:5173

Por isso, o CORS foi configurado no Program.cs para permitir que o front-end consiga acessar a API.

Como executar o back-end

A partir da raiz do projeto, acesse a pasta da API:

cd backend/ControleGastos.API

Restaure as dependências:

dotnet restore

Compile o projeto:

dotnet build

Aplique as migrations:

dotnet ef database update

Execute a API:

dotnet run

A API será executada em:

http://localhost:5244
Testes realizados

Os testes foram feitos manualmente usando requisições HTTP e também durante a integração com o front-end.

Pessoas

Foram validados:

Cadastro de pessoa maior de idade
Cadastro de pessoa menor de idade
Cadastro com nome vazio
Cadastro com data futura
Listagem de pessoas
Busca por Id
Exclusão de pessoa
Tentativa de exclusão de pessoa inexistente
Transações

Foram validados:

Cadastro de receita para pessoa maior de idade
Cadastro de despesa para pessoa maior de idade
Cadastro de despesa para pessoa menor de idade
Bloqueio de receita para pessoa menor de idade
Cadastro com descrição vazia
Cadastro com valor zero
Cadastro com valor negativo
Cadastro com tipo inválido
Cadastro com pessoa inexistente
Listagem de transações
Busca de transação por Id
Totais

Foram validados:

Total de receitas por pessoa
Total de despesas por pessoa
Saldo por pessoa
Total geral de receitas
Total geral de despesas
Saldo líquido geral
Atualização dos totais após novos cadastros
Atualização dos totais após exclusão de pessoa
Exclusão em cascata

Foi validado que, ao excluir uma pessoa:

A pessoa é removida
As transações vinculadas também são removidas
Os totais são recalculados corretamente
Status do back-end
Back-end funcional e validado localmente.

Funcionalidades concluídas:

API REST criada
Banco SQLite configurado
Entity Framework Core configurado
Migrations criadas e aplicadas
Cadastro de pessoas
Exclusão de pessoas
Cadastro de transações
Consulta de totais
Regras de negócio
Validações
CORS para integração com o front-end
Testes manuais concluídos

