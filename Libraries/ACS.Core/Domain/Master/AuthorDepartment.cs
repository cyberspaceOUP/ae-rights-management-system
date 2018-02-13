//Create by Saddam on 09/06/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Master
{
    
    public partial class AuthorDepartment : BaseEntity, ILocalizedEntity
    {
        public string DepartmentName { get; set; }

    }
}
