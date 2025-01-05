using System.ComponentModel.DataAnnotations;

namespace DeveloperChallenges.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // Note: Use hashing for storing passwords securely

        public string role { get; set; }
    }
}
