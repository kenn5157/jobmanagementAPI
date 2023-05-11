
using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyResolver;

public class DependencyResolver
{
    public static void RegisterApplicationLayer(IServiceCollection service)
    {
        service.AddScoped<IAuthenticationService, AuthenticationService>();
        service.AddScoped<IProblemService, ProblemService>();
    }
}