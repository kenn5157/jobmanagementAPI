
using Application.Interfaces;
using Application.Validators;
using Applicatoin.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyResolver;

public class DependencyResolver
{
    public static void RegisterApplicationLayer(IServiceCollection service)
    {
        service.AddScoped<IAuthenticationService, AuthenticationService>();
        service.AddScoped<IProblemService, ProblemService>();
        service.AddScoped<ProblemValidator, ProblemValidator>();
        service.AddScoped<RegisterValidator, RegisterValidator>();
    }
}