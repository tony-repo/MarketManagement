using MarketManagement.Model.Domain;
using MarketManagement.Model.Request;
using MarketManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using MarketManagement.Model.Dto;

namespace MarketManagement.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public UsersController(IUsersService usersService, ILogger<UsersController> logger, IMapper mapper)
        {
            _usersService = usersService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersInfo()
        {
            var usersInfo = await _usersService.GetUsersInfo();

            var result = usersInfo.Select(user => this._mapper.Map<UserDto>(user)) ;

            return Ok(result);
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRequest createUserRequest)
        {
            var user = new User
            {
                FirstName = createUserRequest.FirstName,
                LastName = createUserRequest.LastName,
                Email = createUserRequest.Email,
                Phone = createUserRequest.Phone,
                OrganizationId = createUserRequest.OrganizationId,
                OrganizationName = createUserRequest.OrganizationName,
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
