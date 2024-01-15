using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using Hospital.Domain.Common;

namespace Hospital.Domain;

public class Autorizacion : BaseDomainModel{
    [Column(TypeName = "NVARCHAR(15)")]
    public string? PacienteCedula { get; set; }

    [Column(TypeName = "NVARCHAR(150)")]
    public string? TipoAutorizacion { get; set; }

    [Column(TypeName = "DATETIME2")]
    public DateTime? FechaSolicitud { get; set; }

    [Column(TypeName = "DATETIME2")]
    public DateTime? FechaAprobacion { get; set; }

    public virtual Paciente? Paciente { get; set; }

    /*
    public Autorizacion(string pacienteCedula, string tipoAutorizacion, string descripcion, DateTime fechaSolicitud, DateTime fechaAprobacion){
        this.PacienteCedula = pacienteCedula;
        this.TipoAutorizacion = tipoAutorizacion;
        this.Descripcion = descripcion;
        this.FechaSolicitud = fechaSolicitud;
        this.FechaAprobacion = fechaAprobacion;
    }
    */
}