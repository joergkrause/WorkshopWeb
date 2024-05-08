using Grpc.Net.Client;
using WebFrontend.Components;
using BackendForFrontend;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services
  .AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
  .AddMicrosoftIdentityWebApp(config =>
  {
    builder.Configuration.Bind("AzureAd", config);

    config.SaveTokens = true;
  });
builder.Services.AddSingleton<HttpClient>(sp =>
{
  var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
  var httpClient = new HttpClient(new DelegeateHandler(httpContextAccessor));
  return httpClient;
});

builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddControllers();
builder.Services.AddControllersWithViews().AddMicrosoftIdentityUI();
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddRazorPages();

builder.Services.AddGrpc();
var documentChannel = GrpcChannel.ForAddress("https://localhost:7033");
builder.Services.AddSingleton(documentChannel);
builder.Services.AddSingleton(new Documents.DocumentsClient(documentChannel));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error", createScopeForErrors: true);
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapRazorPages();
app.MapControllers();

app.Run();
