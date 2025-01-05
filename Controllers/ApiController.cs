using DeveloperChallenges.DTO;
using DeveloperChallenges.Models;
using DeveloperChallenges.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeveloperChallenges.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApiController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet("Challenges")]
        public IActionResult GetChallenges()
        {
            try
            {
                var Challenge = _applicationService.GetChallenges();
                return Ok(Challenge);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("ChallengesByUser")]
        public IActionResult GetChallengesByUser([FromQuery] string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("Username is required.");
            }

            try
            {
                var challenges = _applicationService.GetChallengesByUser(username);
                if (challenges == null || challenges.Count == 0)
                {
                    return NotFound("No challenges found for the specified user.");
                }
                return Ok(challenges);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("AddChallengeToReplay")]
        public IActionResult AddReplayToChallenge([FromBody]ChallengeReplay challengeReplay)
        {
            try
            {
                var response = _applicationService.AddReplayToChallenge(challengeReplay);
                return Ok(new { success = true, message = "Replay added to challenge" });
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost("AddChallenge")]
        public IActionResult AddChallenge([FromBody]Challenge challenge)
        {
            try
            {
                var response = _applicationService.AddChallenege(challenge);
                return Ok(new { success = true, message = "Challenge added successfully!" });
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpGet("GetChallengeById/{id}")]
        public IActionResult GetChallengeById(int id)
        {
            try
            {
                var challenge = _applicationService.GetChallengeById(id);
                if (challenge == null)
                {
                    return NotFound($"Challenge with Id {id} not found.");
                }
                return Ok(challenge);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetChallengeByCategory")]
        public IActionResult GetChallengeByCategory([FromQuery] string category)
        {
            try
            {
                var challenge = _applicationService.GetChallengeByCategory(category);
                if (challenge == null)
                {
                    return NotFound("There were no challenges");
                }
                return Ok(challenge);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpGet("GetCategories")]
        public IActionResult GetCategories()
        {
            var categories = _applicationService.GetDistinctCategories();
            return Ok(categories);
        }
    }
}
