﻿// <auto-generated />
using System;
using MP.Cliente.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MP.Cliente.API.Migrations
{
    [DbContext(typeof(ClienteDbContext))]
    [Migration("20240524233438_createClienteMapping")]
    partial class createClienteMapping
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MP.Cliente.API.Models.Endereco", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Complemento")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId")
                        .IsUnique();

                    b.ToTable("Enderecos", (string)null);
                });

            modelBuilder.Entity("MP.Cliente.API.Models.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasColumnName("Email");

                    b.Property<bool>("Excluido")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Clientes", (string)null);
                });

            modelBuilder.Entity("MP.Cliente.API.Models.Endereco", b =>
                {
                    b.HasOne("MP.Cliente.API.Models.Usuario", "Usuario")
                        .WithOne("Endereco")
                        .HasForeignKey("MP.Cliente.API.Models.Endereco", "UsuarioId")
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("MP.Cliente.API.Models.Usuario", b =>
                {
                    b.OwnsOne("MP.Core.ObjetosDeDominio.Cpf", "Cpf", b1 =>
                        {
                            b1.Property<Guid>("UsuarioId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Numero")
                                .IsRequired()
                                .HasMaxLength(11)
                                .HasColumnType("varchar(11)")
                                .HasColumnName("Cpf");

                            b1.HasKey("UsuarioId");

                            b1.ToTable("Clientes");

                            b1.WithOwner()
                                .HasForeignKey("UsuarioId");
                        });

                    b.Navigation("Cpf")
                        .IsRequired();
                });

            modelBuilder.Entity("MP.Cliente.API.Models.Usuario", b =>
                {
                    b.Navigation("Endereco")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
