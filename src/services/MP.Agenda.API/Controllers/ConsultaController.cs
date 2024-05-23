using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MP.Agenda.API.Interfaces;
using MP.Agenda.API.Models;
using MP.Core.Data;

namespace MP.Agenda.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/agenda")]
    public class ConsultaController : Controller
    {
        private readonly IConsultaRepository _consultaRepository;

        public ConsultaController(IConsultaRepository consultaRepository)
        {
            _consultaRepository = consultaRepository;
        }

        [AllowAnonymous]
        [HttpGet("consutas")]
        public async Task<IEnumerable<Consulta>> BuscarTodas()
        {
           return await _consultaRepository.ObterTodas();
        }
        [ClaimsAuthorize("Agenda","Ler")]
        [HttpGet("consutas/{id}")]
        public async Task<Consulta> Consulta(Guid id)
        {
            return await _consultaRepository.ObterPorId(id);
        }
    }
}
