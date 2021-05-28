
Implement Jwt validation in .Net core
===============================================================

Note: we need to add below libraries firstly

``` ps
    Install-Package Microsoft.AspNetCore.Authentication.JwtBearer
    Install-Package System.IdentityModel.Tokens.Jwt
```

1 Set up validation in ConfigureServices

``` c#

        // Jwt authentication
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(option =>
            {
                // comment it, since if the server is used in internal network, it will need to set to false
                //option.RequireHttpsMetadata = false;

                var setting = _configuration.GetSection("JwtSettingsOptions").Get<JwtSettingOptions>();

                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(setting.SecretKey)),
                    ValidIssuer = setting.Issuer,
                    ValidateIssuer = true,
                    ValidAudience = setting.Audience,
                    ValidateAudience = false
                };
            });

```

2 Enable authorization and authentication

``` C#
    app.UseAuthentication();
    app.UseAuthorization();
```

3 Add request token API:

``` c#
        [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly JwtSettingOptions _jwtSettingOptions;

        public AuthenticationController(IOptions<JwtSettingOptions> jwtSettingOptions)
        {
            _jwtSettingOptions = jwtSettingOptions.Value;
        }

        [HttpPost]
        [Route("token")]
        public ActionResult RequestToken(LoginRequest loginRequest)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, loginRequest.UserName),
                new Claim(ClaimTypes.Role, "Admin"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettingOptions.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(_jwtSettingOptions.Issuer, _jwtSettingOptions.Audience, claims, expires: DateTime.UtcNow.AddMinutes(_jwtSettingOptions.AccessExpiration), signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            var refreshToken = "1123345";

            return Ok(new[] { token, refreshToken });
        }
    }

```

4 Add authentication in swagger

``` c#
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "MarketManagement", Version = "v1" });

        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "",
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });

    });
```

Enable migrations for project
================================================

1. Install Microsoft.EntityFrameworkCore.Tools to project

2. Go to Tools -> Nuget Package Manager -> Package Manage Console

3. Use below command to run migrations

``` bash

    Enable-Migrations
    add-Migration modelupdate_2021_05_25 -Context MySqlDbContext

```
