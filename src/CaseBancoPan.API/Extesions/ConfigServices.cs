using CaseBancoPan.API.ContextDb;
using CaseBancoPan.API.Interface;
using CaseBancoPan.API.Repository;
using CaseBancoPan.API.Services;
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
}