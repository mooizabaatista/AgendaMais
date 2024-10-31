using AgendaMais.Application.Dtos.Estabelecimento;
using AgendaMais.Application.Dtos.Response;
using AgendaMais.Application.Interfaces;
using AgendaMais.Application.Validations;
using AgendaMais.Domain.Entities;
using AgendaMais.Infra.Interfaces;
using AutoMapper;
// ReSharper disable All

namespace AgendaMais.Application.Services;

public class EstabelecimentoService : IEstabelecimentoService
{
    private readonly IEstabelecimentoRepository _estabelecimentoRepository;
    private readonly IMapper _mapper;
    private readonly ResponseDto _response;
    private readonly List<string> _errors;
    
    public EstabelecimentoService(IEstabelecimentoRepository estabelecimentoRepository, IMapper mapper)
    {
        _estabelecimentoRepository = estabelecimentoRepository;
        _mapper = mapper;
        _response = new ResponseDto();
        _errors = new List<string>();
    }

    public async Task<ResponseDto> Get()
    {
        try
        {
            var estabelecimentosEntity = await _estabelecimentoRepository.GetAllAsync();
            var estabelecimentosDto = _mapper.Map<List<EstabelecimentoDto>>(estabelecimentosEntity);

            _response.Resultado = _mapper.Map<List<EstabelecimentoDto>>(estabelecimentosEntity);
            _response.Mensagem = "Dados obtidos com sucesso!";
            _response.EstaValido = true;

            return _response;
        }
        catch (Exception e)
        {
            _response.Mensagem = e.Message;
            return _response;
        }
    }

    public async Task<ResponseDto> Get(int id)
    {
        try
        {
            var estabelecimento = await _estabelecimentoRepository.GetByIdAsync(id);

            if (estabelecimento == null)
                _response.Mensagem = "Estabelecimento não localizado!";
            else
            {
                _response.Resultado = _mapper.Map<EstabelecimentoDto>(estabelecimento);
                _response.EstaValido = true;
                _response.Mensagem = "Dados obtidos com sucesso!";
            }

            return _response;
        }
        catch (Exception e)
        {
            _response.Mensagem = e.Message;
            return _response;
        }
    }

    public async Task<ResponseDto> AddAsync(EstabelecimentoCommandDto entity)
    {
        try
        {
            var estabelecimentoValidation = new EstabelecimentoValidation();

            var validationResult = estabelecimentoValidation.Validate(entity);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    _errors.Add(error.ErrorMessage);
    
                _response.EstaValido = false;
                _response.Resultado = _errors;
                return _response;
            }
            
            
            var estabelecimento = _mapper.Map<Estabelecimento>(entity);
            var resultadoCadastro = await _estabelecimentoRepository.AddAsync(estabelecimento);

            if (resultadoCadastro != null)
            {
                _response.Resultado = _mapper.Map<EstabelecimentoDto>(resultadoCadastro);
                _response.Mensagem = "Estabelecimento adicionado com sucesso!";
                _response.EstaValido = true;

                return _response;
            }
            else
                _response.Mensagem = "Falha ao cadastrar o Estabelecimento!";

            return _response;
        }
        catch (Exception e)
        {
            _response.Mensagem = e.Message;
            return _response;
        }
    }

    public async Task<ResponseDto> UpdateAsync(int id, EstabelecimentoCommandDto entity)
    {
        try
        {
            var estabelecimentoValidation = new EstabelecimentoValidation();

            var validationResult = estabelecimentoValidation.Validate(entity);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    _errors.Add(error.ErrorMessage);
    
                _response.EstaValido = false;
                _response.Resultado = _errors;
                return _response;
            }
            
            var estabelecimento = await _estabelecimentoRepository.GetByIdAsync(id);

            if (estabelecimento == null)
                _response.Mensagem = "Estabelecimento não localizado!";
            else
            {
                var entityToEdit = _mapper.Map<Estabelecimento>(entity);
                await _estabelecimentoRepository.UpdateAsync(id, entityToEdit);
            
                _response.Mensagem = "Estabelecimento atualizado com sucesso!";
                _response.EstaValido = true;
                _response.Resultado = _mapper.Map<EstabelecimentoDto>(entityToEdit);
            }

            return _response;
        }
        catch (Exception e)
        {
            _response.Mensagem = e.Message;
            return _response;
        }
    }

    public async Task<ResponseDto> DeleteAsync(int id)
    {
        try
        {
            var resultadoExclusao = await _estabelecimentoRepository.DeleteAsync(id);

            if (resultadoExclusao)
            {
                _response.Mensagem = "Estabelecimento removido com sucesso!";
                _response.EstaValido = true;
                _response.Resultado = true;
                return _response;
            }
            
            _response.Mensagem = "Falha ao excluir o Estabelecimento!";
            _response.Resultado = false;
            return _response;
        }
        catch (Exception e)
        {
            _response.Mensagem = e.Message;
            return _response;
        }
    }
}