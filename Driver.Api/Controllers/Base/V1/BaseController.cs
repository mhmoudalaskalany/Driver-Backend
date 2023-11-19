using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Candidate.Api.Controllers.Base.V1
{
    /// <inheritdoc />
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
    }
}