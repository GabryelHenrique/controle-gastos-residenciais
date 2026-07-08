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

export type CriarTransacaoRequest = {
  descricao: string
  valor: number
  tipo: number
  pessoaId: number
}

export type TotalPessoa = {
  pessoaId: number
  nome: string
  idade: number
  totalReceitas: number
  totalDespesas: number
  saldo: number
}

export type ResumoTotais = {
  pessoas: TotalPessoa[]
  totalGeralReceitas: number
  totalGeralDespesas: number
  saldoLiquidoGeral: number
}
