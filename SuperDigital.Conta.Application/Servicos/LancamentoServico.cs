using SuperDigital.Conta.Dominio.Entidades;
using SuperDigital.Conta.Dominio.Excecoes;
using SuperDigital.Conta.Dominio.Repositorios;
using SuperDigital.Conta.Dominio.Servicos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SuperDigital.Conta.Applicacao.Servicos
{
    public class LancamentoServico : ILancamentoServico
    {
        private readonly ILancamentoRepositorio _lancamentoRepositorio;
        private readonly IContaCorrenteRepositorio _contaCorrenteRepositorio;
        public LancamentoServico(ILancamentoRepositorio lancamentoRepositorio, IContaCorrenteRepositorio contaCorrenteRepositorio)
        {
            _lancamentoRepositorio = lancamentoRepositorio;
            _contaCorrenteRepositorio = contaCorrenteRepositorio;
        }

        public async Task<Guid> ExecutarTransacaoAsync(ContaCorrente contaCorrenteOrigem, ContaCorrente contaCorrenteDestino,
            TipoLancamento tipoTransacao, decimal valor, CancellationToken cancellationToken)
        {
            var autenticacaoBancariaId = Guid.NewGuid();
            var lancamentos = new[]
            {
                new Lancamento
                {
                    IdentificadorLancamento = Guid.NewGuid(),
                    IdentificadorTransacao = autenticacaoBancariaId,
                    IdentificadorConta = contaCorrenteOrigem.IdentificadorConta,
                    DataLancamento = DateTimeOffset.UtcNow,
                    TipoLancamento = TipoLancamento.TransferenciaEntreContas,
                    TipoTransacao = TipoTransacao.Debito,
                    Valor = valor
                },
                new Lancamento
                {
                    IdentificadorLancamento = Guid.NewGuid(),
                    IdentificadorTransacao = autenticacaoBancariaId,
                    IdentificadorConta = contaCorrenteDestino.IdentificadorConta,
                    DataLancamento = DateTimeOffset.UtcNow,
                    TipoLancamento = TipoLancamento.TransferenciaEntreContas,
                    TipoTransacao = TipoTransacao.Credito,
                    Valor = valor
                }
            };

            var lancamentosRealizados = await _lancamentoRepositorio.SalvarLancamentosAsync(lancamentos);

            if (!lancamentosRealizados)
            {
                throw new LancamentosNaoRealizadosException("Ocorreu um erro ao efetuar a transação.");
            }

            var saldoContaOrigem =
                await _lancamentoRepositorio.ObterSaldoContaAsync(contaCorrenteOrigem.IdentificadorConta);
            var saldoContaDestino =
                await _lancamentoRepositorio.ObterSaldoContaAsync(contaCorrenteDestino.IdentificadorConta);

            contaCorrenteOrigem.AtualizarSaldo(saldoContaOrigem);
            contaCorrenteDestino.AtualizarSaldo(saldoContaDestino);

            var contasAtualizadas = await _contaCorrenteRepositorio.AtualizarSaldoContasAsync(new[]
            {
                contaCorrenteOrigem,
                contaCorrenteDestino
            }, cancellationToken);

            if (!contasAtualizadas)
            {
                throw new ApplicationException("Ocorreu um erro ao atualizar o saldo da conta.");
            }

            return autenticacaoBancariaId;
        }
    }
}
