using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WarehouseAPI;
using WarehouseAPI.Entities;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<WarehouseSeeder>();
builder.Services.AddDbContext<WarehouseDbContext>(option => option
.UseSqlServer(builder.Configuration.GetConnectionString("WarehouseConnectionString")));


var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<WarehouseSeeder>();
seeder.Seed();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
