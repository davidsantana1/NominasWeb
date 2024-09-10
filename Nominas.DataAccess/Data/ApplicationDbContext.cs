using Microsoft.EntityFrameworkCore;
using Nominas.Models;

namespace Nominas.DataAccess.Data;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Departamento>()
			.HasMany(d => d.Empleados)
			.WithOne(e => e.Departamento)
			.HasForeignKey(e => e.DepartamentoId)
			.OnDelete(DeleteBehavior.Cascade);

		modelBuilder.Entity<Departamento>()
			.HasOne(d => d.ResponsableDeArea)
			.WithOne()
			.HasForeignKey<Departamento>(d => d.ResponsableDeAreaId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<RegistroTransaccion>()
			.HasOne(rt => rt.Empleado)
			.WithMany(e => e.RegistroTransacciones)
			.HasForeignKey(rt => rt.EmpleadoId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<Puesto>().HasData(
			new Puesto { Id = 1, Nombre = "Desarrollador", NivelDeRiesgo = "Medio", NivelMinimoSalario = 30000, NivelMaximoSalario = 60000 },
			new Puesto { Id = 2, Nombre = "Analista", NivelDeRiesgo = "Bajo", NivelMinimoSalario = 25000, NivelMaximoSalario = 50000 }
		);

		// Seed data for Empleados
		modelBuilder.Entity<Empleado>().HasData(
			new Empleado { Id = 1, Cedula = "001-1234567-8", Nombre = "Juan Pérez", DepartamentoId = 1, PuestoId = 1, SalarioMensual = 35000, NominaId = 1 },
			new Empleado { Id = 2, Cedula = "002-2345678-9", Nombre = "Ana Gómez", DepartamentoId = 2, PuestoId = 2, SalarioMensual = 28000, NominaId = 1 }
		);

		// Seed data for Departamentos
		modelBuilder.Entity<Departamento>().HasData(
			new Departamento { Id = 1, Nombre = "IT", UbicacionFisica = "Edificio A", ResponsableDeAreaId = 1 },
			new Departamento { Id = 2, Nombre = "Recursos Humanos", UbicacionFisica = "Edificio B", ResponsableDeAreaId = 2 }
		);

		modelBuilder.Entity<RegistroTransaccion>().HasData(
	new RegistroTransaccion
	{
		Id = 1,
		EmpleadoId = 1, // Asegúrate de que este ID exista en la tabla Empleados
		Ingreso = 1000,
		Deduccion = 200,
		TipoTransaccion = "Ingreso",
		Fecha = new DateTime(2024, 9, 1),
		Monto = 800,
		Estado = "Aprobado"
	},
	new RegistroTransaccion
	{
		Id = 2,
		EmpleadoId = 2, // Asegúrate de que este ID exista en la tabla Empleados
		Ingreso = 1200,
		Deduccion = 150,
		TipoTransaccion = "Bono",
		Fecha = new DateTime(2024, 9, 5),
		Monto = 1050,
		Estado = "Aprobado"
	},
	new RegistroTransaccion
	{
		Id = 3,
		EmpleadoId = 1, // Asegúrate de que este ID exista en la tabla Empleados
		Ingreso = 500,
		Deduccion = 50,
		TipoTransaccion = "Horas Extras",
		Fecha = new DateTime(2024, 9, 10),
		Monto = 450,
		Estado = "Pendiente"
	}
);

		base.OnModelCreating(modelBuilder);
	}

	public DbSet<Departamento> Departamentos { get; set; }
	public DbSet<Empleado> Empleados { get; set; }
	public DbSet<Puesto> Puestos { get; set; }
	public DbSet<RegistroTransaccion> RegistroTransacciones { get; set; }
	public DbSet<TipoDeDeduccion> TiposDeDeducciones { get; set; }
	public DbSet<TipoDeIngreso> TiposDeIngreso { get; set; }
}
