using AgendaMais.Application.Dtos.Agendamento;
using AgendaMais.Application.Dtos.AgendamentoServico;
using AgendaMais.Application.Dtos.Cliente;
using AgendaMais.Application.Dtos.Estabelecimento;
using AgendaMais.Application.Dtos.Servico;
using AgendaMais.Domain.Entities;
using AutoMapper;
using AgendamentoCommandDto = AgendaMais.Application.Dtos.Agendamento.AgendamentoCommandDto;

namespace AgendaMais.Application.AutoMapper;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<Estabelecimento, EstabelecimentoDto>().ReverseMap();
        CreateMap<Estabelecimento, EstabelecimentoCommandDto>().ReverseMap();
        CreateMap<EstabelecimentoDto, EstabelecimentoCommandDto>().ReverseMap();
        
        CreateMap<Cliente, ClienteDto>().ReverseMap();
        CreateMap<Cliente, ClienteCommandDto>().ReverseMap();
        CreateMap<ClienteDto, ClienteCommandDto>().ReverseMap();
        
        CreateMap<Servico, ServicoDto>().ReverseMap();
        CreateMap<Servico, ServicoCommandDto>().ReverseMap();
        CreateMap<ServicoDto, ServicoCommandDto>().ReverseMap();
        
        CreateMap<Agendamento, AgendamentoDto>().ReverseMap();
        CreateMap<Agendamento, AgendamentoCommandDto>().ReverseMap();
        CreateMap<AgendamentoDto, AgendamentoCommandDto>().ReverseMap();
        
        CreateMap<AgendamentoServico, AgendamentoServicoDto>().ReverseMap();
        CreateMap<AgendamentoServico, AgendamentoServicoCommandDto>().ReverseMap();
        CreateMap<AgendamentoServicoDto, AgendamentoServicoCommandDto>().ReverseMap();
    }
}