using BusinessLogicLayer;
using BusinessLogicLayer.Mappings;
using DataAccessLayer;
using DomainModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserContext, UserContext>();

builder.Services
  .AddAuthentication("bearer")
  .AddJwtBearer(options =>
{
  // options.TokenValidationParameters.
  options.Events.OnTokenValidated = async context =>
  {
    var userContext = context.HttpContext.RequestServices.GetRequiredService<IUserContext>();    
    userContext.Principal = context.Principal;
  };
});

builder.Services.AddAuthorization(configure =>
{
  configure.AddPolicy("read:documents", policy =>
  {
    policy.RequireClaim("scope", "read:documents");
    policy.RequireRole("admin");
    policy.Build();
  });
});


// Add services to the container.
var cs = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DocumentContext>(options =>
   options.UseSqlServer(cs));
builder.Services.AddScoped<DocumentManager>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
