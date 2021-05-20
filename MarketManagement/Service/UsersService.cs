using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MarketManagement.Model;
using MarketManagement.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace MarketManagement.Service
{
    public interface IUsersService
    {
        Task CreateUser(User user);
        Task<IEnumerable<User>> GetUsersInfo();
    }

    public class UsersService : IUsersService
    {
        private readonly SqlServerDbContext _dbContext;

        private readonly IMapper _mapper;

        public UsersService(SqlServerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Task CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetUsersInfo()
        {
            var userEntities = await _dbContext.Users.ToListAsync();
            var test = _mapper.Map<User>(userEntities.FirstOrDefault());
            return userEntities?.Select(entity => _mapper.Map<User>(entity));
        }
    }
}
