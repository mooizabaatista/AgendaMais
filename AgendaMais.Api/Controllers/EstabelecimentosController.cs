using AgendaMais.Application.Dtos.Estabelecimento;
using AgendaMais.Application.Interfaces;
using AgendaMais.Domain.Entities;
using AgendaMais.Infra.Context;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgendaMais.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstabelecimentosController : ControllerBase
    {
        private readonly IEstabelecimentoService _estabelecimentoService;
        
        public EstabelecimentosController(IEstabelecimentoService estabelecimentoService)
        {
            _estabelecimentoService = estabelecimentoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _estabelecimentoService.Get();

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
                var result = await _estabelecimentoService.Get(id);

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
        public async Task<IActionResult> Post(EstabelecimentoCommandDto estabelecimentoDto)
        {
            try
            {
                var result = await _estabelecimentoService.AddAsync(estabelecimentoDto);

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
        public async Task<IActionResult> Put(int id, EstabelecimentoCommandDto estabelecimentoDto)
        {
            try
            {
                var result = await _estabelecimentoService.UpdateAsync(id, estabelecimentoDto);

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
                var result = await _estabelecimentoService.DeleteAsync(id);

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
