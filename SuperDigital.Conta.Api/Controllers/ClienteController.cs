using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperDigital.Conta.Applicacao.Requests.CriarCliente;
using System.Threading.Tasks;
using SuperDigital.Conta.Api.Contracts.CriarCliente;
using SuperDigital.Conta.Api.SharedContracts;

namespace SuperDigital.Conta.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/Cliente")]
    public class ClienteController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ClienteController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Realiza cadastro de um novo cliente
        /// </summary>
        /// <param name="parameters">Dados do cliente</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Result), 201)]
        public async Task<ActionResult<CriarClienteResult>> PostAsync([FromBody] CriarClienteParameters parameters)
        {
            var request = _mapper.Map<CriarClienteCommand>(parameters);

            var response = await _mediator.Send(request);

            return _mapper.Map<CriarClienteResult>(response);
        }
    }
}
