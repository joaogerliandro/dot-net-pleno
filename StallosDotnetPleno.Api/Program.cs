using StallosDotnetPleno.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using StallosDotnetPleno.Infrastructure.Data;
using StallosDotnetPleno.Application.Interfaces;
using StallosDotnetPleno.Application.Services;
using StallosDotnetPleno.Infrastructure.Interfaces;
using StallosDotnetPleno.Domain.Entities;
using StallosDotnetPleno.Domain.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PersonValidator>());

builder.Services.AddEndpointsApiExplorer();

// Configure database context
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));

// Register repositories
builder.Services.AddScoped<IRepository<Person>, PersonRepository>();
builder.Services.AddScoped<IRepository<Address>, AddressRepository>();

// Register services
builder.Services.AddScoped<IPersonService, PersonService>();

// Register validators
builder.Services.AddScoped<IValidator<Person>, PersonValidator>();
builder.Services.AddScoped<IValidator<Address>, AddressValidator>();

/*
    Authentication Services
 */

builder.Services.AddSwaggerGen();

// Add logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "LibraryManagementSystem API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
