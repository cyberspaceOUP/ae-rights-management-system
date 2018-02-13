//Create by Saddam 02/05/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Master
{
    public partial class LanguageMaster : BaseEntity, ILocalizedEntity 
    {
        public string LanguageName { get; set; }
        public string LanguageCode { get; set; }


    }
}
