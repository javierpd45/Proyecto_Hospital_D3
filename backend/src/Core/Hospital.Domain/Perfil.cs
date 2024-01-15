using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hospital.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Domain;

//[Keyless]
public class Perfil {
    //ID heredado de BaseDomainModel

    [Key]
    public int? PerfilID { get; set; }

    [Column(TypeName = "NVARCHAR(150)")]
    public string? NombrePerfil { get; set; }

    [Column(TypeName = "NVARCHAR(150)")]
    public string? Descripcion { get; set; } //Descripcion sera heredado

    public virtual ICollection<Usuario>? Usuarios { get; set; } //Relacion muchos

    /*
    public Perfil(string nombrePerfil, string descripcion){
        //this.ID = id_Perfil;
        this.NombrePerfil = nombrePerfil;
        this.Descripcion = descripcion;
    }
    */
}