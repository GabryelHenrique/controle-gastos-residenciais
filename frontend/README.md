# Controle de Gastos Residenciais - Front-end

Este README documenta a parte de front-end do projeto **Controle de Gastos Residenciais**.

O front-end foi desenvolvido com **React**, **TypeScript** e **Vite**, sendo responsável por exibir a interface da aplicação, permitir a interação do usuário, enviar dados para a API e apresentar os resultados retornados pelo back-end.

---

## Objetivo do front-end

O front-end tem como objetivo permitir que o usuário utilize o sistema de forma visual e simples.

Pela interface é possível:

- Cadastrar pessoas
- Listar pessoas cadastradas
- Excluir pessoas
- Cadastrar receitas e despesas
- Listar transações
- Visualizar totais financeiros por pessoa
- Visualizar o total geral da residência
- Exibir mensagens de erro retornadas pela API
- Atualizar a tela automaticamente após cadastros e exclusões

A construção foi feita com foco em organização, clareza da lógica e separação de responsabilidades.

---

## Tecnologias utilizadas

- React
- TypeScript
- Vite
- CSS
- Fetch API
- npm
- Git e GitHub

---

## Estrutura do front-end

```text
frontend/
├── src/
│   ├── components/
│   │   ├── PessoasSection.tsx
│   │   ├── TransacoesSection.tsx
│   │   └── TotaisSection.tsx
│   │
│   ├── services/
│   │   └── api.ts
│   │
│   ├── types/
│   │   └── index.ts
│   │
│   ├── App.tsx
│   ├── App.css
│   ├── index.css
│   └── main.tsx
│
├── package.json
├── index.html
└── vite.config.ts
Organização da interface

O front-end foi separado em pastas para evitar que toda a lógica ficasse concentrada no App.tsx.

components/
→ Contém as seções visuais da aplicação.

services/
→ Centraliza a comunicação com a API.

types/
→ Define os formatos dos dados usados pelo TypeScript.

Essa divisão deixa o código mais organizado, facilita a leitura e melhora a manutenção do projeto.

Componentes
PessoasSection.tsx

Componente responsável pela seção de pessoas.

Funcionalidades:

Cadastro de pessoa
Validação de nome obrigatório
Validação de data de nascimento obrigatória
Listagem de pessoas cadastradas
Exibição da idade calculada pelo back-end
Exclusão de pessoa
Confirmação antes da exclusão
Exibição de mensagens de erro
TransacoesSection.tsx

Componente responsável pela seção de transações.

Funcionalidades:

Cadastro de transação
Seleção da pessoa vinculada
Cadastro de receita ou despesa
Validação de descrição obrigatória
Validação de valor maior que zero
Validação de pessoa selecionada
Listagem de transações
Exibição do tipo como Receita ou Despesa
Exibição do valor em moeda brasileira
Exibição do nome da pessoa vinculada à transação
TotaisSection.tsx

Componente responsável pela exibição dos totais financeiros.

Funcionalidades:

Exibição dos totais por pessoa
Exibição do total de receitas
Exibição do total de despesas
Exibição do saldo por pessoa
Exibição do total geral de receitas
Exibição do total geral de despesas
Exibição do saldo líquido geral

Os totais são calculados pelo back-end.
O front-end apenas recebe esses dados e apresenta na tela.

App.tsx

O App.tsx funciona como o organizador principal da interface.

Ele é responsável por:

Guardar os estados principais da aplicação
Carregar os dados da API
Repassar os dados para os componentes
Atualizar a tela após cadastros e exclusões

Estados principais usados:

pessoas
transacoes
totais

A função carregarDados centraliza a atualização dos dados principais da tela.

Essa função é usada quando a aplicação abre e também depois de ações como cadastro ou exclusão.

Fluxo simplificado:

App carrega os dados da API
↓
Repassa os dados para os componentes
↓
Componente executa uma ação
↓
App recarrega os dados
↓
Tela é atualizada
Comunicação com a API

A comunicação com o back-end foi centralizada no arquivo:

src/services/api.ts

Esse arquivo contém funções responsáveis por chamar os endpoints da API.

Principais funções:

buscarPessoas
cadastrarPessoa
excluirPessoa
buscarTransacoes
cadastrarTransacao
buscarTotais

Essa organização evita que os componentes fiquem cheios de chamadas fetch.

A separação ficou assim:

components
→ cuidam da interface e interação do usuário

services/api.ts
→ cuida das requisições HTTP

types
→ define o formato dos dados
Tipos TypeScript

Os tipos ficam no arquivo:

src/types/index.ts

Eles foram criados para representar os dados enviados e recebidos da API.

Principais tipos:

Pessoa
Transacao
CriarPessoaRequest
CriarTransacaoRequest
TotalPessoa
ResumoTotais

O uso de TypeScript ajuda a evitar erros, melhora a leitura do código e deixa mais claro quais dados cada parte da aplicação espera receber.

Validações no front-end

O front-end possui validações simples para melhorar a experiência do usuário antes de enviar os dados para a API.

Pessoas
Nome obrigatório
Data de nascimento obrigatória
Transações
Descrição obrigatória
Valor maior que zero
Pessoa selecionada obrigatória

As regras principais continuam sendo validadas pelo back-end, garantindo maior segurança e consistência.

Exemplo:

A regra de menor de idade não poder cadastrar receita é validada pelo back-end.
O front-end recebe a mensagem de erro e exibe para o usuário.
Integração com o back-end

Durante o desenvolvimento, a aplicação utiliza:

Back-end: http://localhost:5244
Front-end: http://localhost:5173

A URL base da API está configurada no arquivo api.ts:

const API_URL = 'http://localhost:5244/api'

O back-end precisa estar em execução para que o front-end consiga carregar, cadastrar e atualizar os dados.

Como executar o front-end

A partir da raiz do projeto, acesse a pasta do front-end:

cd frontend

Instale as dependências:

npm install

Execute o projeto:

npm run dev

O Vite irá disponibilizar a aplicação em:

http://localhost:5173
Testes realizados no front-end

Os testes foram realizados manualmente pela interface React, com o back-end em execução.

Pessoas

Foram validados:

Cadastro de pessoa pela tela
Cadastro de pessoa maior de idade
Cadastro de pessoa menor de idade
Validação de nome obrigatório
Validação de data de nascimento obrigatória
Exibição de erro retornado pelo back-end
Listagem de pessoas cadastradas
Exibição da idade calculada
Exclusão de pessoa
Confirmação antes da exclusão
Cancelamento da exclusão
Atualização automática da lista após cadastro
Atualização automática da lista após exclusão
Transações

Foram validados:

Cadastro de receita para pessoa maior de idade
Cadastro de despesa para pessoa maior de idade
Cadastro de despesa para pessoa menor de idade
Bloqueio de receita para pessoa menor de idade
Cadastro sem descrição
Cadastro com valor zero
Cadastro sem pessoa selecionada
Listagem de transações
Exibição correta do tipo Receita ou Despesa
Exibição correta dos valores em moeda brasileira
Exibição correta do nome da pessoa vinculada à transação
Atualização automática após cadastro de transação
Totais

Foram validados:

Exibição dos totais por pessoa
Exibição do total de receitas por pessoa
Exibição do total de despesas por pessoa
Exibição do saldo por pessoa
Exibição do total geral de receitas
Exibição do total geral de despesas
Exibição do saldo líquido geral
Atualização dos totais após cadastro de transação
Atualização dos totais após exclusão de pessoa
Exclusão em cascata pela interface

Foi validado que, ao excluir uma pessoa pela tela:

A pessoa é removida da listagem
As transações vinculadas também deixam de aparecer
Os totais são recalculados
A interface é atualizada automaticamente
Decisões de organização

Durante o desenvolvimento, foi priorizada a clareza da estrutura do front-end.

Algumas decisões importantes:

Separar a interface em componentes
Centralizar chamadas HTTP no api.ts
Criar tipos específicos no TypeScript
Manter o App.tsx como organizador geral da aplicação
Evitar concentrar toda a lógica em um único arquivo
Deixar o back-end responsável pelas regras principais de negócio
Usar o front-end para validações iniciais e melhor experiência do usuário

Essa organização foi pensada para deixar o projeto mais legível e demonstrar cuidado com manutenção, lógica e separação de responsabilidades.

Status do front-end
Front-end funcional e integrado com o back-end.

Funcionalidades concluídas:

Interface React criada
Componentes separados por responsabilidade
Integração com API
Cadastro de pessoas
Exclusão de pessoas
Cadastro de transações
Listagem de pessoas
Listagem de transações
Exibição de totais
Exibição de erros
Validações básicas no front-end
Atualização automática da tela após ações
Testes manuais concluídos