using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class PagoConfiguration : IEntityTypeConfiguration<Pago> 
{
    public void Configure(EntityTypeBuilder<Pago> builder)
    {

        builder.ToTable("Pago");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.FormaPago)
        .HasColumnName("FormaPago")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();

        builder.Property(p => p.FechaPago)
        .HasColumnName("FechaPago")
        .HasColumnType("Date")
        .IsRequired();

        builder.Property(p => p.Total)
        .HasColumnName("Total")
        .HasColumnType("decimal(18,10)")
        .IsRequired();

        builder.HasOne(p => p.Cliente)
        .WithMany(p => p.Pagos)
        .HasForeignKey(p => p.IdClienteFk);
    }
}