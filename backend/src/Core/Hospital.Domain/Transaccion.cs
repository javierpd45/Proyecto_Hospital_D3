using System.ComponentModel.DataAnnotations.Schema;
using Hospital.Domain.Common;

namespace Hospital.Domain;

public class Transaccion : BaseDomainModel {
    [Column(TypeName = "INT")]
    public int ID_Cuenta { get; set; }

    [Column(TypeName = "NVARCHAR(150)")]
    public string TipoTransaccion { get; set; }

    [Column(TypeName = "DECIMAL(10, 2)")]
    public decimal Monto { get; set; }

    [Column(TypeName = "DATETIME")]
    public DateTime FechaTransaccion { get; set; }

    public virtual Cuenta? Cuenta { get; set; }

    public Transaccion(int id_Cuenta, string tipoTransaccion, decimal monto, DateTime fechaTransaccion){
        this.ID_Cuenta = id_Cuenta;
        this.TipoTransaccion = tipoTransaccion;
        this.Monto = monto;
        this.FechaTransaccion = fechaTransaccion;
    }
}