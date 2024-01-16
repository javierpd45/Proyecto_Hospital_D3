using AutoMapper;
using Hospital.Application.Behaviors;
using Hospital.Application.Mappings;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddAplicationServices(
                        this IServiceCollection services,
                        IConfiguration configuration
    )
    {
        var mapperConfig = new MapperConfiguration(mc => {
            mc.AddProfile(new MappingProfile());
        });

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}