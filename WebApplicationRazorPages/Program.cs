using System.Reflection.Metadata.Ecma335;
using WebApplicationRazorPages;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<DevicesClient>(sp =>
{
  var httpClient = new HttpClient();
  var client = new DocumentClient("https://localhost:7242", httpClient);
  return client;
});  
// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
