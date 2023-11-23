 
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class OficinaConfiguration : IEntityTypeConfiguration<Oficina>
{
    public void Configure(EntityTypeBuilder<Oficina> builder)
    {
        builder.ToTable("oficina");

        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id)
        .HasColumnName("codigo_oficina")
        .IsRequired();

        builder.Property(d => d.Ciudad)
        .HasColumnName("ciudad")
        .HasColumnType("varchar")
        .IsRequired()
        .HasMaxLength(30);

        builder.Property(d => d.Pais)
        .HasColumnName("pais")
        .HasColumnType("varchar")
        .HasMaxLength(50)
        .IsRequired();

        builder.Property(d => d.Region)
        .HasColumnName("region")
        .HasColumnType("varchar")
        .HasMaxLength(50);

        builder.Property(d => d.CodigoPostal)
        .HasColumnName("codigo_postal")
        .HasColumnType("varchar")
        .HasMaxLength(10)
        .IsRequired();

        builder.Property(d => d.Telefono)
        .HasColumnName("telefono")
        .HasColumnType("varchar")
        .HasMaxLength(20)
        .IsRequired();

        builder.Property(d => d.LineaDireccion1)
        .HasColumnName("linea_direccion1")
        .HasColumnType("varchar")
        .HasMaxLength(50)
        .IsRequired();

        builder.Property(d => d.LineaDireccion2)
        .HasColumnName("linea_direccion2")
        .HasColumnType("varchar")
        .HasMaxLength(50);
    }
}