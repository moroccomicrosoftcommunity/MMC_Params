using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using ParamsService.API.Services;
using ParamsService.Application;
using ParamsService.Infrastructure;

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
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();

app.UseCors("AllowSpecificOrigin");
app.MapControllers();
app.UseRouting();
app.Run();
