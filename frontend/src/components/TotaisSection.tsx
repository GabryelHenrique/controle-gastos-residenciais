import type { ResumoTotais } from '../types'


// O componente Total, não vai ter responsabilidade de fazer calculos, ele apenas vai exibir os dados já calculados no backend.
// Resumo financeiro retornado pela API.
type TotaisSectionProps = {
  totais: ResumoTotais | null
}

// Formata os valores numéricos para a moeda brasileira (real).
function TotaisSection({ totais }: TotaisSectionProps) {
  function formatarValor(valor: number) {
    return valor.toLocaleString('pt-BR', {
      style: 'currency',
      currency: 'BRL',
    })
  }

  if (!totais) {
    return (
      <section className="card">
        <h2>Totais</h2>
        <p>Nenhum total carregado.</p>
      </section>
    )
  }

  return (
    <section className="card">
      <h2>Totais</h2>

      {totais.pessoas.length === 0 ? (
        <p>Nenhum dado financeiro cadastrado.</p>
      ) : (
        <ul className="lista">
          {totais.pessoas.map((pessoa) => (
            <li key={pessoa.pessoaId}>
              <span>
                <strong>{pessoa.nome}</strong> - {pessoa.idade} anos
              </span>

              <span>
                Receitas: {formatarValor(pessoa.totalReceitas)} | Despesas:{' '}
                {formatarValor(pessoa.totalDespesas)} | Saldo:{' '}
                {formatarValor(pessoa.saldo)}
              </span>
            </li>
          ))}
        </ul>
      )}

      <div className="resumo-geral">
        <p>
          <strong>Total geral de receitas:</strong>{' '}
          {formatarValor(totais.totalGeralReceitas)}
        </p>

        <p>
          <strong>Total geral de despesas:</strong>{' '}
          {formatarValor(totais.totalGeralDespesas)}
        </p>

        <p>
          <strong>Saldo líquido geral:</strong>{' '}
          {formatarValor(totais.saldoLiquidoGeral)}
        </p>
      </div>
    </section>
  )
}

export default TotaisSection