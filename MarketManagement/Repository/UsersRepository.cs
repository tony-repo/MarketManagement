using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MarketManagement.Model.Domain;
using MarketManagement.Persistence;

namespace MarketManagement.Repository
{
    public interface IUsersRepository
    {
        Task CreateUser(User user);
    }

    public class UsersRepository : IUsersRepository
    {
        private const string _connectionStrName = "MarketManagement";
        
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public UsersRepository(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task CreateUser(User user)
        {
            using var dbConnection = _sqlConnectionFactory.GetDbConnection(_connectionStrName);
            await dbConnection.ExecuteAsync("", user, commandType: CommandType.StoredProcedure);
        }
    }
}
