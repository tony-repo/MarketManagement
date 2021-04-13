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

namespace MarketManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        [Route("ping")]
        public async Task<IActionResult> GetPing()
        {
            return Ok("Success");
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
