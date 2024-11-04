using AgendaMais.Domain.Entities;
using AgendaMais.Infra.Context;
using AgendaMais.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AgendaMais.Infra.Interfaces;

public interface IClienteRepository
{
    Task<List<Cliente>> GetAllAsync(int estabelecimentoId);
    Task<Cliente?> GetByIdAsync(int id);
    Task <Cliente?>AddAsync(Cliente entity);
    Task<Cliente?> UpdateAsync(int id, Cliente entity);
    Task<bool> DeleteAsync(int id);
}