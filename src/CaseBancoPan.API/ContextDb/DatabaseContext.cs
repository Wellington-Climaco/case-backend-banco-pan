using CaseBancoPan.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseBancoPan.API.ContextDb;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options){}
    
    public DbSet<Pessoa> Pessoas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pessoa>().HasKey(x => x.Id);
        modelBuilder.Entity<Pessoa>().Property(x => x.Id).ValueGeneratedNever();
        
        modelBuilder.Entity<Pessoa>().Property(x => x.UpdatedAt).IsRequired(false);
        
        modelBuilder.Entity<Pessoa>().Property(x => x.DataNascimento).HasColumnType("date");
        
        modelBuilder.Entity<Pessoa>().Property(x => x.CreatedAt).HasColumnType("smalldatetime");
        modelBuilder.Entity<Pessoa>().Property(x => x.CreatedAt).HasColumnType("smalldatetime");
        
        
    }
}