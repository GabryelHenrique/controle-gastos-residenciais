import { useState, type FormEvent } from 'react'
import { cadastrarPessoa, excluirPessoa } from '../services/api'
import type { Pessoa } from '../types'

type PessoasSectionProps = {
  pessoas: Pessoa[]
  onDadosAtualizados: () => Promise<void>
}

function PessoasSection({ pessoas, onDadosAtualizados }: PessoasSectionProps) {
  const [nome, setNome] = useState('')
  const [dataNascimento, setDataNascimento] = useState('')
  const [erro, setErro] = useState('')

  async function handleCadastrarPessoa(event: FormEvent<HTMLFormElement>) {
    event.preventDefault()

    setErro('')

    if (!nome.trim()) {
      setErro('Informe o nome da pessoa.')
      return
    }

    if (!dataNascimento) {
      setErro('Informe a data de nascimento.')
      return
    }

    try {
      await cadastrarPessoa({
        nome: nome.trim(),
        dataNascimento,
      })

      setNome('')
      setDataNascimento('')

      await onDadosAtualizados()
    } catch (error) {
      if (error instanceof Error) {
        setErro(error.message)
      } else {
        setErro('Erro inesperado ao cadastrar pessoa.')
      }
    }
  }

  async function handleExcluirPessoa(id: number) {
    const confirmar = window.confirm(
      'Deseja realmente excluir essa pessoa? As transações vinculadas a ela também serão apagadas.'
    )

    if (!confirmar) {
      return
    }

    setErro('')

    try {
      await excluirPessoa(id)
      await onDadosAtualizados()
    } catch (error) {
      if (error instanceof Error) {
        setErro(error.message)
      } else {
        setErro('Erro inesperado ao excluir pessoa.')
      }
    }
  }

  return (
    <section className="card">
      <h2>Pessoas</h2>

      <form className="formulario" onSubmit={handleCadastrarPessoa}>
        <input
          type="text"
          placeholder="Nome"
          value={nome}
          onChange={(event) => setNome(event.target.value)}
        />

        <input
          type="date"
          value={dataNascimento}
          onChange={(event) => setDataNascimento(event.target.value)}
        />

        <button type="submit">
          Cadastrar pessoa
        </button>
      </form>

      {erro && <p className="erro">{erro}</p>}

      {pessoas.length === 0 ? (
        <p>Nenhuma pessoa cadastrada.</p>
      ) : (
        <ul className="lista">
          {pessoas.map((pessoa) => (
            <li key={pessoa.id}>
              <span>
                <strong>{pessoa.nome}</strong> - {pessoa.idade} anos
              </span>

              <button
                type="button"
                className="botao-excluir"
                onClick={() => handleExcluirPessoa(pessoa.id)}
              >
                Excluir
              </button>
            </li>
          ))}
        </ul>
      )}
    </section>
  )
}

export default PessoasSection