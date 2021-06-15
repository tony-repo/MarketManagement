using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MarketManagement.Model;
using MarketManagement.Model.Domain;
using MarketManagement.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace MarketManagement.Service
{
    public interface IUsersService
    {
        Task CreateUser(User user);
        Task<IEnumerable<User>> GetUsersInfo();
        Task<User> GetUser(Guid userId);
        Task<bool> IsAuthedUser(User user);
    }

    public class UsersService : IUsersService
    {
        private readonly MySqlDbContext _dbContext;

        private readonly IMapper _mapper;

        public UsersService(MySqlDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task CreateUser(User user)
        {
            try
            {
                var organizationEntity = new OrganizationEntity()
                {
                    Id = user.OrganizationId ?? Guid.NewGuid(),
                    Name = user.OrganizationName
                };

                user.OrganizationId = organizationEntity.Id;
                var userEntity = _mapper.Map<UserEntity>(user);

                await _dbContext.Organizations.AddAsync(organizationEntity);
                await _dbContext.Users.AddAsync(userEntity);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<User> GetUser(Guid userId)
        {
            var userEntities = await _dbContext.Users.ToListAsync();
            var user = _mapper.Map<User>(userEntities.FirstOrDefault());
            return user;
        }

        public async Task<IEnumerable<User>> GetUsersInfo()
        {
            try
            {
                var userEntities = await _dbContext.Users.ToListAsync();
                var organizations = await _dbContext.Organizations.ToListAsync();
                var result = userEntities?.Select(entity => _mapper.Map<User>(entity))
                     .Select(entity =>
                     {
                         entity.OrganizationName =
                                 organizations.FirstOrDefault(o => o.Id == entity.OrganizationId)?.Name;
                         return entity;
                     });
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> IsAuthedUser(User user)
        {
            var result = await _dbContext.Users.FirstOrDefaultAsync(u =>
                u.Email.ToLower() == user.Email.ToLower() && u.Password == user.Password);
            if (result == null)
            {
                return false;
            }

            return true;
        }
    }
}
