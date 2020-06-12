using AutoMapper;
using MediatR;
using SuperDigital.Conta.Dominio.Entidades;
using SuperDigital.Conta.Dominio.Excecoes;
using SuperDigital.Conta.Dominio.Repositorios;
using System.Threading;
using System.Threading.Tasks;

namespace SuperDigital.Conta.Applicacao.Requests.CriarCliente
{
    public class CriarClienteCommandHandler : IRequestHandler<CriarClienteCommand, CriarClienteCommandResult>
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IMapper _mapper;

        public CriarClienteCommandHandler(IClienteRepositorio clienteRepositorio, IMapper mapper)
        {
            _clienteRepositorio = clienteRepositorio;
            _mapper = mapper;
        }

        public async Task<CriarClienteCommandResult> Handle(CriarClienteCommand request, CancellationToken cancellationToken)
        {
            var clienteExistente = await _clienteRepositorio.ObterClientePorDocumentoAsync(request.Documento, cancellationToken);

            if (clienteExistente != null)
            {
                throw new ClienteJaCadastradoException("Cliente já cadastrado.");
            }

            var cliente = _mapper.Map<Cliente>(request);

            var result = await _clienteRepositorio.CriarClienteAsync(cliente, cancellationToken);

            return new CriarClienteCommandResult
            {
                Documento = result.Documento,
                Nome = result.Nome
            };
        }
    }
}
