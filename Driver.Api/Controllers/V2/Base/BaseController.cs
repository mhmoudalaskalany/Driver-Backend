using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Driver.Api.Controllers.V2.Base
{
    /// <inheritdoc />
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
    }
}