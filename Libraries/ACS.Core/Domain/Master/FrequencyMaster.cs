//Create by Ankush 10/08/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Master
{
    public partial class FrequencyMaster : BaseEntity, ILocalizedEntity 
    {
        public string Frequency { get; set; }
    }
}
