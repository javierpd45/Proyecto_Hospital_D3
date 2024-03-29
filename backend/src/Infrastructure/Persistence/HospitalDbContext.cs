using System.Runtime.CompilerServices;
using Hospital.Domain;
using Hospital.Domain.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Hospital.Infrastructure.Persistence;

public class HospitalDbContext : IdentityDbContext {//IdentityDbContext<Usuario> solo si usamos IdentityUser como clase Padre en la clase Usuario
    public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options){}

    //----------------------------------------------------------------------------------------------------------------
    /*
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var userName = "system";

        foreach (var entry in ChangeTracker.Entries<BaseDomainModel>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.CreatedBy = userName;
                    break;
                
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.Now;
                    entry.Entity.LastModifiedBy = userName;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);

    }
    */
    //----------------------------------------------------------------------------------------------------------------

    protected void OModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //builder.Entity<Perfil>().HasKey(p => p.PerfillID);

        builder.Entity<Perfil>() //Relacion uno a muchos Perfiles - Usuarios
            .HasMany(u => u.Usuarios)
            .WithOne(p => p.Perfil)
            .HasForeignKey(p => p.PerfilID)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade); //Cuando no se quiere que se borre la lista de usuarios se utiliza .Restrict
            //En este caso utilizamos .Cascade para eliminar todos los usuarios relacionados a un perfil

        /*
        builder.Entity<Usuario>() //Relacion uno a muchos Usuarios - Ingresos_y_Altas
            .HasMany(ia => ia.Ingresos_y_Altas)
            .WithOne(u => u.Usuario)
            .HasForeignKey(u => u.ID_Doctor)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        */

        builder.Entity<Usuario>() //Relacion uno a muchos Usuarios - Procedimientos
            .HasMany(p => p.Procedimientos)
            .WithOne(u => u.Usuario)
            .HasForeignKey(u => u.UsuarioID)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Paciente>() //Relacion uno a muchos Pacientes - Ingresos_y_Altas
            .HasMany(ia => ia.IngresoAltas)
            .WithOne(p => p.Paciente)
            .HasForeignKey(p => p.PacienteCedula)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Paciente>() //Relacion uno a muchos Pacientes - Procedimientos
            .HasMany(pr => pr.Procedimientos)
            .WithOne(p => p.Paciente)
            .HasForeignKey(p => p.PacienteCedula)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Paciente>() //Relacion uno a muchos Pacientes - Analisis
            .HasMany(a => a.Analisis)
            .WithOne(p => p.Paciente)
            .HasForeignKey(p => p.PacienteCedula)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Paciente>() //Relacion uno a muchos Pacientes - Autorizaciones
            .HasMany(a => a.Autorizaciones)
            .WithOne(p => p.Paciente)
            .HasForeignKey(p => p.PacienteCedula)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Paciente>() //Relacion uno a muchos Pacientes - Cuentas
            .HasMany(c => c.Cuentas)
            .WithOne(p => p.Paciente)
            .HasForeignKey(p => p.PacienteCedula)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Cuenta>() //Relacion uno a muchos Cuentas - Transacciones
            .HasMany(t => t.Transacciones)
            .WithOne(c => c.Cuenta)
            .HasForeignKey(c => c.CuentaID)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Cuenta>() //Hace que le devuelva a la base de datos la descripcion de la enumeracion, en vez del valor entero
            .Property(e => e.Estado)
            .HasConversion(
                c => c.ToString(),
                c => (CuentaEstado)Enum.Parse(typeof(CuentaEstado), c)
            );

        //builder.Entity<Usuario>().Property(x => x.Id).HasMaxLength(36); //250)
        //builder.Entity<Usuario>().Property(x => x.NormalizedUserName).HasMaxLength(90);
        //Esto solo si utilizamos IdentityUser como clase Padre en alguna clase, en este caso en la clase Usuario
        //Como estamos usando la clase BaseDomainModel no haremos este paso (Clase 11. Trabajando en el DBContext)

        /*
        builder.Entity<Analisis>()
        .Ignore(a => a.LastModifiedBy)
        .Ignore(a => a.LastModifiedDate)
        .Ignore(a => a.CreatedDate)
        .Ignore(a => a.CreatedBy);
        */

    }

    public DbSet<Analisis> Analisis { get; set; }

    public DbSet<Autorizacion> Autorizaciones { get; set; }

    public DbSet<Cuenta> Cuentas { get; set; }

    public DbSet<IngresoAlta> Ingresos_y_Altas { get; set; }

    public DbSet<Paciente> Pacientes { get; set; }

    public DbSet<Perfil> Perfiles { get; set; }

    public DbSet<Procedimiento> Procedimientos { get; set; }

    public DbSet<Transaccion> Transacciones { get; set; }

    public DbSet<Usuario> Usuarios { get; set; }
}