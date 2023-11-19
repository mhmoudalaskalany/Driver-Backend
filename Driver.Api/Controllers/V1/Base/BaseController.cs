using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Driver.Api.Controllers.V1.Base
{
    /// <inheritdoc />
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
    }
}