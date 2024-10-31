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
    public class ServicosController : ControllerBase
    {
        private readonly IServicoService _servicoService;

        public ServicosController(IServicoService servicoService)
        {
            _servicoService = servicoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _servicoService.Get();

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
                var result = await _servicoService.Get(id);

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
        public async Task<IActionResult> Post(ServicoCommandDto servicoDto)
        {
            try
            {
                var result = await _servicoService.AddAsync(servicoDto);

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
        public async Task<IActionResult> Put(int id, ServicoCommandDto servicoDto)
        {
            try
            {
                var result = await _servicoService.UpdateAsync(id, servicoDto);

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
                var result = await _servicoService.DeleteAsync(id);

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