import type { 
    CriarPessoaRequest, 
    CriarTransacaoRequest, 
    Pessoa, 
    Transacao,
} from '../types'

const API_URL = 'http://localhost:5244/api'

export async function buscarPessoas(): Promise<Pessoa[]> {
  const resposta = await fetch(`${API_URL}/pessoas`)
  return resposta.json()
}

export async function cadastrarPessoa(dados: CriarPessoaRequest): Promise<void> {
  const resposta = await fetch(`${API_URL}/pessoas`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(dados),
  })

  if (!resposta.ok) {
    const mensagemErro = await resposta.text()
    throw new Error(mensagemErro || 'Erro ao cadastrar pessoa.')
  }
}

export async function excluirPessoa(id: number): Promise<void> {
  const resposta = await fetch(`${API_URL}/pessoas/${id}`, {
    method: 'DELETE',
  })

  if (!resposta.ok) {
    const mensagemErro = await resposta.text()
    throw new Error(mensagemErro || 'Erro ao excluir pessoa.')
  }
}

export async function buscarTransacoes(): Promise<Transacao[]> {
  const resposta = await fetch(`${API_URL}/transacoes`)
  return resposta.json()
}

export async function cadastrarTransacao(
  dados: CriarTransacaoRequest
): Promise<void> {
  const resposta = await fetch(`${API_URL}/transacoes`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(dados),
  })

  if (!resposta.ok) {
    const mensagemErro = await resposta.text()
    throw new Error(mensagemErro || 'Erro ao cadastrar transação.')
  }
}
