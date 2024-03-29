using System.Globalization;
using System.Text;
using System.Text.Json.Serialization;
using Hospital.Application;
using Hospital.Application.Contracts.Identity;
using Hospital.Application.Features.Perfiles.Queries.GetPerfilList;
using Hospital.Domain;
using Hospital.Infrastructure.Persistence;
using Hospital.Infrastructure.Services.Auth;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

internal class Program
{
    private static async Task Main(string[] args)
    {

        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddAplicationServices(builder.Configuration);

        builder.Services.AddDbContext<HospitalDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"),
            b => b.MigrationsAssembly(typeof(HospitalDbContext).Assembly.FullName) //Imprime el log de cada tarea, en la consola
            )
        );

        builder.Services.AddMediatR(typeof(GetPerfilListQueryHandler).Assembly); //Para las consultas de los Perfiles

        // Add services to the container.

        builder.Services.AddControllers(opt => //Para que tengan que loguearse antes de hacer cualquier cosa
        {
            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            opt.Filters.Add(new AuthorizeFilter(policy));
        }).AddJsonOptions(x =>
                        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

        //Para seguridad, pero como no utilizamos IdentityUser no lo utilizo
        // -----------------------------------------------------------------------------------------------------------------------------------
        // Revisar que todo funcione bien

        /*
        IdentityBuilder identityBuilder = builder.Services.AddIdentityCore<Usuario>();
        identityBuilder = new IdentityBuilder(identityBuilder.UserType, identityBuilder.Services);

        identityBuilder.AddRoles<IdentityRole>().AddDefaultTokenProviders();
        identityBuilder.AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<Usuario, IdentityRole>>();

        identityBuilder.AddEntityFrameworkStores<HospitalDbContext>();
        identityBuilder.AddSignInManager<SignInManager<Usuario>>();
        */

        builder.Services.TryAddSingleton<ISystemClock, SystemClock>();

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]));
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateAudience = false,
                ValidateIssuer = false
            };
        });

        builder.Services.AddCors(Options =>
        {
            Options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            );
        });

        //------------------------------------------------------------------------------------------------------------------------------

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        //Sin esto no funciona Swagger
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddTransient<IAuthService, AuthService>();
        var app = builder.Build();

        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            _ = endpoints.MapControllers();
        });

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //app.UseHttpsRedirection();

        //app.UseAuthorization();
        app.UseAuthentication();

        // ------*Revisar que funcione bien*------
        app.UseCors("CorsPolicy");

/*
#region WeatherForecast 1
        var summaries = new[]
        {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

        app.MapGet("/weatherforecast", () =>
        {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
                .ToArray();
            return forecast;
        })
        .WithName("GetWeatherForecast")
        .WithOpenApi();
#endregion
*/

        //---------------------------------------------------------------------------------------------------------------------------------
#region Insercion de datos con Json
        using (var scope = app.Services.CreateScope())
        {
            var service = scope.ServiceProvider;
            var loggerFactory = service.GetRequiredService<ILoggerFactory>();

            try
            {
                var context = service.GetRequiredService<HospitalDbContext>();
                //var usuarioManager = service.GetRequiredService<UserManager<Usuario>>();
                //var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
                await context.Database.OpenConnectionAsync();
                //await HospitalDbContextData.LoadDataAsync(context, usuarioManager, roleManager, loggerFactory);
                await HospitalDbContextData.LoadDataAsync(context, loggerFactory);
                await context.Database.BeginTransactionAsync();

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "Error en el proceso");
            }

        }
#endregion
        //---------------------------------------------------------------------------------------------------------------------------------
        await app.RunAsync();
        //app.Run();
    }

}

/*
#region WeatherForecast 2
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
#endregion
*/