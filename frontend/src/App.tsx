import { useEffect, useState } from 'react'
import './App.css'
import PessoasSection from './components/PessoasSection'
import TransacoesSection from './components/TransacoesSection'
import TotaisSection from './components/TotaisSection'
import { buscarPessoas, buscarTotais, buscarTransacoes } from './services/api'
import type { Pessoa, ResumoTotais, Transacao } from './types'

function App() {
  const [pessoas, setPessoas] = useState<Pessoa[]>([])
  const [transacoes, setTransacoes] = useState<Transacao[]>([])
  const [totais, setTotais] = useState<ResumoTotais | null>(null)

  async function carregarDados() {
    const pessoasApi = await buscarPessoas()
    const transacoesApi = await buscarTransacoes()
    const totaisApi = await buscarTotais()

    setPessoas(pessoasApi)
    setTransacoes(transacoesApi)
    setTotais(totaisApi)
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
        onDadosAtualizados={carregarDados}
      />

      <TotaisSection totais={totais} />
    </main>
  )
}

export default App