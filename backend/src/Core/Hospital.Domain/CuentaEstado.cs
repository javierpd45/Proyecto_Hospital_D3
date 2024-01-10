using System.Runtime.Serialization;

namespace Hospital.Domain;

public enum CuentaEstado {
    [EnumMember(Value = "Pagada")]
    Pagada,
    [EnumMember(Value = "Pendiente")]
    Pendiente
}