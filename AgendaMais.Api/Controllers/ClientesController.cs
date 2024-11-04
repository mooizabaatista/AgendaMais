using AgendaMais.Application.Dtos.Cliente;
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
    public class ClientesController : ControllerBase
    {
        private IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet("GetAll/{estabelecimentoId:int}")]
        public async Task<IActionResult> GetAll(int estabelecimentoId)
        {
            try
            {
                var result = await _clienteService.GetAll(estabelecimentoId);

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
                var result = await _clienteService.Get(id);

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
        public async Task<IActionResult> Post(ClienteCommandDto clienteDto)
        {
            try
            {
                var result = await _clienteService.AddAsync(clienteDto);

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
        public async Task<IActionResult> Put(int id, ClienteCommandDto clienteDto)
        {
            try
            {
                var result = await _clienteService.UpdateAsync(id, clienteDto);

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
                try
                {
                    var result = await _clienteService.DeleteAsync(id);

                    if (result.EstaValido)
                        return Ok(result);

                    return BadRequest(result);
                }
                catch (Exception e)
                {
                    return BadRequest(e.InnerException?.Message ?? e.Message);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException?.Message ?? e.Message);
            }
        }
    }
}
