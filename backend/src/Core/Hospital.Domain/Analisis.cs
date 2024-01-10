using System.ComponentModel.DataAnnotations.Schema;
using Hospital.Domain.Common;

namespace Hospital.Domain;

public class Analisis : BaseDomainModel{
    [Column(TypeName = "INT")]
    public int ID_Paciente { get; set; }

    [Column(TypeName = "NVARCHAR(150)")]
    public string TipoAnalisis { get; set; }

    [Column(TypeName = "NVARCHAR(250)")]
    public string Resultados { get; set; }

    [Column(TypeName = "DATETIME")]
    public DateTime FechaRealizacion { get; set; }

    public virtual Paciente? Paciente { get; set; }

    public Analisis(int id_Paciente, string tipoAnalisis, string resultados, DateTime fechaRealizacion){
        this.ID_Paciente = id_Paciente;
        this.TipoAnalisis = tipoAnalisis;
        this.Resultados = resultados;
        this.FechaRealizacion = fechaRealizacion;
    }
}