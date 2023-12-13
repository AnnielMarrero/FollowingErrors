using FollowingErrors.Entities;

namespace FollowingErrors.Dtos
{
    public class BugDto : BaseEntity
    {
        public required string Description { get; set; }
        public required string UserName { get; set; }
        public required string Project { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
