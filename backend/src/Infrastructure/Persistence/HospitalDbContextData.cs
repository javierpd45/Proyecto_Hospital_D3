using Hospital.Application.Models.Authorization;
using Hospital.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Hospital.Infrastructure.Persistence;

public class HospitalDbContextData{

    public static async Task LoadDataAsync(
        HospitalDbContext context,
        UserManager<UserAsp> usuarioManager,
        RoleManager<IdentityRole> roleManager,
        ILoggerFactory loggerFactory
    )
    {
        try
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(Role.ADMIN));
                await roleManager.CreateAsync(new IdentityRole(Role.USER));
            }

            if (usuarioManager.Users.Any())
            {
                var usuarioAdmin = new UserAsp
                {
                    Nombre = "Pedro",
                    Apellido = "Lora",
                    Email = "javierpd45@gmail.com",
                    UserName = "pedro.lora",
                    Telefono = "0123456789"
                };

                await usuarioManager.CreateAsync(usuarioAdmin, "PasswordPedroLora123$");
                await usuarioManager.AddToRoleAsync(usuarioAdmin, Role.ADMIN);

                var usuario = new UserAsp
                {
                    Nombre = "Juan",
                    Apellido = "Perez",
                    Email = "juan.perez@gmail.com",
                    UserName = "juan.perez",
                    Telefono = "9876543210"
                };

                await usuarioManager.CreateAsync(usuario, "PasswordJuanPerez321$");
                await usuarioManager.AddToRoleAsync(usuario, Role.USER);
            }
        
            if (context.Perfiles!.Any())
            {
                var perfilData = File.ReadAllText("../Data/perfil.json");
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
}