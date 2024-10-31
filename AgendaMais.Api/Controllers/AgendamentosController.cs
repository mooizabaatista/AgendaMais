using AgendaMais.Application.Dtos.Agendamento;
using AgendaMais.Application.Dtos.AgendamentoServico;
using AgendaMais.Application.Dtos.Servico;
using AgendaMais.Application.Interfaces;
using AgendaMais.Domain.Entities;
using AgendaMais.Infra.Context;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgendaMais.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentosController : ControllerBase
    {
        private readonly IAgendamentoServicoService _agendamentoServicoService;
        private readonly  IAgendamentoService _agendamentoService;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AgendamentosController(ApplicationDbContext context, IMapper mapper, IAgendamentoServicoService agendamentoServicoService, IAgendamentoService agendamentoService)
        {
            _context = context;
            _mapper = mapper;
            _agendamentoServicoService = agendamentoServicoService;
            _agendamentoService = agendamentoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _agendamentoService.Get();
                if (result.EstaValido)
                    return Ok(result);
                
                return BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException?.Message ?? e.Message);
            }
        }

        [HttpGet("GetById/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _agendamentoService.Get(id);
                if (result.EstaValido)
                    return Ok(result);
                
                return BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException?.Message ?? e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(AgendamentoCommandDto agendamentoDto)
        {
            try
            {
                var result = await _agendamentoService.AddAsync(agendamentoDto);
                if (result.EstaValido)
                    return Ok(result);
                
                return BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException?.Message ?? e.Message);
            }
        }

        [HttpPut("Update/{id:int}")]
        public async Task<IActionResult> Put(int id, AgendamentoCommandDto agendamentoDto)
        {
            try
            {
                var result = await _agendamentoService.UpdateAsync(id, agendamentoDto);
                if (result.EstaValido)
                    return Ok(result);
                
                return BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException?.Message ?? e.Message);
            }
        }

        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Put(int id)
        {
            try
            {
                var result = await _agendamentoService.DeleteAsync(id);
                if (result.EstaValido)
                    return Ok(result);
                
                return BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException?.Message ?? e.Message);
            }
        }
    }
}