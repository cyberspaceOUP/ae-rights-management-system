//Created by sanjeet 
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Master
{
    public partial class ExecutiveLoginHistory : BaseEntity 
    {
        public string ExecutiveUserName { get; set;}
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
       
    }
}
