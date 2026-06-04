
using Microsoft.EntityFrameworkCore;
using CashflowGateway.Infrastructure;
using CashflowGateway.Application;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var serverVersion = ServerVersion.AutoDetect(connectionString);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, serverVersion));


builder.Services.AddScoped<DbContext>(provider => provider.GetRequiredService<AppDbContext>());


builder.Services.AddScoped<ISyncService, SyncService>();


builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();
app.Run();

