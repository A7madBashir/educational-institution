using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationalInstitution.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BaseApiController : ControllerBase { }
