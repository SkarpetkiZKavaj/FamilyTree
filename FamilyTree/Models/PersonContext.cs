using Microsoft.EntityFrameworkCore;

namespace FamilyTree.Models;

public class PersonContext : DbContext
{
    public virtual DbSet<Person> Persons { get; set; }
    public virtual DbSet<Description> Descriptions { get; set; }

    public PersonContext()
    {
    }

    public PersonContext(DbContextOptions<PersonContext> options) : base()
    {
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql();
        }
    }
}