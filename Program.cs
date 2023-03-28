using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using WarehouseAPI;
using WarehouseAPI.Entities;
using WarehouseAPI.Middleware;
using WarehouseAPI.Models;
using WarehouseAPI.Models.Validators;
using WarehouseAPI.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddScoped<IGoodsService, GoodsService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDetailsService, OrderDetailsService>();
builder.Services.AddScoped<IPasswordHasher<Employee>, PasswordHasher<Employee>>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IValidator<RegisterEmployeesDto>, RegisterEmployeeDtoValidator>();
builder.Services.AddScoped<IValidator<AddOrderDetailsDto>, AddOrderDetailsDtoValidation>();
builder.Services.AddScoped<IValidator<UpdateStatusDto>, UpdateStatusDtoValidator>();
builder.Services.AddScoped<IValidator<ClientDto>, AddClientDtoValidation>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<WarehouseSeeder>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddDbContext<WarehouseDbContext>(option => option
.UseSqlServer(builder.Configuration.GetConnectionString("WarehouseConnectionString")));
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddSwaggerGen();
var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
    option.DefaultScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
    };
});

var app = builder.Build();
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<WarehouseSeeder>();
seeder.Seed();

// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "WarehouseAPI");
});
app.UseAuthorization();

app.MapControllers();

app.Run();
