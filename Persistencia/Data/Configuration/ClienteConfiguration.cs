

using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class ClienteConfiguration: IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("cliente");

        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id);

        builder.Property(d => d.NombreCliente)
        .HasColumnName("nombre_cliente")
        .HasColumnType("varchar")
        .IsRequired()
        .HasMaxLength(250);

        builder.Property(d => d.NombreContacto)
        .HasColumnName("nombre_contacto")
        .HasColumnType("varchar")
        .HasMaxLength(30);

        builder.Property(d => d.ApellidoContacto)
        .HasColumnName("apellido_contacto")
        .HasColumnType("varchar")
        .HasMaxLength(30);

        builder.Property(d => d.Telefono)
        .HasColumnName("telefono")
        .HasColumnType("varchar")
        .HasMaxLength(15)
        .IsRequired();

        builder.Property(d => d.Fax)
        .HasColumnName("fax")
        .HasColumnType("varchar")
        .HasMaxLength(15)
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
        
        builder.Property(d => d.Ciudad)
        .HasColumnName("ciudad")
        .HasColumnType("varchar")
        .HasMaxLength(50)
        .IsRequired();
        
        builder.Property(d => d.Region)
        .HasColumnName("region")
        .HasColumnType("varchar")
        .HasMaxLength(50);
        
        builder.Property(d => d.Pais)
        .HasColumnName("pais")
        .HasColumnType("varchar")
        .HasMaxLength(50);
        
        builder.Property(d => d.CodigoPostal)
        .HasColumnName("codigo_postal")
        .HasColumnType("varchar")
        .HasMaxLength(50);
        
        builder.Property(d => d.LimiteCredito)
        .HasColumnName("limite_credito")
        .HasColumnType("decimal(15,2)");


        builder.HasOne(d => d.Empleado)
        .WithMany(d => d.Clientes)
        .HasForeignKey(d => d.CodigoEmpleadoRepVentas)
        .IsRequired(false);
    }
}