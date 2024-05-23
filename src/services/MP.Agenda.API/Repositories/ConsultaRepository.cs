using Microsoft.EntityFrameworkCore;
using MP.Agenda.API.Data;
using MP.Agenda.API.Interfaces;
using MP.Agenda.API.Models;
using MP.Core.Data;

namespace MP.Agenda.API.Repositories
{
    public class ConsultaRepository : IConsultaRepository
    {
        private readonly AgendaDbContext _context;

        public ConsultaRepository(AgendaDbContext context)
        {
            _context = context;
        }
        public IUnitOfWork UnitOfWork => _context;


        public async Task<IEnumerable<Consulta>> ObterTodas()
        {
           return await _context.Consultas.AsNoTracking().ToListAsync();
        }


        public async Task<Consulta> ObterPorId(Guid id)
        {
            return await _context.Consultas.FindAsync(id);
        }


        public void Adicionar(Consulta consulta)
        {
            _context.Consultas.Add(consulta);
        }

        public void Atualizar(Consulta consulta)
        {
            _context.Consultas.Update(consulta);
        }   


        public void Dispose()
        {
            _context?.Dispose();

        }
    }
}
