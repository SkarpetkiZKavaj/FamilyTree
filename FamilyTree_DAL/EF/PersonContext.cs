using System.Security.Cryptography;
using FamilyTree_DAL.Models;
using FTEntities.Models.Tree;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FamilyTree_DAL.EF;

public class PersonContext : DbContext
{
    private IConfiguration configuration;
    private string ownerId;
    public virtual DbSet<Person> Persons { get; set; }
    public virtual DbSet<Tree> Trees { get; set; }
    public virtual DbSet<Description> Descriptions { get; set; }

    public PersonContext()
    {
    }

    public PersonContext(DbContextOptions<PersonContext> options, IConfiguration configuration, IHttpContextAccessor accessor) : base()
    {
        this.configuration = configuration;
        ownerId = accessor?.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(configuration["ConnectionString:DefaultConnection"]);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>().HasQueryFilter(p => p.OwnerId == ownerId);
        modelBuilder.Entity<Tree>().HasQueryFilter(t => t.OwnerId == ownerId);
        modelBuilder.Entity<Description>().HasQueryFilter(d => d.OwnerId == ownerId);
    }
}