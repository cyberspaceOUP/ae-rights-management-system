//create by Saddam on 02/05/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Master
{
    public partial class GeographicalMaster : BaseEntity, ILocalizedEntity
    {
        public string geogcode { get; set; }
        public string geogName { get; set; }
        public string geogtype { get; set; }
        public int? parentid { get; set; }
        //public virtual GeographicalMaster GeographicalM { get; set; }

    }
}
