import { useEffect, useState, type FormEvent } from 'react'
import './App.css'

type Pessoa = {
  id: number
  nome: string
  dataNascimento: string
  idade: number
}

const API_URL = 'http://localhost:5244/api'

function App() {
  const [pessoas, setPessoas] = useState<Pessoa[]>([])
  const [nome, setNome] = useState('')
  const [dataNascimento, setDataNascimento] = useState('')
  const [erro, setErro] = useState('')

  async function buscarPessoas() {
    const resposta = await fetch(`${API_URL}/pessoas`)
    const dados = await resposta.json()

    setPessoas(dados)
  }

  async function cadastrarPessoa(event: FormEvent<HTMLFormElement>) {
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

    const resposta = await fetch(`${API_URL}/pessoas`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        nome: nome.trim(),
        dataNascimento,
      }),
    })

    if (!resposta.ok) {
      const mensagemErro = await resposta.text()
      setErro(mensagemErro)
      return
    }

    setNome('')
    setDataNascimento('')

    await buscarPessoas()
  }

  async function excluirPessoa(id: number) {
    const confirmar = window.confirm(
      'Deseja realmente excluir essa pessoa? As transações vinculadas a ela também serão apagadas.'
    )

    if (!confirmar) {
      return
    }

    setErro('')

    const resposta = await fetch(`${API_URL}/pessoas/${id}`, {
      method: 'DELETE',
    })

    if (!resposta.ok) {
      const mensagemErro = await resposta.text()
      setErro(mensagemErro || 'Erro ao excluir essa pessoa.')
      return
    }

    await buscarPessoas()
  }

  useEffect(() => {
    buscarPessoas()
  }, [])

  return (
    <main className="container">
      <h1>Controle de Gastos Residenciais</h1>

      <section className="card">
        <h2>Pessoas</h2>

        <form className="formulario" onSubmit={cadastrarPessoa}>
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
                  onClick={() => excluirPessoa(pessoa.id)}
                >
                  Excluir
                </button>
              </li>
            ))}
          </ul>
        )}
      </section>
    </main>
  )
}

export default App