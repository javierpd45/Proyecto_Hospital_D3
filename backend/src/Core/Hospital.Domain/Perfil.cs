using System.ComponentModel.DataAnnotations.Schema;
using Hospital.Domain.Common;

namespace Hospital.Domain;

public class Perfil : BaseDomainModel {
    //ID heredado de BaseDomainModel

    [Column(TypeName = "NVARCHAR(150)")]
    public string? NombrePerfil { get; set; }

    //[Column(TypeName = "NVARCHAR(150)")]
    //public string Descripcion { get; set; } //Descripcion sera heredado

    public virtual ICollection<Usuario>? Usuarios { get; set; } //Relacion muchos

    /*
    public Perfil(string nombrePerfil, string descripcion){
        //this.ID = id_Perfil;
        this.NombrePerfil = nombrePerfil;
        this.Descripcion = descripcion;
    }
    */
}