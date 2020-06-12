using MediatR;
using SuperDigital.Conta.Dominio.Excecoes;
using SuperDigital.Conta.Dominio.Repositorios;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace SuperDigital.Conta.Applicacao.Requests.ObterSaldoContaCorrente
{
    public class ObterSaldoContaCorrenteCommandHandler : IRequestHandler<ObterSaldoContaCorrenteCommand,
        ObterSaldoContaCorrenteCommandResult>
    {
        private readonly IContaCorrenteRepositorio _contaCorrenteRepositorio;

        public ObterSaldoContaCorrenteCommandHandler(IContaCorrenteRepositorio contaCorrenteRepositorio)
        {
            _contaCorrenteRepositorio = contaCorrenteRepositorio;
        }

        public async Task<ObterSaldoContaCorrenteCommandResult> Handle(ObterSaldoContaCorrenteCommand request, CancellationToken cancellationToken)
        {
            if (request.IdentificadorConta == default)
            {
                throw new ClienteNaoAutenticadoException("Você precisa se autenticar para visualizar seu saldo.");
            }

            var contaCorrente = await
                _contaCorrenteRepositorio.ObterContaPorIdAsync(request.IdentificadorConta, cancellationToken);

            return new ObterSaldoContaCorrenteCommandResult
            {
                Saldo = contaCorrente.Saldo,
                SaldoFormatado = contaCorrente.Saldo.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"))
            };
        }
    }
}