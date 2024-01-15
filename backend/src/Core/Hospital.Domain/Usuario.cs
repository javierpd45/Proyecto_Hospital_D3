using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Hospital.Domain.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Hospital.Domain;

public class Usuario : BaseDomainModel { //IdentityUser

    //ID heredado de BaseDomainModel

    [Column(TypeName = "INT")]
    public int? RollID { get; set; } //(Clave foránea referenciando la tabla de Perfiles)

    [Column(TypeName = "NVARCHAR(150)")]
    public string? Nombre { get; set; }

    [Column(TypeName = "NVARCHAR(150)")]
    public string? Apellido { get; set; }

    [Column(TypeName = "NVARCHAR(100)")]
    public string? NombreUsuario { get; set; }

    [Column(TypeName = "NVARCHAR(100)")]
    public string? Contrasenia { get; set; }

    [Column(TypeName = "NVARCHAR(50)")]
    public string? Correo { get; set; }

    [Column(TypeName = "NVARCHAR(15)")]
    public string? Telefono { get; set; }

    //FechaCreacion se hereda de la clase abstracta BaseDomainModel
    //[Column(TypeName = "DATETIME2")]
    //public DateTime FechaCreacion { get; set; }

    public bool? EstaActivo { get; set; } = true; //Propiedad para saber si el usuario esta activo o no esta activo
                                      //Sirve para solo darle acceso a la aplicacion si esta activo

    public virtual Perfil? Perfil { get; set; } //Relacion Uno

    //public virtual ICollection<Ingreso_y_Alta>? Ingresos_y_Altas { get; set; }

    public virtual ICollection<Procedimiento>? Procedimientos { get; set; }

    /*
    public Usuario(int rollid, string nombre, string apellido, string nombreUsuario, 
            string contrasenia, string correo, string telefono, DateTime fechaCreacion, bool estaActivo){

        //this.ID = id_Usuario;
        this.RollID = rollid;
        this.Nombre = nombre;
        this.Apellido = apellido;
        this.NombreUsuario = nombreUsuario;
        this.Contrasenia = contrasenia;
        this.Correo = correo;
        this.Telefono = telefono;
        this.FechaCreacion = fechaCreacion;
        this.EstaActivo = estaActivo;
    }
    */

}

/*
Usuarios:

Atributos:
UsuarioID (Clave primaria)
Nombre
Apellido
NombreUsuario
Contraseña (se recomienda almacenar de forma segura utilizando técnicas de hash y salting)
DatosContacto (puede incluir correo electrónico, teléfono, etc.)
FechaCreacion
RolID (Clave foránea referenciando la tabla de Perfiles)
*/