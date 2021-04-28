using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MarketManagement.Configuration;
using MarketManagement.Model.Domain;
using MarketManagement.Model.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MarketManagement.Service
{
    public interface IJwtTokenService
    {
        Task<string> CreateToken(string userName);
        Task<string> RefreshToken();
        Task DeactiveActiveToken();
    }

    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtSettingOptions _jwtSettingOptions;
        public JwtTokenService(IOptions<JwtSettingOptions> jwtSettingOptions)
        {
            _jwtSettingOptions = jwtSettingOptions.Value;
        }

        public async Task<string> CreateToken(string userName)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, "Admin"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettingOptions.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(_jwtSettingOptions.Issuer, _jwtSettingOptions.Audience, claims, expires: DateTime.UtcNow.AddMinutes(_jwtSettingOptions.AccessExpiration), signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;
        }

        public Task<string> Create(User user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettingOptions.SecretKey));

            DateTime authTime = DateTime.UtcNow;
            DateTime expiresAt = authTime.AddMinutes(Convert.ToDouble(_jwtSettingOptions.RefreshExpiration));

            //将用户信息添加到 Claim 中
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);

            var role = new StringBuilder();
            user.Roles.ForEach(r => role.Append(r.RoleName + ","));

            IEnumerable<Claim> claims = new Claim[] {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,role.ToString().TrimEnd(',')),
                new Claim(ClaimTypes.Expiration,expiresAt.ToString())
            };
            identity.AddClaims(claims);

            //签发一个加密后的用户信息凭证，用来标识用户的身份
            //_httpContextAccessor.HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),//创建声明信息
                Issuer = _jwtSettingOptions.Issuer,//Jwt token 的签发者
                Audience = _jwtSettingOptions.Audience,//Jwt token 的接收者
                Expires = expiresAt,//过期时间
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)//创建 token
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            //存储 Token 信息
            var jwt = new
            {
                UserId = user.Id,
                Token = tokenHandler.WriteToken(token),
                Auths = new DateTimeOffset(authTime).ToUnixTimeSeconds(),
                Expires = new DateTimeOffset(expiresAt).ToUnixTimeSeconds(),
                Success = true
            };

            _tokens.Add(jwt);

            return jwt;
        }

        public Task DeactiveActiveToken()
        {
            throw new NotImplementedException();
        }

        public Task<string> RefreshToken()
        {
            throw new NotImplementedException();
        }
    }
}
