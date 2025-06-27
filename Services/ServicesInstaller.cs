using EducationalInstitution.Mapper;
using EducationalInstitution.Models.Settings;

namespace EducationalInstitution.Services;

public static class ServicesInstaller
{
    /// <summary>
    /// Install services from external extension method
    /// </summary>
    /// <param name="services"></param>
    public static void CustomServicesInstaller(this IServiceCollection services)
    {
        services.AddSingleton<AppMapper>();

        services
            .AddOptions<TokenSettings>()
            .BindConfiguration(nameof(TokenSettings))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();
    }
}
