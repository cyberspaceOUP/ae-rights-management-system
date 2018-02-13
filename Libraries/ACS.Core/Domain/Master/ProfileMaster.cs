//Create by Saddam on 2904/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Master
{
    public partial class ProfileMaster : BaseEntity, ILocalizedEntity
    {
        public string Profilecode { get; set; }
        public string ProfileName { get; set; }
        public int Departmentid { get; set; }


    }
}
