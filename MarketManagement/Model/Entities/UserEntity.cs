using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketManagement.Model.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public  string Phone { get; set; }

        public Guid OrganizationId { get; set; }
    }
}
