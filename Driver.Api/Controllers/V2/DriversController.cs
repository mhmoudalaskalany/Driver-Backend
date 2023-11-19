using System.Collections.Generic;
using System.Threading.Tasks;
using Asp.Versioning;
using Driver.Api.Controllers.V2.Base;
using Driver.Application.Services.Driver;
using Driver.Common.Core;
using Driver.Common.DTO.Driver;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Driver.Api.Controllers.V2
{
    /// <summary>
    /// Drivers Controller V2
    /// </summary>
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DriversController : BaseController
    {
        private readonly IDriverService _service;
        /// <summary>
        /// Constructor
        /// </summary>
        public DriversController(IDriverService service)
        {
            _service = service;
        }


        /// <summary>
        /// Get All Candidates
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult<List<DriverDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }
    }
}
