using System.Threading.Tasks;
using DeveloperChallenges.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using DeveloperChallenges.Models;

namespace DeveloperChallenges.Services
{
    public class UserService
    {
        private readonly ApplicationContext _context;

        public UserService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task RegisterUserAsync(string username, string password, string email, string role)
        {
            // Hash the password
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
            numBytesRequested: 256 / 8));

            var user = new User
            {
                Username = username,
                Password = hashed,
                Email = email,
                role = role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
            if (user == null)
                return null;

            // Hash the input password with the same salt and compare
            byte[] salt = new byte[128 / 8]; // Retrieve the stored salt
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            if (user.Password == hashed)
                return null;

            return user;
        }
    }
}
