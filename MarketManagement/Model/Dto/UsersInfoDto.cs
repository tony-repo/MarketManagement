using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketManagement.Model.Dto
{
    public class UsersInfoDto
    {
        public string Name { get; set; }
        public  string Email { get; set; }
        public  string Phone { get; set; }
        public string OrganizationName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
