using FluentValidation;
using FollowingErrors.Dtos;
using Microsoft.EntityFrameworkCore;

namespace FollowingErrors.Validators
{
    public class BugValidator : AbstractValidator<AddBugDto>
    {
        readonly BugsManager _db;

        public BugValidator(BugsManager db)
        {
            _db = db;
            RuleFor(dto => dto.Description).NotEmpty().NotNull().MaximumLength(100);

            RuleFor(x => x.UserId).Must((id) => AnyUser(id).Result).WithMessage("User not found");

            RuleFor(x => x.ProjectId)
                .Must((id) => AnyProject(id).Result)
                .WithMessage("Project Not found");
        }

        public async Task<bool> AnyUser(int id) => await _db.User.AnyAsync(_ => _.Id == id);

        public async Task<bool> AnyProject(int id) => await _db.Project.AnyAsync(_ => _.Id == id);
    }
}
