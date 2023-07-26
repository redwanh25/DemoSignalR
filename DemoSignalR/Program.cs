using DemoSignalR.Context;
using DemoSignalR.Hubs;
using Microsoft.EntityFrameworkCore;
using Serilog;

var allowAllHeaders = "AllowAllHeaders";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowAllHeaders,
                      builder => {
                          builder.AllowAnyOrigin()
                                 .AllowAnyHeader()
                                 .AllowAnyMethod();
                      });
});

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Host.UseSerilog((hostingContext, configuration) =>
{
    configuration
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(hostingContext.Configuration);
});

var app = builder.Build();

// Add middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(allowAllHeaders);
app.UseAuthorization();

app.MapControllers();
app.MapHub<NotificationHub>("/hub/notification");

app.Run();
