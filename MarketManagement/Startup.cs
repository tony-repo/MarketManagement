using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MarketManagement.Configuration;
using MarketManagement.Model;
using MarketManagement.Model.AutoMapper;
using MarketManagement.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace MarketManagement
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUsersService, UsersService>();

            services.AddCors();

            var rootSection = _configuration.GetSection("MarketManagementOptions");
            services.Configure<MarketManagementOptions>(rootSection);

            var jwtSettingsSection = _configuration.GetSection("JwtSettingsOptions");
            services.Configure<JwtSettingOptions>(jwtSettingsSection);

            // Jwt authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    // comment it, since if the server is used in internal network, it will need to set to false
                    // option.RequireHttpsMetadata = false;

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


            // Oauth Authentication for testing we just use jwt token for authentication
            //IdentityModelEventSource.ShowPII = true; //Add this line for detail exception
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        //The url for IdentityServer
            //        options.Authority = "https://localhost:44302";
            //        //The ApiResource Name
            //        options.Audience = "market";
            //    });

            // For sql server
            //services.AddDbContext<SqlServerDbContext>(options =>
            //{
            //    options.UseSqlServer(_configuration.GetConnectionString("market"));

            //});

            services.AddDbContextPool<MySqlDbContext>(options =>
            {
                var version = ServerVersion.Parse("8.0.25", ServerType.MySql);
                string connectionStr = _configuration.GetConnectionString("market");

                options.UseMySql(connectionStr, version, (myOptions) =>
                {

                });
            });

            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IJwtTokenService, JwtTokenService>();
            services.AddSingleton<IMapper>(_ => new Mapper(MapperSetup.MapperConfiguration));
            services.AddControllers();
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MarketManagement v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // Add jwt
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                //.AllowAnyOrigin()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
