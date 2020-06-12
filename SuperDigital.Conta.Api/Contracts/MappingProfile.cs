using System.Net;
using AutoMapper;
using SuperDigital.Conta.Api.Contracts.Autenticar;
using SuperDigital.Conta.Api.Contracts.CriarCliente;
using SuperDigital.Conta.Api.Contracts.CriarConta;
using SuperDigital.Conta.Api.Contracts.CriarLancamento;
using SuperDigital.Conta.Api.Contracts.ObterSaldoContaCorrente;
using SuperDigital.Conta.Applicacao.Requests.AutenticarCliente;
using SuperDigital.Conta.Applicacao.Requests.CriarCliente;
using SuperDigital.Conta.Applicacao.Requests.CriarContaCorrente;
using SuperDigital.Conta.Applicacao.Requests.CriarLancamento;
using SuperDigital.Conta.Applicacao.Requests.ObterSaldoContaCorrente;

namespace SuperDigital.Conta.Api.Contracts
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CriarContaCorrenteParameters, CriarContaCorrenteCommand>();
            CreateMap<CriarContaCorrenteCommandResult, CriarContaCorrenteResult>()
                .ForMember(x => x.Code, x => x.MapFrom(opt => HttpStatusCode.Created))
                .ForMember(x => x.Message, x => x.Ignore())
                .ForMember(x => x.Success, x => x.Ignore())
                .ForMember(x => x.IdentificadorConta, x => x.Ignore());

            CreateMap<CriarLancamentoParameters, CriarLancamentoCommand>()
                .ForAllMembers(x => x.Ignore());

            CreateMap<CriarLancamentoCommandResult, CriarLancamentoResult>()
                .ForMember(x => x.Code, x => x.MapFrom(opt => HttpStatusCode.Created))
                .ForMember(x => x.Message, x => x.Ignore())
                .ForMember(x => x.Success, x => x.Ignore());

            CreateMap<CriarClienteParameters, CriarClienteCommand>();

            CreateMap<CriarClienteCommandResult, CriarClienteResult>()
                .ForMember(x => x.Code, x => x.MapFrom(opt => HttpStatusCode.Created))
                .ForMember(x => x.Code, x => x.MapFrom(opt => HttpStatusCode.Created))
                .ForMember(x => x.Message, x => x.Ignore())
                .ForMember(x => x.Success, x => x.Ignore());

            CreateMap<AutenticarClienteParameters, AutenticarClienteCommand>();
            CreateMap<AutenticarClienteCommandResult, AutenticarClienteResult>()
                .ForMember(x => x.Code, x => x.MapFrom(opt => HttpStatusCode.OK))
                .ForMember(x => x.Message, x => x.Ignore())
                .ForMember(x => x.Success, x => x.Ignore())
                .ForMember(x => x.Token, x => x.Ignore());

            CreateMap<ObterSaldoContaCorrenteCommandResult, ObterSaldoContaCorrenteResult>()
            .ForMember(x => x.Code, x => x.MapFrom(opt => HttpStatusCode.OK))
            .ForMember(x => x.Message, x => x.Ignore())
            .ForMember(x => x.Success, x => x.Ignore());

        }
    }
}
