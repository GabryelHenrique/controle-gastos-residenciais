# Controle de Gastos Residenciais

Projeto desenvolvido como parte de um case técnico para controle de gastos residenciais.

A aplicação permite cadastrar pessoas, registrar receitas e despesas, aplicar regras de negócio para menores de idade, consultar totais financeiros por pessoa e visualizar o saldo geral da residência.

O projeto foi desenvolvido com foco em:

- Organização de código
- Separação de responsabilidades
- Regras de negócio claras
- Integração entre back-end e front-end
- Persistência de dados
- Interface funcional e validada

---

## Tecnologias utilizadas

### Back-end

- C#
- ASP.NET Core Web API
- .NET 10
- Entity Framework Core
- SQLite
- REST Client para testes HTTP

### Front-end

- React
- TypeScript
- Vite
- CSS
- Fetch API

### Versionamento

- Git
- GitHub

---

## Objetivo do projeto

O objetivo da aplicação é permitir o controle financeiro de pessoas de uma residência.

## Funcionalidades implementadas
 
## Pessoas
- Cadastro de pessoas
- Listagem de pessoas
- Busca de pessoa por Id no back-end
- Exclusão de pessoas
- Cálculo automático da idade a partir da data de nascimento
- Validação de nome obrigatório
- Validação de data de nascimento futura
## Transações
- Cadastro de transações financeiras
- Listagem de transações
- Busca de transação por Id no back-end
- Associação de transação a uma pessoa
- Cadastro de receitas
- Cadastro de despesas
- Validação de descrição obrigatória
- Validação de valor maior que zero
- Validação de tipo da transação
- Validação de pessoa existente
## Regras de negócio
- Pessoas menores de 18 anos podem cadastrar apenas despesas
- Pessoas maiores de idade podem cadastrar receitas e despesas
- Ao excluir uma pessoa, todas as transações vinculadas a ela também são excluídas
- Os totais são recalculados após cadastros e exclusões
## Totais financeiros
- Total de receitas por pessoa
- Total de despesas por pessoa
- Saldo por pessoa
- Total geral de receitas
- Total geral de despesas

---

## Estrutura geral do projeto

