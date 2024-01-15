using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hospital.Domain.Common;

namespace Hospital.Domain;

public class Procedimiento{
    //ID_Procedimiento sera heredado de BaseDomainModel

    [Key]
    public int ProcedimientoID { get; set; }

    [Column(TypeName = "NVARCHAR(15)")]
    public string? PacienteCedula { get; set; } //Llave foranea de la tabla Pacientes

    [Column(TypeName = "INT")]
    public int? MedicoResponsable { get; set; } //Llave foranea de la tabla Usuarios (representa el medico responsable del procedimiento)

    [Column(TypeName = "NVARCHAR(250)")]
    public string? Descripcion { get; set; } //Descripcion del procedimiento, sera heredado

    [Column(TypeName = "DATETIME2")]
    public DateTime? FechaRealizacion { get; set; } //Fecha de realizacion del procedimiento

    [Column(TypeName = "NVARCHAR(250)")]
    public string? Resultados { get; set; }

    public virtual Usuario? Usuario { get; set; }

    public virtual Paciente? Paciente { get; set; }

    /*
    public Procedimiento(string pacienteCedula, int medicoResponsable, string descripcion, 
                            DateTime fechaRealizacion, string resultados ){
        //this.ID = id_Procedimiento;
        this.PacienteCedula = pacienteCedula;
        this.MedicoResponsable = medicoResponsable;
        this.Descripcion = descripcion;
        this.FechaRealizacion = fechaRealizacion;
        this.Resultados = resultados;
    }
    */
}