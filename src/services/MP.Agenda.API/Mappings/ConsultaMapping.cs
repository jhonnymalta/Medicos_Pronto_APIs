using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MP.Agenda.API.Models;

namespace MP.Agenda.API.Mappings
{
    public class ConsultaMapping : IEntityTypeConfiguration<Consulta>
    {
        public void Configure(EntityTypeBuilder<Consulta> builder)
        {
           builder.HasKey(c => c.Id);

           builder.Property(c => c.Data)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(c => c.Descricao)
                 .HasColumnType("varchar(250)");

            builder.Property(c => c.Confirmado)
                 .HasColumnType("Boolean");

            builder.Property(c => c.Finalizado)
                 .HasColumnType("Boolean");

            builder.Property(c => c.Cancelado)
                 .HasColumnType("Boolean");

            builder.ToTable("Consultas");

        }
    }

}
