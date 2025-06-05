using EducationalInstitution.Data;
using EducationalInstitution.Models.Identity;

namespace EducationalInstitution.Services.Repositories;

public interface IUserRepository : IRepository<ApplicationUser, Ulid> { }

public class UserRepository(ApplicationDbContext db)
    : Repository<ApplicationUser, Ulid>(db),
        IUserRepository { }
