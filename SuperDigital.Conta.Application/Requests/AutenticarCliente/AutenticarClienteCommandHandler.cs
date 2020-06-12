using AutoMapper;
using MediatR;
using SuperDigital.Conta.Dominio.Excecoes;
using SuperDigital.Conta.Dominio.Repositorios;
using SuperDigital.Conta.Infraestrutura.Util;
using System.Threading;
using System.Threading.Tasks;

namespace SuperDigital.Conta.Applicacao.Requests.AutenticarCliente
{
    public class AutenticarClienteCommandHandler : IRequestHandler<AutenticarClienteCommand, AutenticarClienteCommandResult>
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IContaCorrenteRepositorio _contaCorrenteRepositorio;
        private readonly IMapper _mapper;
        
        public AutenticarClienteCommandHandler(IClienteRepositorio clienteRepositorio, IMapper mapper, IContaCorrenteRepositorio contaCorrenteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
            _mapper = mapper;
            _contaCorrenteRepositorio = contaCorrenteRepositorio;
        }
        
        public async Task<AutenticarClienteCommandResult> Handle(AutenticarClienteCommand request, CancellationToken cancellationToken)
        {
            var senhaCriptografada = request.Senha.Criptografar();

            var cliente = await
                _clienteRepositorio.ObterClientePorCredenciaisAsync(request.Documento, senhaCriptografada,
                    cancellationToken);

            if (cliente == null)
            {
                throw new ClienteNaoAutenticadoException("Usuário ou senha incorreto.");
            }

            var contaCorrente = await _contaCorrenteRepositorio.ObterContaPorDocumentoAsync(cliente.Documento, cancellationToken);

            if (contaCorrente == null)
            {
                throw new ClienteNaoPossuiContaCorrenteException("Cliente não possuí conta corrente, favor cadastrar.");
            }

            return new AutenticarClienteCommandResult
            {
                IdentificadorConta = contaCorrente.IdentificadorConta,
            };
        }
    }
}
