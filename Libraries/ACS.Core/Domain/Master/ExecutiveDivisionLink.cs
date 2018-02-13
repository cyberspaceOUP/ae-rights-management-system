//Create by saddam on 02/05/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Master
{
    public partial class ExecutiveDivisionLink : BaseEntity, ILocalizedEntity
    {
        public int executiveid { get; set; }
        public virtual ExecutiveMaster ExecutiveM { get; set; }

        public int divisionid { get; set; }
        public virtual DivisionMaster DivisionM { get; set; }




    }
}
