using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MarketManagement.Configuration;
using MarketManagement.Model;
using MarketManagement.Service;
using Microsoft.AspNetCore.Mvc;
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

        public AuthenticationController(IOptions<JwtSettingOptions> jwtSettingOptions, IJwtTokenService jwtTokenService)
        {
            _jwtSettingOptions = jwtSettingOptions.Value;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost]
        [Route("token")]
        public async Task<ActionResult> RequestToken(LoginRequest loginRequest)
        {

            if (loginRequest.UserName != "123456" && loginRequest.Password != "123456")
            {
                return Unauthorized("UserName or Password is not correct.");
            }

            var refreshToken = "1123345";

            var token = await _jwtTokenService.CreateToken(loginRequest.UserName);

            return Ok(new { jwtToken = token, jwtRefreshToken = refreshToken });
        }
    }
}
