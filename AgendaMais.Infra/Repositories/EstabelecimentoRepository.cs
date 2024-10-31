using AgendaMais.Domain.Entities;
using AgendaMais.Infra.Context;
using AgendaMais.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgendaMais.Infra.Repositories;

public class EstabelecimentoRepository : IEstabelecimentoRepository
{
    private readonly ApplicationDbContext _context;

    public EstabelecimentoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Estabelecimento>> GetAllAsync()
    {
       return await _context
           .Estabelecimentos
           .AsNoTracking()
           .ToListAsync();
    }

    public async Task<Estabelecimento?> GetByIdAsync(int id)
    {
       return await _context
           .Estabelecimentos
           .AsNoTracking()
           .FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task<Estabelecimento?> AddAsync(Estabelecimento entity)
    {
        var resultadoCadastro = _context.AddAsync(entity).Result.Entity;
        await _context.SaveChangesAsync();

        return resultadoCadastro;
    }

    public async Task<Estabelecimento?> UpdateAsync(int id, Estabelecimento entity)
    {
        entity.Id = id;
        _context.Estabelecimentos.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Estabelecimentos
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        if (entity == null)
            return false;
        
        _context.Estabelecimentos.Remove(entity);
        await _context.SaveChangesAsync();
        
        return true;
    }
}