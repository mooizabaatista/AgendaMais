using AgendaMais.Domain.Entities;
using AgendaMais.Infra.Context;
using AgendaMais.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AgendaMais.Infra.Interfaces;

public interface IAgendamentoRepository
{
    Task<List<Agendamento>> GetAllAsync(int estabelecimentoId);
    Task<Agendamento?> GetByIdAsync(int id);
    Task <Agendamento?>AddAsync(Agendamento entity);
    Task<Agendamento?> UpdateAsync(int id, Agendamento entity);
    Task<bool> DeleteAsync(int id);
}