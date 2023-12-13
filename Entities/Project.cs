namespace FollowingErrors.Entities
{
    public class Project : BaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<Bug> Bugs { get; set; } = new HashSet<Bug>();
    }
}
