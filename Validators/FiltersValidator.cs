using FluentValidation;
using FollowingErrors.Dtos;

namespace FollowingErrors.Validators
{
    public class FiltersValidator : AbstractValidator<BugFilter>
    {
        public FiltersValidator()
        {
            RuleFor(dto => dto)
                .Must(
                    (dto) =>
                        dto.ProjectId != null
                        || dto.UserId != null
                        || dto.StartDate != null
                        || dto.EndDate != null
                )
                .WithMessage("You must provide at least one filter for list the bugs");
        }
    }
}
