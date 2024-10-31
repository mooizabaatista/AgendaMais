using AgendaMais.Application.Dtos.AgendamentoServico;
using AgendaMais.Application.Dtos.Servico;
using AgendaMais.Application.Interfaces;
using AgendaMais.Domain.Entities;
using AgendaMais.Infra.Interfaces;
using AutoMapper;

namespace AgendaMais.Application.Services;

public class AgendamentoServicoService : IAgendamentoServicoService
{
    private readonly IAgendamentoServicoRepository _agendamentoServicoRepository;
    private readonly IMapper _mapper;

    public AgendamentoServicoService(IAgendamentoServicoRepository agendamentoServicoRepository, IMapper mapper)
    {
        _agendamentoServicoRepository = agendamentoServicoRepository;
        _mapper = mapper;
    }

    public async Task<List<ServicoDto>> ObteServicosrPorAgendamentoid(int agendamentoId)
    {
        var result = await _agendamentoServicoRepository.ObteServicosrPorAgendamentoid(agendamentoId);
        return _mapper.Map<List<ServicoDto>>(result);
    }

    public async Task<List<AgendamentoServicoDto>> ObterPorAgendamentoid(int agendamentoId)
    {
        var result = await _agendamentoServicoRepository.ObterPorAgendamentoid(agendamentoId);
        return _mapper.Map<List<AgendamentoServicoDto>>(result);
    }

    public async Task<AgendamentoServicoDto?> ObterPorAgendamentoIdServicoId(int agendamentoId, int servicoId)
    {
        var result = await _agendamentoServicoRepository.ObterPorAgendamentoIdServicoId(agendamentoId, servicoId);
        return _mapper.Map<AgendamentoServicoDto>(result);
    }

    public async Task<AgendamentoServicoDto> AddAsync(int agendamentoid, int servicoId)
    {
        var result = await _agendamentoServicoRepository.AddAsync(agendamentoid, servicoId);
        return _mapper.Map<AgendamentoServicoDto>(result);
    }

    public async Task AddRangeAsync(List<AgendamentoServicoCommandDto> agendamentoServicos)
    {
        await _agendamentoServicoRepository.AddRangeAsync(_mapper.Map<List<AgendamentoServico>>(agendamentoServicos));
    }

    public async Task RemoveRange(List<AgendamentoServicoCommandDto> agendamentoServicos)
    {
        await _agendamentoServicoRepository.RemoveRange(_mapper.Map<List<AgendamentoServico>>(agendamentoServicos));
    }
}