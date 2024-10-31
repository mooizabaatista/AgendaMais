using AgendaMais.Application.Dtos.Agendamento;
using AgendaMais.Application.Dtos.AgendamentoServico;
using AgendaMais.Application.Dtos.Response;
using AgendaMais.Application.Dtos.Servico;
using AgendaMais.Application.Interfaces;
using AgendaMais.Application.Validations;
using AgendaMais.Domain.Entities;
using AgendaMais.Infra.Interfaces;
using AutoMapper;
using FluentValidation.Results;

// ReSharper disable All

namespace AgendaMais.Application.Services;

public class AgendamentoService : IAgendamentoService
{
    private readonly IAgendamentoRepository _agendamentoRepository;
    private readonly IAgendamentoServicoRepository _agendamentoServicoRepository;
    private readonly IServicoService _servicoService;
    private readonly IMapper _mapper;
    private readonly ResponseDto _response;
    private readonly List<string> _errors;


    public AgendamentoService(IAgendamentoRepository agendamentoRepository,
        IAgendamentoServicoRepository agendamentoServicoRepository, IMapper mapper, IServicoService servicoService)
    {
        _agendamentoRepository = agendamentoRepository;
        _agendamentoServicoRepository = agendamentoServicoRepository;
        _mapper = mapper;
        _servicoService = servicoService;
        _response = new ResponseDto();
        _errors = new List<string>();
    }

