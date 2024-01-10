using System.ComponentModel.DataAnnotations.Schema;
using Hospital.Domain.Common;

namespace Hospital.Domain;

public class Cuenta : BaseDomainModel {

    //El ID de la cuenta se hereda de la clase BaseDomainModel

    [Column(TypeName = "INT")]
    public int ID_Paciente { get; set; } //(Clave foránea referenciando la tabla de Pacientes)

    // [Column(TypeName = "NVARCHAR(150)")]
    // public string NombreOperacion { get; set; }

    [Column(TypeName = "DECIMAL(10, 2)")]
    public decimal? Saldo { get; set; }

    [Column(TypeName = "NVARCHAR(20)")]
    public CuentaEstado Estado { get; set; } //= CuentaEstado.Pagada; //Esto se hace por si es necesario darle un valor por defecto

    // [Column(TypeName = "NVARCHAR(250)")]
    // public string? Descripcion { get; set; } //Descripcion sera heredado

    //public DateTime? FechaCreacion { get; set; }
    //Se toma de la clase abstracta BaseDomainModel

    public virtual Paciente? Paciente { get; set; }

    public virtual ICollection<Transaccion>? Transacciones { get; set; }

    public Cuenta(int id_Paciente, decimal saldo, string descripcion, CuentaEstado estado, DateTime fechaCreacion){
        //this.ID = id_Cuenta;
        this.ID_Paciente = id_Paciente;
        //this.NombreOperacion = nombreOperacion;
        this.Saldo = saldo;
        this.Estado = estado;
        this.Descripcion = descripcion;
        this.FechaCreacion = fechaCreacion;
    }

}

/*
Cuentas:

Atributos:
CuentaID (Clave primaria)
PacienteID (Clave foránea referenciando la tabla de Pacientes)
Saldo
FechaCreacion
Estado
*/