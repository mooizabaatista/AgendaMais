using AgendaMais.Application.Dtos.Cliente;
using AgendaMais.Application.Dtos.Response;
using AgendaMais.Application.Interfaces;
using AgendaMais.Application.Validations;
using AgendaMais.Domain.Entities;
using AgendaMais.Infra.Interfaces;
using AutoMapper;
// ReSharper disable All

namespace AgendaMais.Application.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IEstabelecimentoRepository _estabelecimentoRepository;
    private readonly IMapper _mapper;
    private readonly ResponseDto _response;
    private readonly List<string> _errors;

    public ClienteService(IClienteRepository clienteRepository, IMapper mapper,
        IEstabelecimentoRepository estabelecimentoRepository)
    {
        _clienteRepository = clienteRepository;
        _mapper = mapper;
        _estabelecimentoRepository = estabelecimentoRepository;
        _response = new ResponseDto();
        _errors = new List<string>();
    }

    public async Task<ResponseDto> GetAll(int estabelecimentoId)
    {
        try
        {
            var clientesEntity = await _clienteRepository.GetAllAsync(estabelecimentoId);
            var clientesDto = _mapper.Map<List<ClienteDto>>(clientesEntity);

            _response.Resultado = _mapper.Map<List<ClienteDto>>(clientesEntity);
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
            var cliente = await _clienteRepository.GetByIdAsync(id);

            if (cliente == null)
                _response.Mensagem = "Cliente não localizado!";
            else
            {
                _response.Resultado = _mapper.Map<ClienteDto>(cliente);
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

    public async Task<ResponseDto> AddAsync(ClienteCommandDto entity)
    {
        try
        {
            var clienteValidation = new ClienteValidation();

            var validationResult = clienteValidation.Validate(entity);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    _errors.Add(error.ErrorMessage);
    
                _response.EstaValido = false;
                _response.Resultado = _errors;
                return _response;
            }
            
            var cliente = _mapper.Map<Cliente>(entity);

            var estabelecimento = await _estabelecimentoRepository.GetByIdAsync(entity.EstabelecimentoId);

            if (estabelecimento == null)
            {
                _response.Mensagem = "Estabelecimento não localizado!";
            }
            else
            {
                var resultadoCadastro = await _clienteRepository.AddAsync(cliente);

                if (resultadoCadastro != null)
                {
                    _response.Resultado = _mapper.Map<ClienteDto>(resultadoCadastro);
                    _response.Mensagem = "Cliente adicionado com sucesso!";
                    _response.EstaValido = true;

                    return _response;
                }
                else
                    _response.Mensagem = "Falha ao cadastrar o Cliente!";
            }

            return _response;
        }
        catch (Exception e)
        {
            _response.Mensagem = e.Message;
            return _response;
        }
    }

    public async Task<ResponseDto> UpdateAsync(int id, ClienteCommandDto entity)
    {
        try
        {
            var clienteValidation = new ClienteValidation();

            var validationResult = clienteValidation.Validate(entity);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    _errors.Add(error.ErrorMessage);
    
                _response.EstaValido = false;
                _response.Resultado = _errors;
                return _response;
            }
            
            var cliente = await _clienteRepository.GetByIdAsync(id);

            if (cliente == null)
                _response.Mensagem = "Cliente não localizado!";
            else
            {
                var estabelecimento = await _estabelecimentoRepository.GetByIdAsync(entity.EstabelecimentoId);

                if (estabelecimento == null)
                {
                    _response.Mensagem = "Estabelecimento não localizado!";
                }
                else
                {
                    var entityToEdit = _mapper.Map<Cliente>(entity);
                    await _clienteRepository.UpdateAsync(id, entityToEdit);

                    _response.Mensagem = "Cliente atualizado com sucesso!";
                    _response.EstaValido = true;
                    _response.Resultado = _mapper.Map<ClienteDto>(entityToEdit);
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
            var resultadoExclusao = await _clienteRepository.DeleteAsync(id);

            if (resultadoExclusao)
            {
                _response.Mensagem = "Cliente removido com sucesso!";
                _response.EstaValido = true;
                _response.Resultado = true;
                return _response;
            }

            _response.Mensagem = "Falha ao excluir o Cliente!";
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