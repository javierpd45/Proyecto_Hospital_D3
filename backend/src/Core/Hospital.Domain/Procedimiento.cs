using System.ComponentModel.DataAnnotations.Schema;
using Hospital.Domain.Common;

namespace Hospital.Domain;

public class Procedimiento : BaseDomainModel{
    //ID_Procedimiento sera heredado de BaseDomainModel

    [Column(TypeName = "INT")]
    public int ID_Paciente { get; set; } //Llave foranea de la tabla Pacientes

    [Column(TypeName = "INT")]
    public int MedicoResponsable { get; set; } //Llave foranea de la tabla Usuarios (representa el medico responsable del procedimiento)

    //public string Descripcion { get; set; } //Descripcion del procedimiento, sera heredado

    [Column(TypeName = "DATETIME")]
    public DateTime FechaRealizacion { get; set; } //Fecha de realizacion del procedimiento

    [Column(TypeName = "NVARCHAR(250)")]
    public string Resultados { get; set; }

    public virtual Usuario? Usuario { get; set; }

    public virtual Paciente? Paciente { get; set; }

    public Procedimiento(int id_Paciente, int medicoResponsable, string descripcion, 
                            DateTime fechaRealizacion, string resultados ){
        //this.ID = id_Procedimiento;
        this.ID_Paciente = id_Paciente;
        this.MedicoResponsable = medicoResponsable;
        this.Descripcion = descripcion;
        this.FechaRealizacion = fechaRealizacion;
        this.Resultados = resultados;
    }
}