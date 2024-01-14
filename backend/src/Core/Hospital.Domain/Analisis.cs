using System.ComponentModel.DataAnnotations.Schema;
using Hospital.Domain.Common;

namespace Hospital.Domain;

public class Analisis : BaseDomainModel{
    [Column(TypeName = "NVARCHAR(15)")]
    public int PacienteCedula { get; set; }

    [Column(TypeName = "NVARCHAR(150)")]
    public string TipoAnalisis { get; set; }

    [Column(TypeName = "NVARCHAR(250)")]
    public string Resultados { get; set; }

    [Column(TypeName = "DATETIME2")]
    public DateTime FechaRealizacion { get; set; }

    public virtual Paciente? Paciente { get; set; }

    public Analisis(int pacienteCedula, string tipoAnalisis, string resultados, DateTime fechaRealizacion){
        this.PacienteCedula = pacienteCedula;
        this.TipoAnalisis = tipoAnalisis;
        this.Resultados = resultados;
        this.FechaRealizacion = fechaRealizacion;
    }
}