using System.Reflection;
using DemoRestApi.Actions;
using DemoRestApi.Models;
using DemoRestApi.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var assemblies = new[] {Assembly.GetExecutingAssembly()};

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidation(opt =>
{
    opt.RegisterValidatorsFromAssemblies(assemblies);
    //opt.DisableDataAnnotationsValidation = true;
    //opt.AutomaticValidationEnabled = true;
});
builder.Services.AddHttpClient();
builder.Services.AddMediatR(assemblies);
builder.Services.AddScoped<ICatFactApiClient, CatFactApiClient>();

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
