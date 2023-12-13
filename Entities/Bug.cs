using System.ComponentModel.DataAnnotations;

namespace FollowingErrors.Entities
{
    public class Bug : BaseEntity
    {
        [MaxLength(100)]
        public required string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; } = default!;

        public int ProjectId { get; set; }
        public virtual Project Project { get; set; } = default!;
    }
}
