using FamilyTree_BAL.DTO;
using FluentValidation;

namespace FamilyTree_BAL.Validators;

public class PersonDTOValidator : AbstractValidator<PersonDTO>
{
    public PersonDTOValidator()
    {
        RuleFor(x => x.OwnerId).Matches(@"\S{8}-\S{4}-\S{4}-\S{4}-\S{12}");
        RuleFor(x => x.Age).GreaterThan(0);
        RuleFor(x => x.Description).NotNull();
        RuleFor(x => x.FirstName).Length(0, 50);
        RuleFor(x => x.LastName).Length(0, 50);
    }
}