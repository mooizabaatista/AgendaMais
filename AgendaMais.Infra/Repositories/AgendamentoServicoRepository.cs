using AgendaMais.Domain.Entities;
using AgendaMais.Infra.Context;
using AgendaMais.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgendaMais.Infra.Repositories;

public class AgendamentoServicoRepository : IAgendamentoServicoRepository
{
    private readonly ApplicationDbContext _context;

    public AgendamentoServicoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<AgendamentoServico>> ObterPorAgendamentoid(int agendamentoId)
    {
        return await _context
            .AgendamentoServicos
            .Where(x => x.AgendamentoId == agendamentoId)
            .Include(x => x.Servico)
            .ThenInclude(x => x.Estabelecimento)
            .ToListAsync();
    }

    public async Task<List<Servico>> ObteServicosrPorAgendamentoid(int agendamentoId)
    {
        return await _context
            .AgendamentoServicos
            .Where(x => x.AgendamentoId == agendamentoId)
            .Include(x => x.Servico)
            .ThenInclude(x => x.Estabelecimento)
            .Select(x => x.Servico)
            .ToListAsync();
    }

    public async Task<AgendamentoServico?> ObterPorAgendamentoIdServicoId(int agendamentoId, int servicoId)
    {
        return await _context
            .AgendamentoServicos
            .Where(x => x.AgendamentoId == agendamentoId && x.ServicoId == servicoId)
            .FirstOrDefaultAsync();
    }

    public async Task<AgendamentoServico> AddAsync(int agendamentoid, int servicoId)
    {
        var result = new AgendamentoServico();
        
        result.AgendamentoId = agendamentoid;
        result.ServicoId = servicoId;
        
        await _context.AgendamentoServicos.AddAsync(result);
        await _context.SaveChangesAsync();
        
        return result;
    }

    public async Task AddRangeAsync(List<AgendamentoServico> agendamentoServicos)
    {
        await _context.AddRangeAsync(agendamentoServicos);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveRange(List<AgendamentoServico> agendamentoServicos)
    {
        _context.RemoveRange(agendamentoServicos);
        await _context.SaveChangesAsync();
    }
}