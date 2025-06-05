using EducationalInstitution.Data.Converter;
using EducationalInstitution.Models.Common.BaseEntity;
using EducationalInstitution.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EducationalInstitution.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, Ulid>(options)
{
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<Ulid>().HaveConversion<UlidToStringConverter>();
        configurationBuilder.Properties<DateTime>().HaveConversion<DateTimeConverter>();
    }
}
