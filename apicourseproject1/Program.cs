using apicourseproject1.Data;
using apicourseproject1.Data.Interfaces;
using apicourseproject1.Data.Profiles;
using apicourseproject1.Services;
using apicourseproject1.Data.Repositories;
using AutoMapper;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Runtime;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
 x =>
 {
     x.SwaggerDoc(
         "v1",
         new OpenApiInfo
         {
             Title = $"{Assembly.GetExecutingAssembly().GetName().Name}",
             Version = "Version 1",
             Description = "Create documentation for Cars",
             Contact = new OpenApiContact
             {
                 Name = "Carlos Linares",
                 Email = "karlozlinarez@gmail.com",
             }
         });
     var xmlFilename = System.IO.Path.Combine(System.
     AppContext.BaseDirectory, $"{Assembly.
     GetExecutingAssembly().GetName().Name}.xml");
     x.IncludeXmlComments(xmlFilename);
 });

builder.Services.AddAutoMapper(cfg =>
{
}, typeof(CarProfile));
// Load DB configuration and register the connection factory for
// injection
var configuration = builder.Configuration;
builder.Services.Configure<DbSettings>(configuration.GetSection("ConnectionStrings"));
builder.Services.AddTransient<DatabaseConnectionFactory>();
builder.Services.AddTransient<CarRepository>();
builder.Services.RegisterDataAccessDependencies();

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
