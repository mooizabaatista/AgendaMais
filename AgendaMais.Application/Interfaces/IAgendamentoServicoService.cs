using AgendaMais.Application.Dtos.AgendamentoServico;
using AgendaMais.Application.Dtos.Response;
using AgendaMais.Application.Dtos.Servico;

namespace AgendaMais.Application.Interfaces;

public interface IAgendamentoServicoService
{
    public Task<List<ServicoDto>> ObteServicosrPorAgendamentoid(int agendamentoId);
    public Task<List<AgendamentoServicoDto>> ObterPorAgendamentoid(int agendamentoId);
    public Task<AgendamentoServicoDto?> ObterPorAgendamentoIdServicoId(int agendamentoId, int servicoId);
    public Task<AgendamentoServicoDto> AddAsync(int agendamentoid, int servicoId);
    public Task AddRangeAsync(List<AgendamentoServicoCommandDto> agendamentoServicos);
    public Task RemoveRange(List<AgendamentoServicoCommandDto> agendamentoServicos);
}