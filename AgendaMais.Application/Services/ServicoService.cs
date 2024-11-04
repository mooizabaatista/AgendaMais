using AgendaMais.Application.Dtos.Servico;
using AgendaMais.Application.Dtos.Response;
using AgendaMais.Application.Interfaces;
using AgendaMais.Application.Validations;
using AgendaMais.Domain.Entities;
using AgendaMais.Infra.Interfaces;
using AutoMapper;
// ReSharper disable All

namespace AgendaMais.Application.Services;

public class ServicoService : IServicoService
{
    private readonly IServicoRepository _servicoRepository;
    private readonly IEstabelecimentoRepository _estabelecimentoRepository;
    private readonly IMapper _mapper;
    private readonly ResponseDto _response;
    private readonly List<string> _errors;

    public ServicoService(IServicoRepository servicoRepository, IMapper mapper,
        IEstabelecimentoRepository estabelecimentoRepository)
    {
        _servicoRepository = servicoRepository;
        _mapper = mapper;
        _estabelecimentoRepository = estabelecimentoRepository;
        _response = new ResponseDto();
        _errors = new List<string>();
    }

    public async Task<ResponseDto> GetAll(int estabelecimentoId)
    {
        try
        {
            var servicosEntity = await _servicoRepository.GetAllAsync(estabelecimentoId);
            var servicosDto = _mapper.Map<List<ServicoDto>>(servicosEntity);

            _response.Resultado = _mapper.Map<List<ServicoDto>>(servicosEntity);
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
            var servico = await _servicoRepository.GetByIdAsync(id);

            if (servico == null)
                _response.Mensagem = "Servico não localizado!";
            else
            {
                _response.Resultado = _mapper.Map<ServicoDto>(servico);
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

    public async Task<ResponseDto> AddAsync(ServicoCommandDto entity)
    {
        try
        {
            var servicoValidation = new ServicoValidation();

            var validationResult = servicoValidation.Validate(entity);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    _errors.Add(error.ErrorMessage);
    
                _response.EstaValido = false;
                _response.Resultado = _errors;
                return _response;
            }
            
            
            var servico = _mapper.Map<Servico>(entity);

            var estabelecimento = await _estabelecimentoRepository.GetByIdAsync(entity.EstabelecimentoId);

            if (estabelecimento == null)
            {
                _response.Mensagem = "Estabelecimento não localizado!";
            }
            else
            {
                var resultadoCadastro = await _servicoRepository.AddAsync(servico);

                if (resultadoCadastro != null)
                {
                    _response.Resultado = _mapper.Map<ServicoDto>(resultadoCadastro);
                    _response.Mensagem = "Servico adicionado com sucesso!";
                    _response.EstaValido = true;

                    return _response;
                }
                else
                    _response.Mensagem = "Falha ao cadastrar o Servico!";
            }

            return _response;
        }
        catch (Exception e)
        {
            _response.Mensagem = e.Message;
            return _response;
        }
    }

    public async Task<ResponseDto> UpdateAsync(int id, ServicoCommandDto entity)
    {
        try
        {
            var servicoValidation = new ServicoValidation();

            var validationResult = servicoValidation.Validate(entity);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    _errors.Add(error.ErrorMessage);
    
                _response.EstaValido = false;
                _response.Resultado = _errors;
                return _response;
            }
            
            var servico = await _servicoRepository.GetByIdAsync(id);

            if (servico == null)
                _response.Mensagem = "Servico não localizado!";
            else
            {
                var estabelecimento = await _estabelecimentoRepository.GetByIdAsync(entity.EstabelecimentoId);

                if (estabelecimento == null)
                {
                    _response.Mensagem = "Estabelecimento não localizado!";
                }
                else
                {
                    var entityToEdit = _mapper.Map<Servico>(entity);
                    await _servicoRepository.UpdateAsync(id, entityToEdit);

                    _response.Mensagem = "Servico atualizado com sucesso!";
                    _response.EstaValido = true;
                    _response.Resultado = _mapper.Map<ServicoDto>(entityToEdit);
                }
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
            var resultadoExclusao = await _servicoRepository.DeleteAsync(id);

            if (resultadoExclusao)
            {
                _response.Mensagem = "Servico removido com sucesso!";
                _response.EstaValido = true;
                _response.Resultado = true;
                return _response;
            }

            _response.Mensagem = "Falha ao excluir o Servico!";
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