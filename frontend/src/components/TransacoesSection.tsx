import type { Pessoa, Transacao } from '../types'

type TransacoesSectionProps = {
  pessoas: Pessoa[]
  transacoes: Transacao[]
}

function TransacoesSection({ pessoas, transacoes }: TransacoesSectionProps) {
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