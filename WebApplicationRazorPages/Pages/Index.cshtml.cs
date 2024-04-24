using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplicationRazorPages.Pages
{
  public class IndexModel : PageModel
  {
    private readonly ILogger<IndexModel> _logger;
    private readonly DevicesClient _devicesClient;

    public IndexModel(ILogger<IndexModel> logger, DevicesClient client)
    {
      _logger = logger;
      _devicesClient = client;
    }

    public async Task OnGet()
    {
      Devices = await _devicesClient.GetAllAsync();
    }

    public IEnumerable<DeviceListDto> Devices { get; set; } = [];

  }
}
