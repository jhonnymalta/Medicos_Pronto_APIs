using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MP.Cliente.API.Models;

namespace MP.Cliente.API.Mappings
{
    public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Logradouro)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(u => u.Numero)
                .IsRequired()
                .HasColumnType("varchar(10)");

            builder.Property(u => u.Cep)
                .IsRequired()
                .HasColumnType("varchar(10)");

            builder.Property(u => u.Complemento)                
                .HasColumnType("varchar(250)");

            builder.Property(u => u.Bairro)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(u => u.Cidade)
                .IsRequired()
                .HasColumnType("varchar(50)");
            

            builder.ToTable("Enderecos");
        }
    }
    
}
