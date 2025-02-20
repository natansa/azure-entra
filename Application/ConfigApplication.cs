using Application.UseCase;
using Application.UseCase.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ConfigApplication
{
    public static void ConfigureApplication(this IServiceCollection services)
    {
        services.AddScoped<IProfileUseCase, ProfileUseCase>();
        services.AddScoped<IUserSyncUseCase, UserSyncUseCase>();
    }
}