using FollowingErrors.Entities;

namespace FollowingErrors.Dtos
{
    public class AddBugDto : BaseEntity
    {
        public required string Description { get; set; }

        public int UserId { get; set; }
        public int ProjectId { get; set; }
    }
}
