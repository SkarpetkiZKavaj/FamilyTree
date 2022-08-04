using FamilyTree_DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FamilyTree_DAL.EF;

public class PersonContext : DbContext
{
    private IConfiguration configuration;
    public virtual DbSet<Person> Persons { get; set; }
    public virtual DbSet<Description> Descriptions { get; set; }

    public PersonContext()
    {
    }

    public PersonContext(DbContextOptions<PersonContext> options, IConfiguration configuration) : base()
    {
        this.configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(configuration["ConnectionString:DefaultConnection"]);
        }
    }
}