using AgendaMais.Domain.Entities;
using AgendaMais.Infra.Context;
using AgendaMais.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgendaMais.Infra.Repositories;

public class ServicoRepository : IServicoRepository
{
    private readonly ApplicationDbContext _context;

    public ServicoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Servico>> GetAllAsync(int estabelecimentoId)
    {
       return await _context
           .Servicos
           .Include(x => x.Estabelecimento)
           .AsNoTracking()
           .Where(x => x.EstabelecimentoId == estabelecimentoId)
           .ToListAsync();
    }

    public async Task<Servico?> GetByIdAsync(int id)
    {
       return await _context
           .Servicos
           .Include(x => x.Estabelecimento)
           .AsNoTracking()
           .FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task<Servico?> AddAsync(Servico entity)
    {
        var resultadoCadastro = _context.AddAsync(entity).Result.Entity;
        await _context.SaveChangesAsync();

        return resultadoCadastro;
    }

    public async Task<Servico?> UpdateAsync(int id, Servico entity)
    {
        entity.Id = id;
        _context.Servicos.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Servicos
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        if (entity == null)
            return false;
        
        _context.Servicos.Remove(entity);
        await _context.SaveChangesAsync();
        
        return true;
    }
}