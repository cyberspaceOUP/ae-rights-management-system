using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.Product;
using ACS.Core.Domain.Master;

namespace ACS.Core.Domain.Master
{
    public partial class UploadDocument : BaseEntity, ILocalizedEntity
    {
        public string MasterName { get; set; }
        public int MasterId { get; set; }
        public string FileName { get; set; }
        public string UploadFileName { get; set; }
    }
}