    public async Task<ResponseDto> Get()
    {
        try
        {
            var agendamentosDto = new List<AgendamentoDto>();
            var agendamentos = await _agendamentoRepository.GetAllAsync();

            if (agendamentos.Count > 0)
            {
                var agendamentoDto = new AgendamentoDto();

                foreach (var agendamento in agendamentos)
                {
                    agendamentoDto = _mapper.Map<AgendamentoDto>(agendamento);

                    var servicos = await _agendamentoServicoRepository
                        .ObteServicosrPorAgendamentoid(agendamento.Id);

                    agendamentoDto.Servicos = _mapper.Map<List<ServicoDto>>(servicos);

                    agendamentosDto.Add(agendamentoDto);
                }

                _response.Resultado = agendamentosDto;
                _response.EstaValido = true;
                _response.Mensagem = "Dados obtidos com sucesso!";
                return _response;
            }
            else
            {
                _response.Mensagem = "Nenhum agendamento cadastrado";
                _response.EstaValido = true;
                return _response;
            }
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
            var agendamento = await _agendamentoRepository.GetByIdAsync(id);

            if (agendamento == null)
                _response.Mensagem = "Agendamento não localizado!";
            else
            {
                var agendamentoDto = new AgendamentoDto();

                agendamentoDto = _mapper.Map<AgendamentoDto>(agendamento);

                var servicos = await _agendamentoServicoRepository
                    .ObteServicosrPorAgendamentoid(agendamentoDto.Id);

                agendamentoDto.Servicos = _mapper.Map<List<ServicoDto>>(servicos);


                _response.Resultado = agendamentoDto;
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

    public async Task<ResponseDto> AddAsync(AgendamentoCommandDto agendamentoDto)
    {
        try
        {
            var agendamentoValidation = new AgendamentoValidation();

            var validationResult = agendamentoValidation.Validate(agendamentoDto);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    _errors.Add(error.ErrorMessage);
    
                _response.EstaValido = false;
                _response.Resultado = _errors;
                return _response;
            }
            
            // Verificar se a lista de serviços contém algo
            if (agendamentoDto.Servicos?.Count > 0)
            {
                // Incrementar no valor total o valor de cada serviço
                foreach (var servicoId in agendamentoDto.Servicos)
                {
                    var servicoExists = await _servicoService.Get(servicoId);
                    if (servicoExists.Resultado != null)
                    {
                        var servico = _mapper.Map<ServicoDto>(servicoExists.Resultado);
                        agendamentoDto.ValorTotal += servico.Preco;
                    }
                    else
                    {
                        _response.EstaValido = false;
                        _response.Mensagem = "Informe um serviço válido.";
                        return _response;
                    }
                }
            }
            else
            {
                _response.Mensagem = "Informe pelo menos 1 serviço";
                return _response;
            }

            // Cadastrar o agendamento
            var agendamentoEntity = _mapper.Map<Agendamento>(agendamentoDto);
            var resultCreate = await _agendamentoRepository.AddAsync(agendamentoEntity);

            if (resultCreate != null)
            {
                // Criar uma lista com os serviços para adicionar na tabela de junção
                var listaAgendamentoServicos = agendamentoDto.Servicos
                    .Select(servicoId => new AgendamentoServicoCommandDto()
                        { AgendamentoId = resultCreate.Id, ServicoId = servicoId })
                    .ToList();
                try
                {
                    // Cadastrar os dados na tabela de junção
                    await _agendamentoServicoRepository.AddRangeAsync(
                        _mapper.Map<List<AgendamentoServico>>(listaAgendamentoServicos));
                }
                catch (Exception e)
                {
                    await _agendamentoRepository.DeleteAsync(resultCreate.Id);
                }
            }
            else
                _response.Mensagem = "Falha ao cadastrar o agendamento.";

            _response.Resultado = _mapper.Map<AgendamentoDto>(resultCreate);
            _response.EstaValido = true;
            return _response;
        }
        catch (Exception e)
        {
            _response.Mensagem = e.Message;
            return _response;
        }
    }

    public async Task<ResponseDto> UpdateAsync(int id, AgendamentoCommandDto agendamentoDto)
    {
        try
        {
            // Localizar o agendamento para edição
            var agendamentoDtoToEdit = await _agendamentoRepository.GetByIdAsync(id);

            if (agendamentoDtoToEdit != null)
            {
                // Capturar todos os serviços vinculados na tabela de junção
                var servicosVinculados = await _agendamentoServicoRepository.ObterPorAgendamentoid(id);

                // Remove todos para adicionar corretamente os novos serviços
                if (servicosVinculados.Count > 0)
                {
                    await _agendamentoServicoRepository.RemoveRange(servicosVinculados);
                }

                // Lista que armazenará os dados que serão inseridos na tabela de junção
                var listaAgendamentoServicos = new List<AgendamentoServicoCommandDto>();
                
                // Zera-se o valor total para calcular novamente com os novos serviços
                agendamentoDto.ValorTotal = 0;
                
                if (agendamentoDto.Servicos != null)
                    foreach (var servicoId in agendamentoDto.Servicos)
                    {
                        var servico = await _servicoService.Get(servicoId);

                        if (servico.Resultado != null)
                        {
                            // Agendamento Serviço que será armazenado
                            var agendamentoServico = new AgendamentoServicoCommandDto()
                            {
                                AgendamentoId = id,
                                ServicoId = servicoId
                            };

                            // Adiciona na lista que será salva na tabela de junção
                            listaAgendamentoServicos.Add(agendamentoServico);

                            // Calcular o valor total
                            var servicoDto = _mapper.Map<ServicoDto>(servico.Resultado);
                            agendamentoDto.ValorTotal += servicoDto.Preco;
                        }
                        else
                            _response.Mensagem =
                                "Um dos serviços informados não existe, verifique os dados e tente novamente";
                    }

                // Adiciona os dados na tabela de junção
                await _agendamentoServicoRepository.AddRangeAsync(
                    _mapper.Map<List<AgendamentoServico>>(listaAgendamentoServicos));
                
                // Atualiza o agendamento
                await _agendamentoRepository.UpdateAsync(id, _mapper.Map<Agendamento>(agendamentoDto));

                _response.Resultado = agendamentoDto;
                _response.EstaValido = true;
                _response.Mensagem = "Agendamento atualizado com sucesso!";
            }
            else
            {
                _response.Mensagem = "Agendamento não localizado";
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
            var resultDelete = await _agendamentoRepository.DeleteAsync(id);

            _response.EstaValido = resultDelete;
            _response.Resultado = resultDelete;
            _response.Mensagem = resultDelete
                ? "Agendamento excluido com sucesso!"
                : "Falha ao excluir o agendamento do sistema.";

            return _response;
        }
        catch (Exception e)
        {
            _response.Mensagem = e.Message;
            return _response;
        }
    }
}