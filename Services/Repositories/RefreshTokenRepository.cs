using EducationalInstitution.Data;
using EducationalInstitution.Models.Identity;

namespace EducationalInstitution.Services.Repositories;

public interface IRefreshTokenRepository : IUlidRepository<RefreshToken> { }

public class RefreshTokenRepository(ApplicationDbContext db)
    : UlidRepository<RefreshToken>(db),
        IRefreshTokenRepository { }
