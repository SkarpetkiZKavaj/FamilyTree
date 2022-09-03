using FamilyTree_BAL.DTO;
using FluentValidation;

namespace FamilyTree_BAL.Validators;

public class DescriptionDTOValidator : AbstractValidator<DescriptionDTO>
{
    public DescriptionDTOValidator()
    {
        RuleFor(x => x.OwnerId).Matches(@"\S{8}-\S{4}-\S{4}-\S{4}-\S{12}");
        RuleFor(x => x.History).Length(0, 1000);
    }
}