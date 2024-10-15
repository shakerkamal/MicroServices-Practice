using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PlatformService.AsyncDataServices;
using PlatformService.Data;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
if(builder.Environment.IsProduction())
{
    builder.Services.AddDbContext<AppDbContext>(opt => 
        opt.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsConn")));
}
else
{
    builder.Services.AddDbContext<AppDbContext>(opt => 
        opt.UseInMemoryDatabase("InMem"));
}

builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
// Add RabbitMQ message client
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v1", new OpenApiInfo {Title = "PlatformService", Version="v1"})
);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

PrepDb.PrepPopulation(app, builder.Environment.IsProduction());

app.Run();
