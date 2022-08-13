using FamilyTree_DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FTEntities.Models.Tree;

public class TreeTypeConfiguration : IEntityTypeConfiguration<Tree>
{
    public void Configure(EntityTypeBuilder<Tree> builder)
    {
        builder.HasMany<Person>(t => t.Members).
            WithMany(p => p.Trees);
    }
}