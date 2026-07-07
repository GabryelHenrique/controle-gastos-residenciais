import { useState, type FormEvent } from 'react'
import { cadastrarTransacao } from '../services/api'
import type { Pessoa, Transacao } from '../types'

type TransacoesSectionProps = {
  pessoas: Pessoa[]
  transacoes: Transacao[]
  onDadosAtualizados: () => Promise<void>
}

function TransacoesSection({
  pessoas,
  transacoes,
  onDadosAtualizados,
}: TransacoesSectionProps) {
  const [descricao, setDescricao] = useState('')
  const [valor, setValor] = useState('')
  const [tipo, setTipo] = useState('2')
  const [pessoaId, setPessoaId] = useState('')
  const [erro, setErro] = useState('')

  async function handleCadastrarTransacao(event: FormEvent<HTMLFormElement>) {
    event.preventDefault()

    setErro('')

    if (!descricao.trim()) {
      setErro('Informe a descrição da transação.')
      return
    }

    if (!valor || Number(valor) <= 0) {
      setErro('Informe um valor maior que zero.')
      return
    }

    if (!pessoaId) {
      setErro('Selecione uma pessoa.')
      return
    }

    try {
      await cadastrarTransacao({
        descricao: descricao.trim(),
        valor: Number(valor),
        tipo: Number(tipo),
        pessoaId: Number(pessoaId),
      })

      setDescricao('')
      setValor('')
      setTipo('2')
      setPessoaId('')

      await onDadosAtualizados()
    } catch (error) {
      if (error instanceof Error) {
        setErro(error.message)
      } else {
        setErro('Erro inesperado ao cadastrar transação.')
      }
    }
  }

  function obterNomePessoa(pessoaId: number) {
    const pessoa = pessoas.find((pessoa) => pessoa.id === pessoaId)

    return pessoa ? pessoa.nome : `Pessoa ${pessoaId}`
  }

  function formatarTipo(tipo: number) {
    return tipo === 1 ? 'Receita' : 'Despesa'
  }

  function formatarValor(valor: number) {
    return valor.toLocaleString('pt-BR', {
      style: 'currency',
      currency: 'BRL',
    })
  }

  return (
    <section className="card">
      <h2>Transações</h2>

      <form className="formulario" onSubmit={handleCadastrarTransacao}>
        <select
          value={pessoaId}
          onChange={(event) => setPessoaId(event.target.value)}
        >
          <option value="">Selecione uma pessoa</option>

          {pessoas.map((pessoa) => (
            <option key={pessoa.id} value={pessoa.id}>
              {pessoa.nome}
            </option>
          ))}
        </select>

        <input
          type="text"
          placeholder="Descrição"
          value={descricao}
          onChange={(event) => setDescricao(event.target.value)}
        />

        <input
          type="number"
          placeholder="Valor"
          min="0"
          step="0.01"
          value={valor}
          onChange={(event) => setValor(event.target.value)}
        />

        <select
          value={tipo}
          onChange={(event) => setTipo(event.target.value)}
        >
          <option value="1">Receita</option>
          <option value="2">Despesa</option>
        </select>

        <button type="submit">
          Cadastrar transação
        </button>
      </form>

      {erro && <p className="erro">{erro}</p>}

      {transacoes.length === 0 ? (
        <p>Nenhuma transação cadastrada.</p>
      ) : (
        <ul className="lista">
          {transacoes.map((transacao) => (
            <li key={transacao.id}>
              <span>
                <strong>{transacao.descricao}</strong> -{' '}
                {formatarTipo(transacao.tipo)} -{' '}
                {formatarValor(transacao.valor)} -{' '}
                {obterNomePessoa(transacao.pessoaId)}
              </span>
            </li>
          ))}
        </ul>
      )}
    </section>
  )
}

export default TransacoesSection