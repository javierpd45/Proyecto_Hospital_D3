using System.ComponentModel.DataAnnotations.Schema;
using Hospital.Domain.Common;

namespace Hospital.Domain;

public class Paciente : BaseDomainModel{

    [Column(TypeName = "NVARCHAR(15)")]
    public string? PacienteCedula { get; set; }

    [Column(TypeName = "NVARCHAR(150)")]
    public string? Nombre { get; set; }

    [Column(TypeName = "NVARCHAR(150)")]
    public string? Apellido { get; set; }

    [Column(TypeName = "NVARCHAR(150)")]
    public string? Direccion { get; set; }

    [Column(TypeName = "NVARCHAR(150)")]
    public string? Correo { get; set; }

    [Column(TypeName = "NVARCHAR(15)")]
    public string? Telefono { get; set; }

    [Column(TypeName = "DATE")]
    public DateOnly? FechaNacimiento { get; set; }

    public virtual ICollection<Ingreso_y_Alta>? Ingresos_y_Altas { get; set; }

    public virtual ICollection<Procedimiento>? Procedimientos { get; set; }

    public virtual ICollection<Analisis>? Analisis { get; set; }

    public virtual ICollection<Autorizacion>? Autorizaciones { get; set; }

    public virtual ICollection<Cuenta>? Cuentas { get; set; }

    /*
    public Paciente(string pacienteCedula ,string nombre, string apellido, string direccion, string correo, string telefono, DateOnly fechaNacimiento){
        this.PacienteCedula = pacienteCedula;
        this.Nombre = nombre;
        this.Apellido = apellido;
        this.Direccion = direccion;
        this.Correo = correo;
        this.Telefono = telefono;
        this.FechaNacimiento = fechaNacimiento;
    }
    */
}