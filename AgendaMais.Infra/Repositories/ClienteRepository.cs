using AgendaMais.Domain.Entities;
using AgendaMais.Infra.Context;
using AgendaMais.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgendaMais.Infra.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly ApplicationDbContext _context;

    public ClienteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Cliente>> GetAllAsync(int estabelecimentoId)
    {
       return await _context
           .Clientes
           .Include(x => x.Estabelecimento)
           .AsNoTracking()
           .Where(x => x.EstabelecimentoId == estabelecimentoId)
           .ToListAsync();
    }

    public async Task<Cliente?> GetByIdAsync(int id)
    {
       return await _context
           .Clientes
           .Include(x => x.Estabelecimento)
           .AsNoTracking()
           .FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task<Cliente?> AddAsync(Cliente entity)
    {
        var resultadoCadastro = _context.AddAsync(entity).Result.Entity;
        await _context.SaveChangesAsync();

        return resultadoCadastro;
    }

    public async Task<Cliente?> UpdateAsync(int id, Cliente entity)
    {
        entity.Id = id;
        _context.Clientes.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Clientes
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        if (entity == null)
            return false;
        
        _context.Clientes.Remove(entity);
        await _context.SaveChangesAsync();
        
        return true;
    }
}