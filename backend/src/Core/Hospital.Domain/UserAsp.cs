using Microsoft.AspNetCore.Identity;

namespace Hospital.Domain;

public class UserAsp : IdentityUser { //Usuario de Asp Net Core
    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Telefono { get; set; }

    public bool IsActive { get; set; }
}