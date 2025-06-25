using System.Reflection;
using EducationalInstitution.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace EducationalInstitution.Data.Seeder;

public class DataSeeder(
    ApplicationDbContext context,
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager,
    IConfiguration config
)
{
    private readonly ApplicationDbContext _context = context;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly RoleManager<ApplicationRole> roleManager = roleManager;
    private readonly IConfiguration config = config;

    // Seed roles and users
    public async Task SeedUsers()
    {
        if (!_userManager.Users.Any())
        {
            // admin user
            var email = config["DefaultUsers:Admin:Email"];
            var password = config["DefaultUsers:Admin:Password"];
            ApplicationUser adminUser = new ApplicationUser
            {
                Email = email,
                EmailConfirmed = true,
                UserName = email,
            };

            await _userManager.CreateAsync(adminUser, password);

            await _userManager.AddToRoleAsync(
                adminUser,
                EducationalInstitution.Models.Common.Roles.Admin
            );
        }
    }

    public async Task SeedRoles()
    {
        if (!_context.Roles.Any())
        {
            var definedRoles = typeof(Models.Common.Roles)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(fi => fi.FieldType == typeof(string))
                .Select(fi => fi.GetValue(null).ToString())
                .ToList();

            foreach (var roleName in definedRoles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new ApplicationRole { Name = roleName });
                }
            }
        }
    }
}
