using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyTree_DAL.Models;

public class PersonTypeConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasOne<Description>(p => p.Description).
            WithOne(d => d.Person).
            HasForeignKey<Description>(d => d.PersonId);
    }
}