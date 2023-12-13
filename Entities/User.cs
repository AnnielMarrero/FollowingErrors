namespace FollowingErrors.Entities
{
    public class User : BaseEntity
    {
        public required string Name { get; set; }

        public required string Surname { get; set; }

        public ICollection<Bug> Bugs { get; set; } = new HashSet<Bug>();
    }
}
