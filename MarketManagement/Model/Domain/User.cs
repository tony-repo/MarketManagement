using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketManagement.Model.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public string OrganizationName { get; set; }

        public Guid? OrganizationId { get; set; }

        public List<Role> Roles { get; set; }
    }
}
