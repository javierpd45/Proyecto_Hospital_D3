using System.Data;
using Hospital.Application.Models.Authorization;
using Hospital.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Hospital.Infrastructure.Persistence;

public class HospitalDbContextData{

    public static async Task LoadDataAsync(
        HospitalDbContext context,
        //UserManager<Usuario> usuarioManager,
        //RoleManager<IdentityRole> roleManager,
        ILoggerFactory loggerFactory
    )
    {
        try
        {
            /*
            
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(Role.Administrador));
                await roleManager.CreateAsync(new IdentityRole(Role.Consulta));
                await roleManager.CreateAsync(new IdentityRole(Role.Mantenimiento));
            }
            
            if (usuarioManager.Users.Any())
            {
                var usuarioAdmin = new Usuario
                {
                    RollID = 2,
                    Nombre = "Pedro",
                    Apellido = "Lora",
                    NombreUsuario = "pedro.lora",
                    Contrasenia = "pedrolora123",
                    Correo = "javierpd45@gmail.com",
                    Telefono = "0123456789"

                    
                    //Nombre = "Pedro",
                    //Apellido = "Lora",
                    //Email = "javierpd45@gmail.com",
                    //UserName = "pedro.lora",
                    //Telefono = "0123456789"
                    
                };

                await usuarioManager.CreateAsync(usuarioAdmin, "PasswordPedroLora123$");
                await usuarioManager.AddToRoleAsync(usuarioAdmin, Role.Administrador);

                var usuarioConsulta = new Usuario
                {
                    RollID = 3,
                    Nombre = "Juan",
                    Apellido = "Perez",
                    NombreUsuario = "juan.perez",
                    Contrasenia = "juanperez321",
                    Correo = "juan.perez@gmail.com",                    
                    Telefono = "9876543210"
                };

                await usuarioManager.CreateAsync(usuarioConsulta, "PasswordJuanPerez321$");
                await usuarioManager.AddToRoleAsync(usuarioConsulta, Role.Consulta);

                var usuarioMantenimiento = new Usuario
                {
                    RollID = 4,
                    Nombre = "Jhoan",
                    Apellido = "Santana",
                    NombreUsuario = "jhoan.santana",
                    Contrasenia = "joansantana123",
                    Correo = "joan.santana@gmail.com",                    
                    Telefono = "0123789654"
                };

                await usuarioManager.CreateAsync(usuarioMantenimiento, "PasswordJoanSantana123$");
                await usuarioManager.AddToRoleAsync(usuarioMantenimiento, Role.Mantenimiento);
            }

            */
        
            if (context.Perfiles!.Any())
            {
                var perfilData = File.ReadAllText("../Infrastructure/Data/perfil.json");
                var perfiles = JsonConvert.DeserializeObject<List<Perfil>>(perfilData);
                await context.Perfiles!.AddRangeAsync(perfiles!);
                await context.SaveChangesAsync();
            }

        }
        catch (Exception e)
        {
            var logger = loggerFactory.CreateLogger<HospitalDbContext>();
            logger.LogError(e.Message);
        }
    }

    public static async Task LoadDataAsync(HospitalDbContext context)
    {
        throw new NotImplementedException();
    }
}