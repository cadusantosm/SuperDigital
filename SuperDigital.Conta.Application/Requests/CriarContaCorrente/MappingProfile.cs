using AutoMapper;
using SuperDigital.Conta.Dominio.Entidades;
using System;

namespace SuperDigital.Conta.Applicacao.Requests.CriarContaCorrente
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CriarContaCorrenteCommand, ContaCorrente>()
                .ForMember(x => x.IdentificadorConta, x => x.MapFrom(opt => Guid.NewGuid()))
                .ForMember(x => x.Saldo, x => x.MapFrom(opt => opt.Valor))
                .ForMember(x => x.DataCriacao, x => x.Ignore())
                .ForMember(x => x.DataAlteracao, x => x.Ignore());
        }
    }
}
