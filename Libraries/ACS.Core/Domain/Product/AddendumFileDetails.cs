using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Product
{
    public partial class AddendumFileDetails : BaseEntity, ILocalizedEntity
    {
        public int AddendumId { get; set; }
        public int LicenseId { get; set; }
        public string FileName { get; set; }
        public string UploadFileName { get; set; }

        #region Navigation Properties
        public virtual AddendumDetails FileAddendumDetails { get; set; }
        #endregion
    }
}
