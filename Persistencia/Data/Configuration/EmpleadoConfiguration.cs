using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class EmpleadoConfiguration : IEntityTypeConfiguration<Empleado> 
{
    public void Configure(EntityTypeBuilder<Empleado> builder)
    {

        builder.ToTable("Empleado");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.Nombre)
        .HasColumnName("Nombre")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();

         builder.Property(p => p.Apellido1)
        .HasColumnName("Apellido1")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();

         builder.Property(p => p.Apellido2)
        .HasColumnName("Apellido2")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();

         builder.Property(p => p.Extension)
        .HasColumnName("Extension")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();

         builder.Property(p => p.Puesto)
        .HasColumnName("Puesto")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();

        builder.HasOne(p => p.Oficina)
        .WithMany(p => p.Empleados)
        .HasForeignKey(p => p.IdOficinaFk);

        builder.HasOne(p => p.Jefe)
        .WithMany(p => p.Empleados)
        .HasForeignKey(p => p.IdJefeFk)
        .IsRequired(false);
    }
}