```text
ControleGastosResidenciais/
├── backend/
│   └── ControleGastos.API/
│       ├── Controllers/
│       ├── Data/
│       ├── Dtos/
│       ├── Enums/
│       ├── Models/
│       ├── Program.cs
│       └── appsettings.json
│
├── frontend/
│   ├── src/
│   │   ├── components/
│   │   ├── services/
│   │   ├── types/
│   │   ├── App.tsx
│   │   ├── App.css
│   │   └── main.tsx
│   │
│   ├── package.json
│   ├── index.html
│   └── vite.config.ts
│
├── README.md
└── .gitignore

Organização do back-end

O back-end foi organizado em camadas para facilitar a leitura e manutenção do código.

Controllers/
→ Recebem as requisições HTTP e executam as ações da API.

Models/
→ Representam as entidades principais do sistema.

Dtos/
→ Representam os dados recebidos e retornados pela API.

Data/
→ Contém o DbContext, responsável pela comunicação com o banco de dados.

Enums/
→ Contém valores fixos usados na aplicação, como Receita e Despesa.

A API é responsável por validar os dados, aplicar regras de negócio e persistir as informações no banco SQLite.

Organização do front-end

O front-end foi separado em pastas para evitar que toda a lógica ficasse concentrada no App.tsx.

components/
→ Contém as seções visuais da aplicação.

services/
→ Centraliza as chamadas HTTP para a API.

types/
→ Define os formatos dos dados usados no TypeScript.

Essa organização melhora a clareza do projeto e separa melhor as responsabilidades da interface.

Componentes principais
PessoasSection.tsx
→ Cadastro, listagem e exclusão de pessoas.

TransacoesSection.tsx
→ Cadastro e listagem de transações.

TotaisSection.tsx
→ Exibição dos totais por pessoa e dos totais gerais.
Decisões técnicas
Idade calculada

A idade não é armazenada diretamente no banco de dados.

Foi escolhida a estratégia de armazenar a data de nascimento e calcular a idade automaticamente no back-end. Isso evita que a idade fique desatualizada com o passar do tempo.

Uso de DTOs

Foram utilizados DTOs para separar os dados de entrada e saída da API dos Models principais.

Essa decisão ajuda a:

Evitar exposição desnecessária das entidades
Organizar melhor os dados recebidos e retornados
Evitar problemas de ciclo JSON entre entidades relacionadas
Facilitar o consumo da API pelo front-end
Uso de enum para tipo de transação

O tipo da transação foi definido com enum:

1 = Receita
2 = Despesa

Isso evita valores livres e ajuda a manter o controle dos tipos aceitos pela aplicação.

Separação do front-end em components, services e types

A organização do front-end foi pensada para demonstrar clareza na estrutura do projeto.

components
→ cuidam da interface

services/api.ts
→ cuida da comunicação com o back-end

types
→ define os formatos dos dados usados na aplicação

Essa separação evita que o código fique concentrado em um único arquivo e facilita a manutenção.

Banco de dados

O projeto utiliza SQLite como banco de dados local.

A comunicação entre o back-end e o SQLite é feita por meio do Entity Framework Core.

As principais tabelas do sistema são:

Pessoas
Transacoes

Cada pessoa possui um Id gerado automaticamente.

Cada transação possui um PessoaId, que representa o vínculo com a pessoa cadastrada.

Como executar o projeto

Para utilizar a aplicação localmente, é necessário executar o back-end e o front-end em terminais separados.

Executar o back-end

Acesse a pasta da API:

cd backend/ControleGastos.API

Restaure as dependências:

dotnet restore

Aplique as migrations:

dotnet ef database update

Execute a API:

dotnet run

A API será executada em:

http://localhost:5244
Executar o front-end

Em outro terminal, acesse a pasta do front-end:

cd frontend

Instale as dependências:

npm install

Execute o projeto:

npm run dev

O front-end será executado em:

http://localhost:5173
Integração entre front-end e back-end

Durante o desenvolvimento, o back-end foi configurado com CORS para permitir requisições vindas do front-end.

Back-end: http://localhost:5244
Front-end: http://localhost:5173

O front-end se comunica com a API através do arquivo:

frontend/src/services/api.ts

Esse arquivo centraliza as chamadas HTTP, como:

buscarPessoas
cadastrarPessoa
excluirPessoa
buscarTransacoes
cadastrarTransacao
buscarTotais
Testes realizados

Foram realizados testes manuais tanto no back-end quanto no front-end.

Testes no back-end

Os endpoints foram testados com arquivo .http utilizando REST Client.

Foram validados:

Cadastro de pessoa maior de idade
Cadastro de pessoa menor de idade
Listagem de pessoas
Busca de pessoa por Id
Exclusão de pessoa
Cadastro de receita
Cadastro de despesa
Bloqueio de receita para menor de idade
Cadastro de despesa para menor de idade
Validação de pessoa inexistente
Validação de descrição vazia
Validação de valor inválido
Validação de tipo inválido
Consulta de totais
Exclusão em cascata
Testes no front-end

Os testes também foram realizados diretamente pela interface React.

Foram validados:

Cadastro de pessoa pela tela
Listagem de pessoas cadastradas
Exibição da idade calculada
Validação de nome obrigatório
Validação de data de nascimento obrigatória
Validação de data futura retornada pelo back-end
Exclusão de pessoa pela tela
Cancelamento da exclusão
Cadastro de receita para pessoa maior de idade
Cadastro de despesa para pessoa maior de idade
Cadastro de despesa para pessoa menor de idade
Bloqueio de receita para pessoa menor de idade
Cadastro de transação sem descrição
Cadastro de transação com valor zero
Cadastro de transação sem pessoa selecionada
Listagem de transações
Exibição do tipo como Receita ou Despesa
Exibição dos valores em moeda brasileira
Exibição da pessoa vinculada à transação
Exibição dos totais por pessoa
Exibição dos totais gerais
Atualização automática dos dados após cadastro
Atualização automática dos dados após exclusão
Recalculo dos totais após exclusão em cascata

Documentações específicas

Este README apresenta a visão geral do projeto.

Para detalhes técnicos de cada parte, consulte:

backend/README.md
→ Documentação específica do back-end.

frontend/README.md
→ Documentação específica do front-end.
