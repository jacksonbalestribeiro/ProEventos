using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Context;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private IEventoService _eventoSevice;

        public EventosController(IEventoService eventoSevice)
        {
            _eventoSevice = eventoSevice;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _eventoSevice.GetAllEventosAsync(true);
                
                if(eventos == null) return NotFound("Nenhum evento encontrado!");

                return Ok(eventos);
            }
            catch (Exception ex)
            {   
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar eventos: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var evento = await _eventoSevice.GetEventoByIdAsync(id, true);
                
                if(evento == null) return NotFound("Nenhum evento encontrado!");

                return Ok(evento);
            }
            catch (Exception ex)
            {   
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar evento");
            }
        }

        [HttpGet("tema/{tema}")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var eventos = await _eventoSevice.GetAllEventosByTemaAsync(tema, true);
                
                if(eventos == null) return NotFound("Nenhum evento encontrado!");

                return Ok(eventos);
            }
            catch (Exception ex)
            {   
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar evento: " + ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddEvento(Evento evento)
        {
            try
            {
                var eventos = await _eventoSevice.AddEventos(evento);
                
                if(eventos == null) return BadRequest("Erro ao salvar evento!");

                return Ok(eventos);
            }
            catch (Exception ex)
            {   
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao salvar o evento");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvento(int id, [FromBody] Evento evento)
        {
            try
            {
                var eventos = await _eventoSevice.UpdateEventos(id, evento);
                
                if(eventos == null) return BadRequest("Erro ao salvar evento");

                return Ok(eventos);
            }
            catch (Exception ex)
            {   
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao salvar o evento: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            try
            {
                var evento = await _eventoSevice.DeleteEvento(id);
                
                if(evento == false) return BadRequest("Erro ao deletar evento!");

                return Ok();
            }
            catch (Exception ex)
            {   
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar o evento");
            }
        }

    }
}
