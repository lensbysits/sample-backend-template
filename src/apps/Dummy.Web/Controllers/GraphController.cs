using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Dummy.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class GraphController : ControllerBase
    {
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly GraphServiceClient _graphServiceClient;
        private readonly IOptions<MicrosoftGraphOptions> _graphOptions;

        public GraphController(ITokenAcquisition tokenAcquisition, GraphServiceClient graphServiceClient, IOptions<MicrosoftGraphOptions> graphOptions)
        {
            _tokenAcquisition = tokenAcquisition;
            _graphServiceClient = graphServiceClient;
            _graphOptions = graphOptions;
        }

        [HttpGet("me/memberof")]
        public async Task<ActionResult> GetMeMemberOf()
        {
            var groups = await _graphServiceClient.Me.MemberOf.Request().GetAsync();
            return Ok(groups);
        }

        [HttpGet("users")]
        public async Task<ActionResult> GetUsers()
        {
            try
            {
                var users = await _graphServiceClient.Users
                    .Request()
                    .GetAsync();

                return Ok(users);

            }
            catch (MsalException ex)
            {
                HttpContext.Response.ContentType = "text/plain";
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await HttpContext.Response.WriteAsync("An authentication error occurred while acquiring a token for downstream API\n" + ex.ErrorCode + "\n" + ex.Message);
            }
            catch (Exception ex)
            {
                if (ex.InnerException is MicrosoftIdentityWebChallengeUserException challengeException)
                {
                    _tokenAcquisition.ReplyForbiddenWithWwwAuthenticateHeader(_graphOptions.Value.Scopes.Split(' '),
                        challengeException.MsalUiRequiredException);
                }
                else
                {
                    return BadRequest("An error occurred while calling the downstream API\n" + ex.Message);
                }
            }

            return Ok();
        }

        [HttpGet("users/{userId}")]
        public async Task<ActionResult> GetUser(string userId)
        {
            try
            {
                var user = await _graphServiceClient.Users[userId].Request()
                    .GetAsync();

                return Ok(user);

            }
            catch (MsalException ex)
            {
                HttpContext.Response.ContentType = "text/plain";
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await HttpContext.Response.WriteAsync("An authentication error occurred while acquiring a token for downstream API\n" + ex.ErrorCode + "\n" + ex.Message);
            }
            catch (Exception ex)
            {
                if (ex.InnerException is MicrosoftIdentityWebChallengeUserException challengeException)
                {
                    _tokenAcquisition.ReplyForbiddenWithWwwAuthenticateHeader(_graphOptions.Value.Scopes.Split(' '),
                        challengeException.MsalUiRequiredException);
                }
                else
                {
                    return BadRequest("An error occurred while calling the downstream API\n" + ex.Message);
                }
            }

            return Ok();
        }

        [HttpPut("users/{userId}")]
        public async Task<ActionResult> UpdateUsers(string userId, string jobTitle)
        {
            try
            {
                var url = _graphServiceClient.Users[userId].Extensions.RequestUrl;
                var result = await _graphServiceClient.Users[userId].Request().UpdateAsync(new User
                {
                    JobTitle = jobTitle,
                    AdditionalData = new Dictionary<string, object>
                    {
                        { "extbj77yqbd_umi", new { isUmiUser = true, isUmiAdmin = true, basesManaged = "abc,def,ghi" } }
                    }
                });


                var user = await _graphServiceClient.Users[userId].Request().Expand(user => user.Extensions).GetAsync();
                return Ok(user);
            }
            catch (MsalException ex)
            {
                HttpContext.Response.ContentType = "text/plain";
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await HttpContext.Response.WriteAsync("An authentication error occurred while acquiring a token for downstream API\n" + ex.ErrorCode + "\n" + ex.Message);
            }
            catch (Exception ex)
            {
                if (ex.InnerException is MicrosoftIdentityWebChallengeUserException challengeException)
                {
                    _tokenAcquisition.ReplyForbiddenWithWwwAuthenticateHeader(_graphOptions.Value.Scopes.Split(' '),
                        challengeException.MsalUiRequiredException);
                }
                else
                {
                    return BadRequest("An error occurred while calling the downstream API\n" + ex.Message);
                }
            }

            return Ok();
        }

        [HttpGet("schemaextentions")]
        public async Task<ActionResult> GetSchemaExtentions(string select = null)
        {
            var query = _graphServiceClient.SchemaExtensions.Request();

            if (!string.IsNullOrEmpty(select))
            {
                query = query.Select(select);
            }

            var users = await query.GetAsync();
            return Ok(users);
        }

        [HttpPost("schemaextentions")]
        public async Task<ActionResult> AddSchemaExtentions()
        {
            try
            {
                var schemaExtension = new SchemaExtension
                {
                    Id = "umi",
                    Description = "UMI related information",
                    TargetTypes = new List<String>()
                    {
                        "User"
                    },
                        Properties = new List<ExtensionSchemaProperty>()
                    {
                        new ExtensionSchemaProperty
                        {
                            Name = "isUmiUser",
                            Type = "Boolean"
                        },
                        new ExtensionSchemaProperty
                        {
                            Name = "isUmiAdmin",
                            Type = "Boolean"
                        },
                        new ExtensionSchemaProperty
                        {
                            Name = "basesManaged",
                            Type = "String"
                        }
                    }
                };

                var query = _graphServiceClient.SchemaExtensions.Request();
                var extention = await query.AddAsync(schemaExtension);

                return Ok(extention);
            }
            catch (MsalException ex)
            {
                HttpContext.Response.ContentType = "text/plain";
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await HttpContext.Response.WriteAsync("An authentication error occurred while acquiring a token for downstream API\n" + ex.ErrorCode + "\n" + ex.Message);
            }
            catch (Exception ex)
            {
                if (ex.InnerException is MicrosoftIdentityWebChallengeUserException challengeException)
                {
                    _tokenAcquisition.ReplyForbiddenWithWwwAuthenticateHeader(_graphOptions.Value.Scopes.Split(' '),
                        challengeException.MsalUiRequiredException);
                }
                else
                {
                    return BadRequest("An error occurred while calling the downstream API\n" + ex.Message);
                }
            }

            return Ok();
        }


        [HttpGet("groups")]
        public async Task<ActionResult> GetGroups()
        {
            var users = await _graphServiceClient.Groups.Request().GetAsync();
            return Ok(users);
        }
    }
}
