 
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class PagoConfiguration : IEntityTypeConfiguration<Pago>
{
    public void Configure(EntityTypeBuilder<Pago> builder)
    {
        builder.ToTable("pago");

        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id)
        .HasColumnName("id_transaccion")
        .IsRequired();

        builder.Property(d => d.FormaPAgo)
        .HasColumnName("forma_pago")
        .HasColumnType("varchar")
        .IsRequired()
        .HasMaxLength(40);

        builder.Property(d => d.FechaPAgo)
        .HasColumnName("fecha_pago")
        .HasColumnType("date")
        .IsRequired();

        builder.Property(d => d.Total)
        .HasColumnName("total")
        .HasColumnType("decimal(15,2)")
        .IsRequired();

        builder.HasOne(d => d.Cliente)
        .WithMany(d => d.Pagos)
        .HasForeignKey(d => d.CodigoCliente);
    }
}