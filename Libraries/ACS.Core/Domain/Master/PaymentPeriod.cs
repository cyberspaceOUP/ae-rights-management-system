﻿using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ACS.Core.Domain.Master
{
    public partial class PaymentPeriod :BaseEntity , ILocalizedEntity 
    {
        public string PaymentType { get; set; }

        public int PeriodValueId { get; set; }
    }
}
