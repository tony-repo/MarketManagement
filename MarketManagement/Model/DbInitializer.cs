using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketManagement.Model.Entities;

namespace MarketManagement.Model
{
    public static class DbInitializer
    {
        public static void Initialize(MySqlDbContext context)
        {
            context.Database.EnsureCreated(); // make the database is created. 

            if (context.Users.Any())
            {
                return;
            }

            var user = new UserEntity
            {
                Id = Guid.NewGuid(),
                Email = "test@qq.com",
                FirstName = "tony",
                LastName = "schema",
                OrganizationId = Guid.Empty,
                Password = "123143423"
            };

            context.Users.Add(user);
            context.SaveChanges();
        }

    }
}
