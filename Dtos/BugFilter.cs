namespace FollowingErrors.Dtos
{
    public class BugFilter
    {
        public int? ProjectId { get; set; }
        public int? UserId { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
