using MediatR;
using SuperDigital.Conta.Dominio.Entidades;
using SuperDigital.Conta.Dominio.Excecoes;
using SuperDigital.Conta.Dominio.Repositorios;
using SuperDigital.Conta.Dominio.Servicos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SuperDigital.Conta.Applicacao.Requests.CriarLancamento
{
    public class CriarLancamentoCommandHandler : IRequestHandler<CriarLancamentoCommand, CriarLancamentoCommandResult>
    {

        private readonly IContaCorrenteRepositorio _contaCorrenteRepositorio;
        private readonly ILancamentoServico _lancamentoServico;

        public CriarLancamentoCommandHandler(IContaCorrenteRepositorio contaCorrenteRepositorio, ILancamentoServico lancamentoServico)
        {
            _contaCorrenteRepositorio = contaCorrenteRepositorio;
            _lancamentoServico = lancamentoServico;
        }

        public async Task<CriarLancamentoCommandResult> Handle(CriarLancamentoCommand request, CancellationToken cancellationToken)
        {
            var contaOrigem = await _contaCorrenteRepositorio.ObterContaPorIdAsync(request.IdentificadorContaOrigem, cancellationToken);

            if (contaOrigem == null)
                throw new ContaCorrenteNaoEncontradaException("Conta origem não foi encontrada.");

            var contaDestino = await _contaCorrenteRepositorio.ObterContaPorDadosBancariosAsync(
                request.AgenciaFavorecido,
                request.NumeroContaFavorecido,
                request.DigitoVerificadorFavorecido,
                request.NumeroDocumentoFavorecido,
                request.TipoContaFavorecido,
                cancellationToken);

            if (contaDestino == null)
                throw new ContaCorrenteNaoEncontradaException("Conta destino não foi encontrada.");

            if (contaOrigem.ToString() == contaDestino.ToString())
                throw new ContaOrigemDeveSerDiferenteDaContaDestinoException("Conta origem deve ser diferente da conta destino.");

            if (contaOrigem.Saldo <= request.ValorLancamento)
                throw new ContaCorrenteSemSaldoParaEfetuarLancamento("Conta origem não tem saldo suficiente para realizar o lançamento.");

            var autenticacaoBancaria = await _lancamentoServico.ExecutarTransacaoAsync(contaOrigem, contaDestino,
                TipoLancamento.TransferenciaEntreContas, request.ValorLancamento, cancellationToken);

            return new CriarLancamentoCommandResult
            {
                AuthenticacaoBancaria = autenticacaoBancaria,
                Valor = request.ValorLancamento,
                TipoTransacao = TipoLancamento.TransferenciaEntreContas,
                TipoConta = TipoConta.Corrente,
                NumeroDocumento = contaDestino.NumeroDocumento,
                ContaOrigem = contaOrigem.ToString(),
                ContaDestino = contaDestino.ToString(),
                ContaDestinoNome = contaDestino.NomeLegal,
                ContaOrigemNome = contaOrigem.NomeLegal,
                DataDaTransacao = DateTimeOffset.UtcNow
            };
        }
    }
}
