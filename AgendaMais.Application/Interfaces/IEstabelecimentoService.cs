using AgendaMais.Application.Dtos.Estabelecimento;
using AgendaMais.Application.Dtos.Response;

namespace AgendaMais.Application.Interfaces;

public interface IEstabelecimentoService
{
    Task<ResponseDto> Get();
    Task<ResponseDto> Get(int id);
    Task<ResponseDto> AddAsync(EstabelecimentoCommandDto entity);
    Task<ResponseDto> UpdateAsync(int id, EstabelecimentoCommandDto entity);
    Task<ResponseDto> DeleteAsync(int id);
}