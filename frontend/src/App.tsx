import { useEffect, useState } from 'react'
import './App.css'
import PessoasSection from './components/PessoasSection'
import TransacoesSection from './components/TransacoesSection'
import { buscarPessoas, buscarTransacoes } from './services/api'
import type { Pessoa, Transacao } from './types'

function App() {
  const [pessoas, setPessoas] = useState<Pessoa[]>([])
  const [transacoes, setTransacoes] = useState<Transacao[]>([])

  async function carregarDados() {
    const pessoasApi = await buscarPessoas()
    const transacoesApi = await buscarTransacoes()

    setPessoas(pessoasApi)
    setTransacoes(transacoesApi)
  }

  useEffect(() => {
    carregarDados()
  }, [])

  return (
    <main className="container">
      <h1>Controle de Gastos Residenciais</h1>

      <PessoasSection
        pessoas={pessoas}
        onDadosAtualizados={carregarDados}
      />

      <TransacoesSection
        pessoas={pessoas}
        transacoes={transacoes}
      />
    </main>
  )
}

export default App