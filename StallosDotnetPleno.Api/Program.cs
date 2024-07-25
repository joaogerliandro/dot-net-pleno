using StallosDotnetPleno.Domain.Interfaces;
using StallosDotnetPleno.Domain.Entities;
using StallosDotnetPleno.Infrastructure.Context;
using StallosDotnetPleno.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using StallosDotnetPleno.Domain.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext with SQL Server
builder.Services.AddDbContext<PersonContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped<IRepository<Person>, PersonRepository>();
builder.Services.AddScoped<IRepository<Address>, AddressRepository>();

// Register validators
builder.Services.AddScoped<IValidator<Person>, PersonValidator>();
builder.Services.AddScoped<IValidator<Address>, AddressValidator>();

// Register services
// builder.Services.AddScoped<ServiceClass>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
