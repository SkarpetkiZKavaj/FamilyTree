using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyTree.Models;

public class DescriptionConfig : IEntityTypeConfiguration<Description>
{
    public void Configure(EntityTypeBuilder<Description> builder)
    {
        builder.HasOne<Person>(d => d.Person).
                WithOne(p => p.Description).
                HasForeignKey<Description>(d => d.PersonId);
    }
}