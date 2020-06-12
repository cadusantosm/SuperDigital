using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperDigital.Conta.Applicacao.Requests.CriarContaCorrente;
using SuperDigital.Conta.Applicacao.Requests.ObterSaldoContaCorrente;
using SuperDigital.Conta.Infraestrutura.Util;
using System.Threading.Tasks;
using SuperDigital.Conta.Api.Contracts.CriarConta;
using SuperDigital.Conta.Api.Contracts.ObterSaldoContaCorrente;
using SuperDigital.Conta.Api.SharedContracts;

namespace SuperDigital.Conta.Api.Controllers
{


    [ApiController]
    [Produces("application/json")]
    [Route("api/ContaCorrente")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public ContaCorrenteController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Realiza cadastro de uma nova conta corrente
        /// </summary>
        /// <param name="parameters">Dados da conta corrente</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(Result), 201)]
        public async Task<ActionResult<CriarContaCorrenteResult>> PostAsync([FromBody] CriarContaCorrenteParameters parameters)
        {
            var request = _mapper.Map<CriarContaCorrenteCommand>(parameters);

            var response = await _mediator.Send(request);

            return _mapper.Map<CriarContaCorrenteResult>(response);
        }

        /// <summary>
        /// Obtém saldo de um cliente
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(Result), 200)]
        public async Task<ActionResult<ObterSaldoContaCorrenteResult>> ObterSaldoAsync()
        {
            var request = new ObterSaldoContaCorrenteCommand
            {
                IdentificadorConta = User.ObterIdentificadorConta()
            };

            var response = await _mediator.Send(request);

            return _mapper.Map<ObterSaldoContaCorrenteResult>(response);
        }
    }
}
