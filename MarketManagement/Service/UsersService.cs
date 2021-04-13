using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketManagement.Model.Domain;

namespace MarketManagement.Service
{
    public interface IUsersService
    {
        Task CreateUser(User user);
    }

    public class UsersService : IUsersService
    {
        public Task CreateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
