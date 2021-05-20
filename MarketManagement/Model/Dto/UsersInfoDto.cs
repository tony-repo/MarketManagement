﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketManagement.Model.Dto
{
    public class UsersInfoDto
    {
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CompanyName { get; set; }

        public string CompanyAddress { get; set; }

        public string Comment { get; set; }
    }
}