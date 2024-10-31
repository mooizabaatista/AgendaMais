using AgendaMais.Application.Dtos.Cliente;
using AgendaMais.Application.Dtos.Response;

namespace AgendaMais.Application.Interfaces;

public interface IClienteService
{
    Task<ResponseDto> Get();
    Task<ResponseDto> Get(int id);
    Task<ResponseDto> AddAsync(ClienteCommandDto entity);
    Task<ResponseDto> UpdateAsync(int id, ClienteCommandDto entity);
    Task<ResponseDto> DeleteAsync(int id);
}