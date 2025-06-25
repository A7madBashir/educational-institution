using EducationalInstitution.Mapper;

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
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();
    }
}
