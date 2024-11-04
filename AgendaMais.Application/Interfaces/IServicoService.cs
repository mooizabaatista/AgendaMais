using AgendaMais.Application.Dtos.Servico;
using AgendaMais.Application.Dtos.Response;

namespace AgendaMais.Application.Interfaces;

public interface IServicoService
{
    Task<ResponseDto> GetAll(int estabelecimentoId);
    Task<ResponseDto> Get(int id);
    Task<ResponseDto> AddAsync(ServicoCommandDto entity);
    Task<ResponseDto> UpdateAsync(int id, ServicoCommandDto entity);
    Task<ResponseDto> DeleteAsync(int id);
}