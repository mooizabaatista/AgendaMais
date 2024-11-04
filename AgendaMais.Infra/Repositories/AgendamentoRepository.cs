using AgendaMais.Domain.Entities;
using AgendaMais.Infra.Context;
using AgendaMais.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgendaMais.Infra.Repositories;

public class AgendamentoRepository : IAgendamentoRepository
{
    private readonly ApplicationDbContext _context;

    public AgendamentoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Agendamento>> GetAllAsync(int estabelecimentoId)
    {
       return await _context
           .Agendamentos
           .AsNoTracking()
           .Include(x => x.Cliente)
           .ThenInclude(x => x.Estabelecimento)
           .Where(x =>x.Cliente.EstabelecimentoId == estabelecimentoId)
           .ToListAsync();
    }

    public async Task<Agendamento?> GetByIdAsync(int id)
    {
       return await _context
           .Agendamentos
           .Include(x => x.Cliente)
           .ThenInclude(x => x.Estabelecimento)
           .AsNoTracking()
           .FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task<Agendamento?> AddAsync(Agendamento entity)
    {
        var resultadoCadastro = _context.AddAsync(entity).Result.Entity;
        await _context.SaveChangesAsync();

        return resultadoCadastro;
    }

    public async Task<Agendamento?> UpdateAsync(int id, Agendamento entity)
    {
        entity.Id = id;
        _context.Agendamentos.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Agendamentos
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        if (entity == null)
            return false;
        
        _context.Agendamentos.Remove(entity);
        await _context.SaveChangesAsync();
        
        return true;
    }
}