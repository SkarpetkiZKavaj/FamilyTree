using FamilyTree_BAL.DTO;
using FluentValidation;

namespace FamilyTree_BAL.Validators;

public class TreeDTOValidator : AbstractValidator<TreeDTO>
{
    public TreeDTOValidator()
    {
        RuleFor(x => x.OwnerId).Matches(@"\S{8}-\S{4}-\S{4}-\S{4}-\S{12}");
        RuleFor(x => x.Name).Length(0, 50);
    }
}