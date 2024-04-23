using BusinessLogicLayer;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DevicesController(DeviceManager deviceManager) : ControllerBase
  {

    [HttpGet("{id}")]
    public async Task<ActionResult<Device>> GetDeviceById(int id)
    {
      var device = await deviceManager.GetDeviceById(id);
      if (device == null)
      {
        return NotFound();
      }
      return device;
    }
  }
}
