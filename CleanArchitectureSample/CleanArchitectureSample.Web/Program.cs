using System.Data;
using CleanArchitectureSample.Application.Behaviors;
using CleanArchitectureSample.Domain.Abstractions;
using CleanArchitectureSample.Infrastructure;
using CleanArchitectureSample.Infrastructure.Repositories;
using CleanArchitectureSample.Web.Middleware;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace CleanArchitectureSample.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;
        var presentationAssembly = typeof(CleanArchitectureSample.Presentation.AssemblyReference).Assembly;
        builder.Services.AddControllers();
        builder.Services.AddControllers().AddApplicationPart(presentationAssembly);

        var applicationAssembly = typeof(CleanArchitectureSample.Application.AssemblyReference).Assembly;
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        builder.Services.AddValidatorsFromAssembly(applicationAssembly);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddMapster();
        builder.Services.AddSwaggerGen(c =>
        {
            var presentationDocumentationFile = $"{presentationAssembly.GetName().Name}.xml";

            var presentationDocumentationFilePath =
                Path.Combine(AppContext.BaseDirectory, presentationDocumentationFile);

            c.IncludeXmlComments(presentationDocumentationFilePath);

            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web", Version = "v1" });
        });

        builder.Services.AddDbContext<ApplicationDbContext>(builder =>
            builder.UseInMemoryDatabase("Webinar"));

        builder.Services.AddScoped<IWebinarRepository, WebinarRepository>();
        
        builder.Services.AddScoped<IUnitOfWork>(
            factory => factory.GetRequiredService<ApplicationDbContext>());
        builder.Services.AddScoped<IDbConnection>(
            factory => factory.GetRequiredService<ApplicationDbContext>().Database.GetDbConnection());

        builder.Services.AddTransient<ExceptionHandlingMiddleware>();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}