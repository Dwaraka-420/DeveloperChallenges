using System.ComponentModel.DataAnnotations;

namespace DeveloperChallenges.Models
{
    public class UserRegistrationDto
    {
        [Key]
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }

    public class UserLoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
