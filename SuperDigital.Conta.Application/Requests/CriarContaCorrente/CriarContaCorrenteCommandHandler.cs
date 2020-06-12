using AutoMapper;
using MediatR;
using SuperDigital.Conta.Dominio.Entidades;
using SuperDigital.Conta.Dominio.Excecoes;
using SuperDigital.Conta.Dominio.Repositorios;
using System.Threading;
using System.Threading.Tasks;

namespace SuperDigital.Conta.Applicacao.Requests.CriarContaCorrente
{
    public class CriarContaCorrenteCommandHandler : IRequestHandler<CriarContaCorrenteCommand, CriarContaCorrenteCommandResult>
    {
        private readonly IContaCorrenteRepositorio _contaCorrenteRepositorio;
        private readonly ILancamentoRepositorio _lancamentoRepositorio;
        private readonly IMapper _mapper;

        public CriarContaCorrenteCommandHandler(IContaCorrenteRepositorio contaCorrenteRepositorio, IMapper mapper, ILancamentoRepositorio lancamentoRepositorio)
        {
            _contaCorrenteRepositorio = contaCorrenteRepositorio;
            _mapper = mapper;
            _lancamentoRepositorio = lancamentoRepositorio;
        }

        public async Task<CriarContaCorrenteCommandResult> Handle(CriarContaCorrenteCommand request, CancellationToken cancellationToken)
        {
            var contaCorrente = _mapper.Map<ContaCorrente>(request);

            if (request.TipoConta != TipoConta.Corrente)
            {
                throw new PermitidoAbrirSomenteContaCorrenteException("É permitido abrir somente conta corrente no momento.");
            }

            var contaExistente = await _contaCorrenteRepositorio.ObterContaPorDadosBancariosAsync(request.Agencia,
                request.NumeroConta, request.DigitoVerificadorConta, request.NumeroDocumento, request.TipoConta, cancellationToken);

            if (contaExistente != null)
            {
                throw new ContaCorrenteJaExisteException("Conta corrente já existe.");
            }

            var conta = await _contaCorrenteRepositorio.CriarContaCorrenteAsync(contaCorrente, cancellationToken);

            if (conta == null)
            {
                throw new ErroParaAbrirContaCorrenteException("Ocorreu um erro ao abrir a conta corrente.");
            }

            var lancamentoInicial = await _lancamentoRepositorio.SalvarLancamentosAsync(new[]
              {
                new Lancamento().CriarLancamentoCredito(request.Valor,contaCorrente.IdentificadorConta)
            });

            if (!lancamentoInicial)
            {
                throw new LancamentosNaoRealizadosException("Um erro ocorreu ao tentar efetuar o lançamento inicial na conta.");
            }

            return new CriarContaCorrenteCommandResult
            {
                IdentificadorConta = conta.IdentificadorConta
            };
        }
    }
}
