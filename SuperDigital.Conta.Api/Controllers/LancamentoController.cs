using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperDigital.Conta.Applicacao.Requests.CriarLancamento;
using SuperDigital.Conta.Infraestrutura.Util;
using System.Threading.Tasks;
using SuperDigital.Conta.Api.Contracts.CriarLancamento;
using SuperDigital.Conta.Api.SharedContracts;

namespace SuperDigital.Conta.Api.Controllers
{

    [ApiController]
    [Produces("application/json")]
    [Route("api/Lancamento")]
    public class LancamentoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public LancamentoController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Realiza um lançamento entre contas (SOMENTE LANÇAMENTOS ENTRE CONTAS CORRENTES DISPONÍVEL)
        /// </summary>
        /// <param name="parameters">Dados para efetuar a lançamento entre contas</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(Result), 201)]
        public async Task<ActionResult<CriarLancamentoResult>> Post([FromBody] CriarLancamentoParameters parameters)
        {
            var request = _mapper.Map<CriarLancamentoCommand>(parameters);

            request.IdentificadorContaOrigem = User.ObterIdentificadorConta();

            var response = await _mediator.Send(request);

            return _mapper.Map<CriarLancamentoResult>(response);
        }
    }
}
