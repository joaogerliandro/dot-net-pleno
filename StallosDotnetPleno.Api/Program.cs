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
using StallosDotnetPleno.Application.Helpers;
using StallosDotnetPleno.Application.Services.RosterServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PersonValidator>());

builder.Services.AddEndpointsApiExplorer();

// Configure database context
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));

// Register repositories
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonTypeRepository, PersonTypeRepository>();

// Register validators
builder.Services.AddScoped<IValidator<Person>, PersonValidator>();
builder.Services.AddScoped<IValidator<Address>, AddressValidator>();

// Register services
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IRosterApiService, RosterApiService>();
builder.Services.AddScoped<IBackgroundProcessingService, BackgroundProcessingService>();

// Register hosted service and task queue
builder.Services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
builder.Services.AddHostedService<BackgroundTaskService>();

// Register helpers
builder.Services.AddSingleton<ConfigHelper>();

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
