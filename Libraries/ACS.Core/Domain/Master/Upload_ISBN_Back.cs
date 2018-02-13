﻿//Create by Saddam on 6/06/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Master
{
   
    public partial class Upload_ISBN_Back : BaseEntity, ILocalizedEntity
    {
        public string UploadFile { get; set; }
        public string DocumentName { get; set; }
        public int ISBNBagId { get; set; }

        public virtual ISBNBag ISBNBagMaster { get; set; }



    }
}
