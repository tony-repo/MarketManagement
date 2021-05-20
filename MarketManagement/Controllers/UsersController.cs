using MarketManagement.Model.Domain;
using MarketManagement.Model.Request;
using MarketManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MarketManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly ILogger _logger;

        public UsersController(IUsersService usersService, ILogger<UsersController> logger)
        {
            _usersService = usersService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        [Route("auth/ping")]
        public async Task<IActionResult> GetAuthPing()
        {
            _logger.LogInformation("Get ping successfully");
            return await Task.FromResult(Ok("auth ping Success"));
        }


        [HttpGet]
        [Route("ping")]
        public async Task<IActionResult> GetPing()
        {
            _logger.LogInformation("Get ping successfully");
            return await Task.FromResult(Ok("ping successfully"));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            _logger.LogInformation("Log in market management");

            var data = loginRequest;

            return Ok("Success for login");
        }


        [HttpGet]
        public async Task<IActionResult> GetUsersInfo()
        {
            var usersInfo = await _usersService.GetUsersInfo();

            return Ok(usersInfo);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRequest createUserRequest)
        {
            var user = new User
            {
                FirstName = createUserRequest.FirstName,
                LastName = createUserRequest.LastName,
                Email = createUserRequest.Email,
                OrganizationId = createUserRequest.OrganizationId
            };

            await _usersService.CreateUser(user);

            return Ok(HttpStatusCode.Accepted);
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            await Task.Delay(1000);
            return Ok(HttpStatusCode.Accepted);
        }
    }
}
