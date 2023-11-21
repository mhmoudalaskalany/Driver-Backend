using System.Collections.Generic;
using System.Threading.Tasks;
using Asp.Versioning;
using Driver.Api.Controllers.V1.Base;
using Driver.Application.Services.Driver;
using Driver.Common.Core;
using Driver.Common.DTO.Driver;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Driver.Api.Controllers.V1
{
    /// <summary>
    /// Drivers Controller
    /// </summary>
    [ApiVersion("1.0")]
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
        /// Get Driver by Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("get/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult<DriverDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _service.GetAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Get Driver by Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("getNameAlphabetized/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult<SuccessResponse<string>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetNameAlphabetizedAsync(int id)
        {
            var result = await _service.GetNameAlphabetizedAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Get All Drivers
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


        /// <summary>
        /// Add Driver
        /// </summary>
        /// <returns></returns>
        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult<DriverDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> AddAsync([FromBody] AddDriverDto model)
        {
            var result = await _service.AddAsync(model);
            return Ok(result);
        }


        /// <summary>
        /// Add Random Drivers
        /// </summary>
        /// <returns></returns>
        [HttpPost("addRandomDrivers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult<int>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> AddRandomDriversAsync()
        {
            var result = await _service.AddRandomDriversAsync();
            return Ok(result);
        }

        /// <summary>
        /// Update Driver
        /// </summary>
        /// <returns></returns>
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult<DriverDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateDriverDto model)
        {
            var result = await _service.UpdateAsync(model);
            return Ok(result);
        }

        /// <summary>
        /// Delete Driver By Id
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _service.DeleteAsync(id);
            return Ok(result);
        }
    }
}
