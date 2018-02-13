//Created by sanjeet 
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Master
{
    public partial class PageAccessMaster : BaseEntity
    {
        public string UserName { get; set; }
        public string PageUrl { get; set; }
        
    }
}
