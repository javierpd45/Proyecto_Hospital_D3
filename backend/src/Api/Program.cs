using System.Globalization;
using System.Text;
using Hospital.Domain;
using Hospital.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

        builder.Services.AddDbContext<HospitalDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"),
            b => b.MigrationsAssembly(typeof(HospitalDbContext).Assembly.FullName) //Imprime el log de cada tarea, en la consola
            )
        );

        // Add services to the container.

        builder.Services.AddControllers(opt => //Para que tengan que loguearse antes de hacer cualquier cosa
        {
            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            opt.Filters.Add(new AuthorizeFilter(policy));
        });

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

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //app.UseHttpsRedirection();

        app.UseAuthorization();
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

        //---------------------------------------------------------------------------------------------------------------------------------

        app.Run();
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