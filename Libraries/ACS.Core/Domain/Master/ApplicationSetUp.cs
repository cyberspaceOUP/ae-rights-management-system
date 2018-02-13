//Create by Saddam on 30/05/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Master
{
    
    public partial class ApplicationSetUp : BaseEntity, ILocalizedEntity
    {
        public string key { get; set; }
        public string   keyValue { get; set; }
        public string keyStatus { get; set; }
        public string keyDescription { get; set; }
     
     
    }
}
