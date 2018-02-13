//Created by sanjeet 
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Master
{
    public partial class LoginHistory :BaseEntity 
    {
       // public int Id { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
       // public DateTime ? Date { get; set; }
        public int Attempt { get; set; }
        
    }
}
