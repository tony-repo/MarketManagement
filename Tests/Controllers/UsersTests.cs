using AutoFixture;
using System;
using System.Threading.Tasks;
using MarketManagement.Controllers;
using MarketManagement.Model;
using MarketManagement.Service;
using NSubstitute;
using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Tests.Controllers
{
    public class UsersTests : BaseTests
    {
        private readonly UsersController _usersController;
        private readonly IUsersService _usersService;
        public UsersTests()
        {
            _usersService = Substitute.For<IUsersService>();
            _usersController = new UsersController(_usersService);
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
