using ACS.Core.Domain.Localization;
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Product
{
    public partial class ProprietorMaster : BaseEntity, ILocalizedEntity
    {
        public int ProductId { get; set; }
        public string ProprietorISBN { get; set; }
        public string ProprietorProduct { get; set; }
        public string ProprietorEdition { get; set; }
        public string ProprietorCopyrightYear { get; set; }
        public int PublishingCompanyId { get; set; }
        public int ProprietorPubCenterId { get; set; }
        public int ProprietorImPrintId { get; set; }
        public string Main { get; set; }
        public string ProprietorAuthorName { get; set; }

        #region Navigation Properties
        public virtual PublishingCompanyMaster ProprietorPublishingCompany { get; set; }
        public virtual PubCenterMaster ProprietorPubCenter { get; set; }
        public virtual ImprintMaster ProprietorImprint { get; set; }

    
        public virtual ICollection<ProprietorAuthorLink> ProprietorAuthorLink { get; set; }

        public virtual ProductMaster ProprietorProductMaster { get; set; }


        #endregion
    }
}
