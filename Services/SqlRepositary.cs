using Azure.Identity;
using DeveloperChallenges.Models;

namespace DeveloperChallenges.Services
{
    public class SqlRepositary
    {
        public readonly string _connectionString;

        public SqlRepositary(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public SqlRepositary(string connectionString)
        {
            _connectionString = connectionString;
        }

        
    }
}
