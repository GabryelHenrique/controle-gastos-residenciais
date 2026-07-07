export type Pessoa = {
  id: number
  nome: string
  dataNascimento: string
  idade: number
}

export type Transacao = {
  id: number
  descricao: string
  valor: number
  tipo: number
  pessoaId: number
}

export type CriarPessoaRequest = {
  nome: string
  dataNascimento: string
}