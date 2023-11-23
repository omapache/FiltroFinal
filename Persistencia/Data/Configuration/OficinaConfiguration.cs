using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class OficinaConfiguration : IEntityTypeConfiguration<Oficina> 
{
    public void Configure(EntityTypeBuilder<Oficina> builder)
    {

        builder.ToTable("Oficina");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.Ciudad)
        .HasColumnName("Ciudad")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();

        builder.Property(p => p.Region)
        .HasColumnName("Region")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();

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

        builder.Property(p => p.Telefono)
        .HasColumnName("Telefono")
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
        .HasMaxLength(255)
        .IsRequired();

    }
}