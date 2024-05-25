using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MP.Cliente.API.Models;

namespace MP.Cliente.API.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(u => u.Id);
            

            builder.Property(u => u.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.OwnsOne(u => u.Cpf, tf =>
            {
                tf.Property(c => c.Numero)
                    .IsRequired()
                    .HasMaxLength(11)
                    .HasColumnName("Cpf")
                    .HasColumnType("varchar(11)");
            });

            builder.Property(u => u.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType("varchar(200)");

            builder.HasOne(e => e.Endereco)
                .WithOne(u => u.Usuario);

            builder.ToTable("Clientes");
        }
    }
    
}
