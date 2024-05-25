using Microsoft.EntityFrameworkCore;
using MP.Cliente.API.Data;
using MP.Cliente.API.Interfaces;
using MP.Cliente.API.Models;

namespace MP.Cliente.API.Repositories
{
    public class CriarClienteFromRabbitMQRepository : ICriarClienteFromRabbitMQRepository
    {
        private readonly DbContextOptions<ClienteDbContext> _context;
        
        public CriarClienteFromRabbitMQRepository(DbContextOptions<ClienteDbContext> context)
        {
            _context = context;
        }
        public async void Adicionar(Usuario usuario)
        {
            
            await using var _db = new ClienteDbContext(_context);
            _db.Add(usuario);
            
            
        }
    }

}
