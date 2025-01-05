using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DeveloperChallenges.Models
{
    // Inherit from IdentityDbContext for Identity integration
    public class ApplicationContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRegistrationDto> UserRegistrationDtos { get; set; }

        public DbSet<ChallengeReplay> challengeReplays { get; set; }

        public DbSet<Challenge> challenges { get; set; }
    }
}
