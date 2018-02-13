//create by Saddam on 02/05/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Master
{
    public partial   class SeriesMaster : BaseEntity, ILocalizedEntity
    {
        public int? divisionid { get; set; }
        public int? Subdivisionid { get; set; }
        public virtual DivisionMaster DivisionM { get; set; }

        public string Seriesname { get; set; }
    }
}
