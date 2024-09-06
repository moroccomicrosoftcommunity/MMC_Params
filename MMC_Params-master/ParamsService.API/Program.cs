using Microsoft.EntityFrameworkCore;
using ParamsService.Infrastructure.Data;
using ParamsService.Infrastructure;
using ParamsService.Application;
using Microsoft.AspNetCore.Builder.Extensions;
using MMCEventsV1.Middlewares;
using ParamsService.API.Services;
using ParamsService.Application.Interfaces;
using ParamsService.Application.Services;
using Microsoft.AspNetCore.Hosting;
using Azure.Storage.Blobs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(x => new BlobServiceClient(builder.Configuration.GetConnectionString("StorageAccount")));

// Add services to the container.


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("https://mmc-apigateway.azurewebsites.net")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();




builder.Services.AddScoped<IRabbitMQ, RpcClient>();
builder.Services.AddScoped<INotificationsServer, NotificationsServer>();
var app = builder.Build();




if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseCors("AllowSpecificOrigin");
app.MapControllers();
app.UseRouting();
app.Run();
