using CaseBancoPan.API.ContextDb;
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
}