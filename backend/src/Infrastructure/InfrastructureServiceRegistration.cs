using Hospital.Application.Models.Token;
using Hospital.Application.Persistence;
using Hospital.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.Persistence;

public static class InfrastructureServiceRegistration{
    
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
                                                                IConfiguration configuration
    )
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));

        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        return services;
    }
}