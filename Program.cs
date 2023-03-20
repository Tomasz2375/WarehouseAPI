using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json.Serialization;
using WarehouseAPI;
using WarehouseAPI.Entities;
using WarehouseAPI.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IGoodsService, GoodsService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<WarehouseSeeder>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddDbContext<WarehouseDbContext>(option => option
.UseSqlServer(builder.Configuration.GetConnectionString("WarehouseConnectionString")));

builder.Services.AddSwaggerGen();
var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<WarehouseSeeder>();
seeder.Seed();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "WarehouseAPI");
});
app.UseAuthorization();

app.MapControllers();

app.Run();
