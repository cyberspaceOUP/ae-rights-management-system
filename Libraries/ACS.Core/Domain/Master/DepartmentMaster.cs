using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Master
{
    public partial class DepartmentMaster : BaseEntity, ILocalizedEntity
    {
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
    }
}
