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
    [Authorize]
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
                UserName = createUserRequest.UserName,
                OrganizationId = createUserRequest.OrganizationId,
                Password = createUserRequest.Password
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
