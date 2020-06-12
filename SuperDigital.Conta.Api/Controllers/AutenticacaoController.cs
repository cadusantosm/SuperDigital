using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SuperDigital.Conta.Applicacao.Requests.AutenticarCliente;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SuperDigital.Conta.Api.Contracts.Autenticar;
using SuperDigital.Conta.Api.SharedContracts;

namespace SuperDigital.Conta.Api.Controllers
{

    [ApiController]
    [Produces("application/json")]
    [Route("api/Autenticacao")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly byte[] _tokenSigningKey;

        public AutenticacaoController(IMediator mediator, IMapper mapper, IConfiguration configuration)
        {
            _mediator = mediator;
            _mapper = mapper;
            _tokenSigningKey = Encoding.ASCII.GetBytes(configuration["Authentication:Secret"]);
        }

        /// <summary>
        /// Realiza autenticação do cliente
        /// </summary>
        /// <param name="parameters">Dados do cliente</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Result), 200)]
        public async Task<ActionResult<AutenticarClienteResult>> PostAsync([FromBody] AutenticarClienteParameters parameters)
        {
            var request = _mapper.Map<AutenticarClienteCommand>(parameters);

            var response = await _mediator.Send(request);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.GroupSid, response.IdentificadorConta.ToString())
                }),

                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_tokenSigningKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            var result = new AutenticarClienteResult
            {
                Token = tokenString,
                Code = HttpStatusCode.OK,
                Success = true
            };

            return result;
        }
    }
}
