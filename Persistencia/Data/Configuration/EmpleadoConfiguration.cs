
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class EmpleadoConfiguration : IEntityTypeConfiguration<Empleado>
{
    public void Configure(EntityTypeBuilder<Empleado> builder)
    {
        builder.ToTable("empleado");

        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id);

        builder.Property(d => d.Nombre)
        .HasColumnName("nombre")
        .HasColumnType("varchar")
        .IsRequired()
        .HasMaxLength(50);

        builder.Property(d => d.Apellido1)
        .HasColumnName("apellido1")
        .HasColumnType("varchar")
        .HasMaxLength(50);

        builder.Property(d => d.Apellido2)
        .HasColumnName("apellido2")
        .HasColumnType("varchar")
        .HasMaxLength(50);

        builder.Property(d => d.Extension)
        .HasColumnName("extension")
        .HasColumnType("varchar")
        .HasMaxLength(10)
        .IsRequired();

        builder.Property(d => d.Email)
        .HasColumnName("email")
        .HasColumnType("varchar")
        .HasMaxLength(100)
        .IsRequired();

        builder.Property(d => d.Puesto)
        .HasColumnName("puesto")
        .HasColumnType("varchar")
        .HasMaxLength(50);

        builder.HasOne(d => d.Oficina)
        .WithMany(d => d.Empleados)
        .HasForeignKey(d => d.CodigoOficina)
        .IsRequired();

        builder.HasOne(d => d.Jefe)
        .WithMany(d => d.Empleados)
        .HasForeignKey(d => d.CodigoJefe)
        .IsRequired(false);
    }
}