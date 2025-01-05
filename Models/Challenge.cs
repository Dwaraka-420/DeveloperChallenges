using System.ComponentModel.DataAnnotations;

namespace DeveloperChallenges.Models
{
    public class Challenge
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }   
        public string Description { get; set; }
        public string CreatedBy { get; set; } // Email or UserId
        public DateTime CreatedAt { get; set; }

        public string Category { get; set;}

        //public ICollection<ChallengeReplay> Replies { get; set; }
    }
}
