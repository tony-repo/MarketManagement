using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MarketManagement.Configuration;
using MarketManagement.Model;
using MarketManagement.Model.Domain;
using MarketManagement.Model.Request;
using MarketManagement.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MarketManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly JwtSettingOptions _jwtSettingOptions;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUsersService _usersService;
        private readonly ILogger _logger;

        public AuthenticationController(IOptions<JwtSettingOptions> jwtSettingOptions, IJwtTokenService jwtTokenService, IUsersService usersService,
            ILogger<AuthenticationController> logger)
        {
            _jwtSettingOptions = jwtSettingOptions.Value;
            _jwtTokenService = jwtTokenService;
            _usersService = usersService;
            _logger = logger;
        }


        [HttpGet]
        [Route("ping")]
        public async Task<IActionResult> GetPing()
        {
            _logger.LogInformation("Get ping successfully");
            return await Task.FromResult(Ok("ping successfully"));
        }

        [HttpPost]
        [Route("signUp")]
        public async Task<IActionResult> SignUp(CreateUserRequest createUserRequest)
        {
            var user = new User
            {
                FirstName = createUserRequest.FirstName,
                LastName = createUserRequest.LastName,
                Email = createUserRequest.Email,
                Phone = createUserRequest.Phone,
                OrganizationId = null,
                OrganizationName = createUserRequest.OrganizationName,
                Password = createUserRequest.Password
            };

            await _usersService.CreateUser(user);

            return Ok(HttpStatusCode.Accepted);
        }

        [HttpPost]
        [Route("token")]
        public async Task<ActionResult> RequestToken(LoginRequest loginRequest)
        {
            var user = new User()
            {
                Email = loginRequest.UserName,
                Password = loginRequest.Password
            };

            if (!await _usersService.IsAuthedUser(user))
            {
                return Unauthorized("Email or Password is not correct.");
            }

            var refreshToken = "1123345";

            var token = await _jwtTokenService.CreateToken(loginRequest.UserName);

            return Ok(new { jwtToken = token, jwtRefreshToken = refreshToken });
        }
    }
}
