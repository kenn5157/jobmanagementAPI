
using Application.Interfaces;
using Application.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyResolver;

public class DependencyResolver
{
    public static void RegisterApplicationLayer(IServiceCollection service)
    {
        service.AddScoped<IProblemService, ProblemService>();
        service.AddScoped<ProblemValidator, ProblemValidator>();
    }
}