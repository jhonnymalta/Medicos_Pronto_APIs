using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MP.Cliente.API.Interfaces;
using MP.Cliente.API.Models;

namespace MP.Cliente.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/clientes")]
    public class ClientesController : Controller
    {
       private readonly IClienteRepository _clienteRepository;

        public ClientesController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [AllowAnonymous]
        [HttpGet("")]
        public async Task<IEnumerable<Usuario>> BuscarTodos()
        {
            return await _clienteRepository.ObterTodos();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<Usuario> BuscarCliente(Guid id)
        {
            return await _clienteRepository.ObterPorId(id);
        }
    }
}
