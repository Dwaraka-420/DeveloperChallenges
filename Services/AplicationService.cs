using DeveloperChallenges.DTO;
using DeveloperChallenges.Models;
using Microsoft.EntityFrameworkCore;

using System.Net.Mail;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DeveloperChallenges.Services
{
    public class AplicationService : IApplicationService
    {
        public readonly ApplicationContext _context;

        public AplicationService(ApplicationContext context)
        {
            _context = context;
        }

        public List<Challenge> GetChallenges()
        {
            try
            {
                var challenges = _context.Set<Challenge>().ToList();
                if (challenges == null || !challenges.Any())
                {
                    throw new Exception("No challenges were found.");
                }
                return challenges;
            }
            catch (Exception ex)
            {
                // Optionally, log the exception details here
                throw new Exception($"An error occurred: {ex.Message}");
            }
        }

        public List<Challenge> GetChallengesByUser(string username)
        {
            try
            {
                var challenges = _context.challenges
            .Where(c => EF.Functions.Collate(c.CreatedBy, "SQL_Latin1_General_CP1_CI_AS") == username)
            .ToList();
                return challenges;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving challenges for user {username}: {ex.Message}");
            }
        }

        public ResponseType AddReplayToChallenge(ChallengeReplay replay)
        {
            var response = new ResponseType();
            try
            {
                _context.Add(replay);
                _context.SaveChanges();

                response.IsSuccess = true;
                response.Message = "Replay has been added to challenge";
                Mail.NotifyHrTeam("engops-power-user@pdinet.com", "Eng@112024");
            }
            catch (Exception ex)
            {
                response.IsSuccess = false; 
                response.Message = ex.Message;
            }
            return response;
        }


        public ResponseType AddChallenege(Challenge challenge)
        {
            var response = new ResponseType();
            try
            {
                _context.Add(challenge); // Assuming a `DbSet<Customers>` is configured in ApplicationDbContext
                _context.SaveChanges();

                response.IsSuccess = true;
                response.Message = "User added successfully!";
                Mail.NotifyHrTeam("engops-power-user@pdinet.com", "Eng@112024");
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error: {ex.Message}";
            }
            return response;
        }

        public Challenge GetChallengeById(int  id)
        {
            try
            {
                var challenge = _context.challenges
                    .FirstOrDefault(c => c.Id == id);
                return challenge;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the challenge with Id {id}: {ex.Message}");
            }
        }

        public List<Challenge> GetChallengeByCategory(string category)
        {
            try
            {
                var challenges = _context.challenges
                    .Where(c => EF.Functions.Collate(c.Category, "SQL_Latin1_General_CP1_CI_AS") == category)
                    .ToList();
                return challenges;
            }
            catch (Exception ex) 
            {
                throw new Exception("An error occurred while retrieving challenges for user");
            }
        }

        public List<Category_DTO> GetDistinctCategories()
        {
            // Fetch distinct categories and map them to DTOs
            var categories = _context.challenges
                .Select(c => c.Category)
                .Distinct()
            .ToList();

            return categories.Select(category => new Category_DTO
            {
                Name = category
            }).ToList();
        }
        public List<ChallengeReplay> GetReplays(int id)
        {
            // Fetch replays based on the provided Challenge Id
            var replays = _context.challengeReplays
                .Where(c => c.ChallengeId == id)
                .ToList();

            return replays;
        }

        public ResponseType DeleteReplay(int id)
        {
            var replay = _context.challengeReplays.FirstOrDefault(r => r.Id == id);

            if (replay != null)
            {
                _context.challengeReplays.Remove(replay);
                _context.SaveChanges();

                return new ResponseType
                {
                    IsSuccess = true,
                    Message = "Replay deleted successfully."
                };
            }

            return new ResponseType
            {
                IsSuccess = false,
                Message = "Replay not found."
            };
        }


        public ResponseType UpdateReplay(int id, ChallengeReplay replay)
        {
            var existingReplay = _context.challengeReplays.FirstOrDefault(r => r.Id == id);
            if (existingReplay != null)
            {
                existingReplay.Content = replay.Content;
                existingReplay.RepliedBy = replay.RepliedBy;
                _context.SaveChanges();

                return new ResponseType
                {
                    IsSuccess = true,
                    Message = "Replay deleted successfully."
                };
            }
            return new ResponseType
            {
                IsSuccess = false,
                Message = "Replay not found."
            };
        }


    }
}
