using AutoFixture;
using System;
using System.Threading.Tasks;
using MarketManagement.Controllers;
using MarketManagement.Model;
using MarketManagement.Service;
using NSubstitute;
using Xunit;
using FluentAssertions;
using MarketManagement.Model.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Tests.Controllers
{
    public class UsersTests : BaseTests
    {
        private readonly UsersController _usersController;
        private readonly IUsersService _usersService;
        public UsersTests()
        {
            _usersService = Substitute.For<IUsersService>();
            var logger = Substitute.For<ILogger<UsersController>>();
            _usersController = new UsersController(_usersService, logger);
        }

        [Fact]
        public async Task GivenCreateUserRequest_CreateUser_CreateUserSuccess()
        {
            var request = Fixture.Create<CreateUserRequest>();
            var result = await _usersController.CreateUser(request);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
