using Application.Interfaces;
using Infrastructure.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyResolver;

public class DependencyResolver
{
    public static void RegisterInfrastructureLayer(IServiceCollection service)
    {
        service.AddScoped<IDatabaseRepository, DatabaseRepository>();
        service.AddScoped<IUserRepository, UserRepository>();
        service.AddScoped<IProblemRepository, ProblemRepository>();
    }
}