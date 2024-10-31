using AgendaMais.Application.Dtos.Agendamento;
using AgendaMais.Application.Dtos.Response;

namespace AgendaMais.Application.Interfaces;

public interface IAgendamentoService
{
    Task<ResponseDto> Get();
    Task<ResponseDto> Get(int id);
    Task<ResponseDto> AddAsync(AgendamentoCommandDto agendamentoDto);
    Task<ResponseDto> UpdateAsync(int id, AgendamentoCommandDto agendamentoDto);
    Task<ResponseDto> DeleteAsync(int id);
}