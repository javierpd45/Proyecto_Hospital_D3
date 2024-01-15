using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hospital.Domain.Common;

namespace Hospital.Domain;

public class Transaccion{

    [Key]
    public int TransaccionID { get; set; }

    [Column(TypeName = "INT")]
    public int? CuentaID { get; set; }

    [Column(TypeName = "NVARCHAR(150)")]
    public string? TipoTransaccion { get; set; }

    [Column(TypeName = "DECIMAL(10, 2)")]
    public decimal? Monto { get; set; }

    [Column(TypeName = "DATETIME2")]
    public DateTime? FechaTransaccion { get; set; }

    public virtual Cuenta? Cuenta { get; set; }

    /*
    public Transaccion(int cuentaId, string tipoTransaccion, decimal monto, DateTime fechaTransaccion){
        this.CuentaID = cuentaId;
        this.TipoTransaccion = tipoTransaccion;
        this.Monto = monto;
        this.FechaTransaccion = fechaTransaccion;
    }
    */
}