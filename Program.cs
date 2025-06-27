using EducationalInstitution.Data;
using EducationalInstitution.Endpoints.Account;
using EducationalInstitution.Extensions;
using EducationalInstitution.Models.Identity;
using EducationalInstitution.Models.Settings;
using EducationalInstitution.Services;
using EducationalInstitution.Services.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NSwag.Generation.Processors.Security;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(
    (context, configuration) => configuration.ReadFrom.Configuration(context.Configuration)
);

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder
    .Services.AddIdentityApiEndpoints<ApplicationUser>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.SignIn.RequireConfirmedEmail = false;
    })
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

builder.Services.AddAuthorizations(builder.Configuration);
builder.Services.CustomServicesInstaller();
builder.Services.RepositoriesInstall();

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews().AddControllersAsServices();
builder.Services.AddRazorPages();
builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
builder.Services.AddEndpointsApiExplorer();

// Configure CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowReactApp",
        builder =>
            builder
                .WithOrigins("https://localhost:44479") // THIS IS YOUR REACT APP'S URL
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
    ); // Important if you use cookies/session IDs (though not common with JWTs)
    // For simple JWTs, you might not need AllowCredentials, but it's safe to include.
});

builder.Services.AddOpenApiDocument(document =>
{
    document.AddSecurity(
        JwtBearerDefaults.AuthenticationScheme,
        new NSwag.OpenApiSecurityScheme
        {
            Type = NSwag.OpenApiSecuritySchemeType.Http,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            BearerFormat = "JWT",
            Description = "Type into the textbox: {your JWT token}.",
        }
    );

    document.OperationProcessors.Add(
        new AspNetCoreOperationSecurityScopeProcessor(JwtBearerDefaults.AuthenticationScheme)
    );
});

builder
    .Services.AddOptions<EmailSettings>()
    .BindConfiguration(nameof(EmailSettings))
    .ValidateDataAnnotations()
    .ValidateOnStart();

if (args.Length != 0 && args[0] == "seed")
{
    await builder.Services.SeedData();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.MapScalarApiReference();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowReactApp"); // <--- APPLY YOUR CORS POLICY HERE!

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.MapUserAccountEndpoints();

app.MapDefaultControllerRoute();

// app.MapFallbackToFile("/");

app.Run();
