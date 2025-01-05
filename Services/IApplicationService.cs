using DeveloperChallenges.DTO;
using DeveloperChallenges.Models;

namespace DeveloperChallenges.Services
{
    public interface IApplicationService
    {
        //Get a list of Challenges
        List<Challenge> GetChallenges();

        ResponseType AddChallenege(Challenge challenge);

        List<Challenge> GetChallengesByUser(string username);

        Challenge GetChallengeById(int Id);

        List<Challenge> GetChallengeByCategory(string category);

        List<Category_DTO> GetDistinctCategories();

        ResponseType AddReplayToChallenge(ChallengeReplay challengeReplay);

    }
}
