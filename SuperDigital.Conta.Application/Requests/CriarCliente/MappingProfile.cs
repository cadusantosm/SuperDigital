using AutoMapper;
using SuperDigital.Conta.Dominio.Entidades;
using SuperDigital.Conta.Infraestrutura.Util;
using System;

namespace SuperDigital.Conta.Applicacao.Requests.CriarCliente
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CriarClienteCommand, Cliente>()
                .ForMember(x => x.Senha, x => x.MapFrom(opt => opt.Senha.Criptografar()))
                .ForMember(x => x.IdentificadorCliente, x => x.MapFrom(opt => Guid.NewGuid()));
        }
    }
}
