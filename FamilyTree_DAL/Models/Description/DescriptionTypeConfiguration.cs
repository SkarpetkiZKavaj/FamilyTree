using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyTree_DAL.Models;

public class DescriptionTypeConfiguration : IEntityTypeConfiguration<Description>
{
    public void Configure(EntityTypeBuilder<Description> builder)
    {
        builder.HasOne<Person>(d => d.Person).
                WithOne(p => p.Description).
                HasForeignKey<Description>(d => d.PersonId);
    }
}