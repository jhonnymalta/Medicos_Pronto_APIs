using Microsoft.EntityFrameworkCore;
using MP.Cliente.API.Data;
using MP.Cliente.API.Interfaces;
using MP.Cliente.API.Models;
using System.ComponentModel.DataAnnotations;

namespace MP.Cliente.API.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ClienteDbContext _context;

        public ClienteRepository(ClienteDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> ObterTodos()
        {
            return await _context.Clientes.AsNoTracking().ToListAsync();
        }

        public async Task<Usuario> ObterPorId(Guid id)
        {
            return await _context.Clientes.FindAsync(id);
        }


        public void Adicionar(Usuario consulta)
        {
             _context.Clientes.Add(consulta);
            

        }

        public void Atualizar(Usuario consulta)
        {
            _context.Clientes.Update(consulta);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
