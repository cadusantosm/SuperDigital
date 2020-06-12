using AutoMapper;
using SuperDigital.Conta.Dominio.Entidades;
using System;

namespace SuperDigital.Conta.Applicacao.Requests.CriarLancamento
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CriarLancamentoCommand, Lancamento>()
                .ForMember(x => x.IdentificadorConta, x => x.MapFrom(opt => Guid.NewGuid()))
                .ForMember(x => x.Id, x => x.Ignore())
                .ForMember(x => x.IdentificadorLancamento, x => x.Ignore())
                .ForMember(x => x.IdentificadorTransacao, x => x.Ignore())
                .ForMember(x => x.Valor, x => x.Ignore())
                .ForMember(x => x.TipoTransacao, x => x.Ignore())
                .ForMember(x => x.DataLancamento, x => x.Ignore())
                .ForMember(x => x.TipoLancamento, x => x.Ignore());

        }
    }
}
