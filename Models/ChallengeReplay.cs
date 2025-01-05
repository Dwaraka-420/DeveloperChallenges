namespace DeveloperChallenges.Models
{
    public class ChallengeReplay
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string RepliedBy { get; set; } // Email or UserId
        public DateTime RepliedAt { get; set; }
        public int ChallengeId { get; set; }
        //public Challenge Challenge { get; set; }

    }
}
