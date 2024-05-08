using BackendForFrontendService.Services;
using BusinessLogicLayer;
using BusinessLogicLayer.Mappings;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var cs = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DocumentContext>(options =>
   options.UseSqlServer(cs));
builder.Services.AddScoped<DocumentManager>();

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<DocumentService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
