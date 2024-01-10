using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Domain.Common;

public abstract class BaseDomainModel {

    [Column(TypeName = "INT")]
    public int ID { get; set; }

    [Column(TypeName = "DATETIME")]
    public DateTime? FechaCreacion { get; set; }

    [Column(TypeName = "NVARCHAR(150)")]
    public string? CreadoPor {get;set;}

    [Column(TypeName = "DATETIME")]
    public DateTime? UltimaFechaModificacion { get; set; }

    [Column(TypeName = "DATETIME")]
    public DateTime? UltimaModificacionPor { get; set; }

    [Column(TypeName = "NVARCHAR(250)")]
    public string? Descripcion { get; set; }
}