using BusinessLogicLayer;
using DataTransferObjects;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Produces("application/json")]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public class DevicesController(DeviceManager deviceManager) : ControllerBase
  {

    [HttpGet("{id:int}", Name = "GetById")]
    [ProducesResponseType(typeof(DocumentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDeviceById([FromRoute] int id)
    {
      var device = await deviceManager.GetDeviceById(id);
      if (device == null)
      {
        return NotFound();
      }
      return Ok(device);
    }

    [HttpGet(Name = "GetAll")]
    [ProducesResponseType(typeof(IEnumerable<DocumentListDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDevices()
    {
      var devices = await deviceManager.GetDevicesAsync();
      return Ok(devices);
    }

    [HttpGet("name", Name = "Search")]
    [ProducesResponseType(typeof(IEnumerable<DocumentListDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDevicesByName([FromQuery] string name, [FromQuery] bool sort)
    {
      var devices = await deviceManager.GetDevicesByNameAsync(name, sort);
      return Ok(devices);
    }

    [HttpPost(Name = "Add")]
    [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddDevice([FromBody] DocumentDto deviceDto)
    {
      //if (!ModelState.IsValid)
      //{
        
      //}

      var result = await deviceManager.AddOrUpdateDeviceAsync(deviceDto);
      if (!result.Success)
      {
        return result.Exception switch
        {
          DbUpdateConcurrencyException => BadRequest("Concurrency Error"),
          DbUpdateException => BadRequest("Validation Error"),
          _ => BadRequest() // result.Exception
        };
      }
      var id = result.Data.Id; // TODO: Get the id of the newly created device
      return Created($"api/devices/{id}", id);
    }

    [HttpPut(Name = "Update")]
    [ProducesResponseType(typeof(void), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateDevice([FromBody] DocumentDto deviceDto)
    {
      var result = await deviceManager.AddOrUpdateDeviceAsync(deviceDto);
      if (!result.Success)
      {
        return result.Exception switch
        {
          DbUpdateConcurrencyException => BadRequest("Concurrency Error"),
          DbUpdateException => BadRequest("Validation Error"),
          _ => BadRequest() // result.Exception
        };
      }
      return Accepted();
    }
  }
}
