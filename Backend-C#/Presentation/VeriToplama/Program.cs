using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using RMS_Service.Presentation.VeriToplama;
using External_Service;
using Persistence;

var builder = Host.CreateApplicationBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddPersistenceRegistration();
builder.Services.AddExternalRegistration();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
using (var scope = host.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated(); 
}
host.Run();
