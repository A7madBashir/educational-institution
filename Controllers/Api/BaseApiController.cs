using EducationalInstitution.Models.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationalInstitution.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
[Authorize(
    AuthenticationSchemes = AuthenticationSchema.Identity + ", " + AuthenticationSchema.Bearer
)]
public class BaseApiController : ControllerBase { }
