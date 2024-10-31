using AgendaMais.Domain.Entities;

namespace AgendaMais.Infra.Interfaces;

public interface IAgendamentoServicoRepository
{
    public Task<List<AgendamentoServico>> ObterPorAgendamentoid(int agendamentoId);
    public Task<List<Servico>> ObteServicosrPorAgendamentoid(int agendamentoId);
    public Task<AgendamentoServico?> ObterPorAgendamentoIdServicoId(int agendamentoId, int servicoId);
    public Task<AgendamentoServico> AddAsync(int agendamentoid, int servicoId);
    public Task AddRangeAsync(List<AgendamentoServico> agendamentoServicos);
    public Task RemoveRange(List<AgendamentoServico> agendamentoServicos);
}