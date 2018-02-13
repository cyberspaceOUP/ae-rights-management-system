//Create by Saddam on 04/05/2016
using ACS.Core.Domain.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ACS.Core.Domain.Master
{
    public partial class DivisionMaster : BaseEntity, ILocalizedEntity
    {
        public string divisionName { get; set; }
        public int divisionlevel { get; set; }


        public int? parentdivisionid { get; set; }
        public virtual DivisionMaster parentdivision { get; set; }

        }
}
