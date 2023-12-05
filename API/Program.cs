using API.Setup;
using Domain.Option;
using Infrastructure.Setup;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var secret = builder.Configuration.GetSection("Authentication:TokenSecret").Value;
builder.Services.AddJwtAuthentication(secret);

builder.Services.Configure<Secrets>(builder.Configuration);

var connectionString = builder.Configuration.GetSection("DatabaseConnectionString").Value;
builder.Services.AddDbContext<DatabaseContext>(config => config.UseNpgsql(connectionString));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.RegisterServices();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseReDoc(config =>
{
    config.DocumentTitle = "ReDoc";
    config.SpecUrl = "/swagger/v1/swagger.json";
});

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
