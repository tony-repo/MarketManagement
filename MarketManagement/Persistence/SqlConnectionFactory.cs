using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MarketManagement.Persistence
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetDbConnection(string name);
    }

    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public SqlConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetDbConnection(string name)
        {
            var connectionStr = _configuration.GetConnectionString(name);
            return new SqlConnection(connectionStr);
        }
    }
}
