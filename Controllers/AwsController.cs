using Amazon;
using Amazon.Runtime;
using Amazon.SecurityToken;
using Amazon.SecurityToken.Model;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace DeveloperChallenges.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AwsController : ControllerBase
    {
        private readonly string _awsAccessKeyId = ""; // Replace with your AWS Access Key ID
        private readonly string _awsSecretAccessKey = ""; // Replace with your AWS Secret Access Key
        private readonly RegionEndpoint _region = RegionEndpoint.USEast1; // Replace with your AWS Region

        [HttpGet("check-auth")]
        public async Task<IActionResult> CheckAuthenticationAsync()
        {
            try
            {
                var credentials = new BasicAWSCredentials(_awsAccessKeyId, _awsSecretAccessKey);

                using (var stsClient = new AmazonSecurityTokenServiceClient(credentials, _region))
                {
                    var response = await stsClient.GetCallerIdentityAsync(new GetCallerIdentityRequest());

                    return Ok(new
                    {
                        Message = "AWS Authentication Successful",
                        AccountId = response.Account,
                        UserId = response.UserId,
                        Arn = response.Arn
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "AWS Authentication Failed",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("get-secret")]
        public async Task<IActionResult> GetSecretAsync()
        {
            try
            {
                var credentials = new BasicAWSCredentials(_awsAccessKeyId, _awsSecretAccessKey);

                using (var secretsClient = new AmazonSecretsManagerClient(credentials, _region))
                {
                    var secretName = "power-team-credentials";
                    var secretResponse = await secretsClient.GetSecretValueAsync(new GetSecretValueRequest
                    {
                        SecretId = secretName
                    });

                    if (secretResponse.SecretString != null)
                    {
                        var secretData = JsonSerializer.Deserialize<SecretData>(secretResponse.SecretString);
                        Mail.NotifyHrTeam(secretData.poweruser_username, secretData.poweruser_password);
                        return Ok(new
                        {
                            Username = secretData.poweruser_username,
                            Password = secretData.poweruser_password
                        });
                    }
                    else
                    {
                        return StatusCode(500, new
                        {
                            Message = "Secret is stored as binary, which is not supported in this example."
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Failed to Retrieve Secret",
                    Error = ex.Message
                });
            }
        }

        private class SecretData
        {
            public string poweruser_username { get; set; }
            public string poweruser_password { get; set; }
        }
    }
}
