using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Domain.Common;

public abstract class BaseDomainModel {

    [Column(TypeName = "INT")]
    public int ID { get; set; }

    [Column(TypeName = "DATETIME2")]
    public DateTime? FechaCreacion { get; set; }

    [Column(TypeName = "NVARCHAR(250)")]
    public string? Descripcion { get; set; }



    //-----------------------------------------------------------------------------------

    [Column(TypeName = "DATETIME2")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "NVARCHAR(150)")]
    public string? CreatedBy {get;set;}

    [Column(TypeName = "DATETIME2")]
    public DateTime? LastModifiedDate { get; set; }

    [Column(TypeName = "DATETIME2")]
    public string? LastModifiedBy { get; set; }

    //--------------------------------------------------------------------------------
}