using FTEntities.IdentityModels.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FamilyTree_DAL.EF;

public class IdentityContext : IdentityDbContext<User>
{
    private IConfiguration configuration;
    
    public IdentityContext()
    {
    }

    public IdentityContext(DbContextOptions<IdentityContext> options, IConfiguration configuration) : base()
    {
        this.configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(configuration["ConnectionString:IdentityConnection"]);
        }
    }
}