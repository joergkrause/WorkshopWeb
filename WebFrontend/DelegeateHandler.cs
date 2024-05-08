

using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;

internal class DelegeateHandler : DelegatingHandler
{

  private readonly IHttpContextAccessor _httpContextAccessor;

    public DelegeateHandler(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
  {
    // get token
    var token = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
    // add token to request
    request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
    return await base.SendAsync(request, cancellationToken);
  }
}