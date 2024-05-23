using Microsoft.EntityFrameworkCore;
using MP.Agenda.API.Models;
using MP.Core.Data;

namespace MP.Agenda.API.Data
{
    public class AgendaDbContext  : DbContext, IUnitOfWork
    {
        public AgendaDbContext(DbContextOptions<AgendaDbContext> options) : base(options) { }


        public DbSet<Consulta> Consultas { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AgendaDbContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }

    }
}
