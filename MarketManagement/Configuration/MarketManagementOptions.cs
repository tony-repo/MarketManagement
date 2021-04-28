using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketManagement.Configuration
{
    public class MarketManagementOptions
    {
        public string MachineName { get; set; }
        public JwtSettingOptions JwtSettings { get; set; }
    }
}
