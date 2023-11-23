using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class ClienteConfiguration : IEntityTypeConfiguration<Cliente> 
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {

        builder.ToTable("Cliente");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.NombreCliente)
        .HasColumnName("NombreCliente")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();

        builder.Property(p => p.NombreContacto)
        .HasColumnName("NombreContacto")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();

        builder.Property(p => p.ApellidoContacto)
        .HasColumnName("ApellidoContacto")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();

        builder.Property(p => p.Telefono)
        .HasColumnName("Telefono")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();

        builder.Property(p => p.Fax)
        .HasColumnName("Fax")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();

        builder.Property(p => p.LineaDireccion1)
        .HasColumnName("LineaDireccion1")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();
        
        builder.Property(p => p.LineaDireccion2)
        .HasColumnName("LineaDireccion2")
        .HasColumnType("varchar")
        .HasMaxLength(255);

        builder.Property(p => p.Ciudad)
        .HasColumnName("Ciudad")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();

        builder.Property(p => p.Region)
        .HasColumnName("Region")
        .HasColumnType("varchar")
        .HasMaxLength(255);

        builder.Property(p => p.Pais)
        .HasColumnName("Pais")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();

        builder.Property(p => p.CodigoPostal)
        .HasColumnName("CodigoPostal")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();

        builder.HasOne(p => p.Empleado)
        .WithMany(p => p.Clientes)
        .HasForeignKey(p => p.IdEmpleadoFk);

        builder.Property(p => p.LimiteCredito)
        .HasColumnName("LimiteCredito")
        .HasColumnType("decimal(18,10)")
        .IsRequired();

    }
}