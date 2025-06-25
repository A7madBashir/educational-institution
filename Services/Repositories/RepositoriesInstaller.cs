namespace EducationalInstitution.Services.Repositories;

public static class RepositoriesInstaller
{
    public static void RepositoriesInstall(this IServiceCollection services)
    {
        services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
    }
}
