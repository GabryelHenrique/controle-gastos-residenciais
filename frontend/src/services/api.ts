import type { 
    CriarPessoaRequest, 
    CriarTransacaoRequest, 
    Pessoa, 
    ResumoTotais,
    Transacao,
} from '../types'

const API_URL = 'http://localhost:5244/api'

// Buscar todas pessoas cadastradas na API.
export async function buscarPessoas(): Promise<Pessoa[]> {
  const resposta = await fetch(`${API_URL}/pessoas`)
  return resposta.json()
}

// Envia para a API dados necessários para um novo cadastro.
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

// Solicitar API exclusão de uma pessoa através do Id.
export async function excluirPessoa(id: number): Promise<void> {
  const resposta = await fetch(`${API_URL}/pessoas/${id}`, {
    method: 'DELETE',
  })

  if (!resposta.ok) {
    const mensagemErro = await resposta.text()
    throw new Error(mensagemErro || 'Erro ao excluir pessoa.')
  }
}

// Busca todas as transações cadastradas na API.
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

// Busca o resumo financeiro já calculado pelo back-end.
export async function buscarTotais(): Promise<ResumoTotais> {
  const resposta = await fetch(`${API_URL}/totais`)
  return resposta.json()
}
