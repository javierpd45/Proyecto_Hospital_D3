using System.ComponentModel.DataAnnotations.Schema;
using Hospital.Domain.Common;

namespace Hospital.Domain;

public class Autorizacion : BaseDomainModel{
    [Column(TypeName = "INT")]
    public int ID_Paciente { get; set; }

    [Column(TypeName = "NVARCHAR(150)")]
    public string TipoAutorizacion { get; set; }

    [Column(TypeName = "DATETIME")]
    public DateTime FechaSolicitud { get; set; }

    [Column(TypeName = "DATETIME")]
    public DateTime FechaAprobacion { get; set; }

    public virtual Paciente? Paciente { get; set; }

    public Autorizacion(int id_Paciente, string tipoAutorizacion, string descripcion, DateTime fechaSolicitud, DateTime fechaAprobacion){
        this.ID_Paciente = id_Paciente;
        this.TipoAutorizacion = tipoAutorizacion;
        this.Descripcion = descripcion;
        this.FechaSolicitud = fechaSolicitud;
        this.FechaAprobacion = fechaAprobacion;
    }
}