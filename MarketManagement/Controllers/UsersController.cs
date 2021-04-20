using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MarketManagement.Model;
using MarketManagement.Model.Domain;
using MarketManagement.Model.Dto;
using MarketManagement.Service;
using Microsoft.Extensions.Logging;

namespace MarketManagement.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet]
        [Route("ping")]
        public async Task<IActionResult> GetPing()
        {
            _logger.LogInformation("Get ping successfully");
            return Ok("Success");
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            _logger.LogInformation("Log in market management");

            var data = loginRequest;

            return Ok("Success for login");
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRequest createUserRequest)
        {
            var user = new User();
            user.FirstName = createUserRequest.FirstName;
            user.LastName = createUserRequest.LastName;
            user.Email = createUserRequest.Email;
            user.OrganizationId = createUserRequest.OrganizationId;

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
