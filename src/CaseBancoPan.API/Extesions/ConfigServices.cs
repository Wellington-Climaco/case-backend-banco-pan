using CaseBancoPan.API.ContextDb;
using CaseBancoPan.API.Entities;
using CaseBancoPan.API.Interface;
using CaseBancoPan.API.Repository;
using CaseBancoPan.API.Services;
using CaseBancoPan.API.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CaseBancoPan.API.Extesions;

public static class ConfigServices
{
    public static IServiceCollection ConfigDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        string connString = configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("ConnectionString not found");
        services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connString));
        
        return services;
    }

    public static IServiceCollection ConfigDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IPessoaRepository, PessoaRepository>();
        services.AddScoped<IPessoaService, PessoaService>();

        return services;
    }

    public static IServiceCollection ConfigFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CadastrarPessoaRequestValidator>();
        return services;
    }

    public async static Task ApplyMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using DatabaseContext dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        Console.WriteLine($"qtd migrations: {dbContext.Database.GetMigrations().Count()}");

        await dbContext.Database.EnsureDeletedAsync();
        Thread.Sleep(5000);
        await dbContext.Database.EnsureCreatedAsync();


        Pessoa[] pessoa =
        {
            new Pessoa("jose", "santos", "rua A", "11970707070", $"email1@email.com", new DateTime(2000, 01, 01)),
            new Pessoa("ana", "souza", "rua B", "11970707070", $"email2@email.com", new DateTime(2000, 01, 01)),
            new Pessoa("maria julia", "cardoso", "rua C", "11970707070", $"email3@email.com", new DateTime(2000, 01, 01)),
            new Pessoa("felipe", "silva", "rua D", "11970707070", $"email4@email.com", new DateTime(2000, 01, 01))
        };
        dbContext.Pessoas.AddRange(pessoa);
        dbContext.SaveChanges();
    }

    public static IServiceCollection ConfigCors(this IServiceCollection services)
    {
        services.AddCors(options => options.AddDefaultPolicy(policy =>
         {
             policy.AllowAnyOrigin();
             policy.AllowAnyMethod();
             policy.AllowAnyHeader();
         }));
        return services;
    }
}