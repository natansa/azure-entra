using Infrastructure.Auth.Interface;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;

namespace Infrastructure.Auth;

public static class ConfigAuth
{
    public static void ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAuthUserService, AuthUserService>();
        //services.AddScoped<ITokenAcquisition, TokenAcquisition>();

        var initialScopes = configuration.GetValue<string>("DownstreamApi:Scopes")?.Split(' ');

        services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApp(configuration)
            .EnableTokenAcquisitionToCallDownstreamApi(initialScopes)
            .AddMicrosoftGraph(configuration.GetSection("DownstreamApi"))
            .AddInMemoryTokenCaches();
    }
}