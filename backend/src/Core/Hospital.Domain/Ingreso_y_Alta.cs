using System.ComponentModel.DataAnnotations.Schema;
using Hospital.Domain.Common;

namespace Hospital.Domain;

public class Ingreso_y_Alta : BaseDomainModel {
    [Column(TypeName = "INT")]
    public int ID_Doctor { get; set; } //Llave foranea de la tabla Usuarios

    [Column(TypeName = "INT")]
    public int ID_Paciente { get; set; } //Llave foranea de la tabla Pacientes

    [Column(TypeName = "DATETIME")]
    public DateTime FechaIngreso { get; set; }

    [Column(TypeName = "DATETIME")]
    public DateTime FechaAlta { get; set; }

    [Column(TypeName = "NVARCHAR(250)")]
    public string MotivoIngreso { get; set; }

    public virtual Usuario? Usuario { get; set; }

    public virtual Paciente? Paciente { get; set; }

    public Ingreso_y_Alta(int id_Doctor, int id_Paciente, DateTime fechaIngreso, DateTime fechaAlta, string motivoIngreso){
        this.ID_Doctor = id_Doctor;
        this.ID_Paciente = id_Paciente;
        this.FechaIngreso = fechaIngreso;
        this.FechaAlta = fechaAlta;
        this.MotivoIngreso = motivoIngreso;
    }
}