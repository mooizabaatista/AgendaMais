using AgendaMais.Domain.Entities;
using AgendaMais.Infra.Context;
using AgendaMais.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AgendaMais.Infra.Interfaces;

public interface IServicoRepository
{
    Task<List<Servico>> GetAllAsync();
    Task<Servico?> GetByIdAsync(int id);
    Task <Servico?>AddAsync(Servico entity);
    Task<Servico?> UpdateAsync(int id, Servico entity);
    Task<bool> DeleteAsync(int id);
}