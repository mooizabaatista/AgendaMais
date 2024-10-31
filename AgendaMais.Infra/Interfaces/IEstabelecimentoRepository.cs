using AgendaMais.Domain.Entities;
using AgendaMais.Infra.Context;
using AgendaMais.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AgendaMais.Infra.Interfaces;

public interface IEstabelecimentoRepository
{
    Task<List<Estabelecimento>> GetAllAsync();
    Task<Estabelecimento?> GetByIdAsync(int id);
    Task <Estabelecimento?>AddAsync(Estabelecimento entity);
    Task<Estabelecimento?> UpdateAsync(int id, Estabelecimento entity);
    Task<bool> DeleteAsync(int id);
}