using Microsoft.EntityFrameworkCore;
using MoonRobotSimulation.Services;
using MoonRobotSimulation.Persistence.Interfaces;
using MoonRobotSimulation.Persistence.Repositories;
using MoonRobotSimulation.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<RobotContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IRobotCommandDataAccess, RobotCommandEF>();
builder.Services.AddScoped<IMapDataAccess, MapEF>();

builder.Services.AddScoped<RobotCommandService>();
builder.Services.AddScoped<MapService>();

builder.Services.AddAuthorization();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
