using ACS.Core.Domain.Localization;
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Product
{
    public partial class ProductMaster : BaseEntity, ILocalizedEntity
    {

        public int DivisionId { get; set; }
        public int SubdivisionId { get; set; }
        public int ProductCategoryId { get; set; }
        public int ProductTypeId { get; set; }
        public int SubProductTypeId { get; set; }
        public string ProjectCode { get; set; }
        public string ProductCode { get; set; }
        public string OUPISBN { get; set; }
        public string WorkingProduct{ get; set; }
        public string WorkingSubProduct { get; set; }
        public string OUPEdition { get; set; }
        public string Volume { get; set; }
        public string CopyrightYear { get; set; }
        public int ImprintId { get; set; }
        public int LanguageId { get; set; }
        public int? SeriesId { get; set; }
        public string Derivatives { get; set; }
        public string OrgISBN { get; set; }
        public DateTime? ProjectedPublishingDate { get; set; } 
        public string ProjectedPrice { get; set; }
        public int? ProjectedCurrencyId { get; set; }
        public int? PubCenterId  { get; set; }
        public string FinalProductName { get; set; }
        public DateTime? PublishingDate { get; set; }
        public int ThirdPartyPermission { get; set; }

        #region Navigation Properties
            public virtual DivisionMaster ProductDivision { get; set; }
            public virtual DivisionMaster ProductSubDivision { get; set; }
            public virtual ProductCategoryMaster ProductProductCategory { get; set; }
            public virtual ProductTypeMaster ProductProductType { get; set; }
            public virtual ProductTypeMaster ProductSubProductType { get; set; }
            public virtual ImprintMaster ProductImprint { get; set; }
            public virtual LanguageMaster ProductLanguage { get; set; }
            public virtual SeriesMaster ProductSeries { get; set; }
            public virtual CurrencyMaster ProductProjectedCurrecy { get; set; }
            public virtual PubCenterMaster ProductPubCenter { get; set; }

            public virtual ICollection<ProprietorMaster> ProductProprietorMaster { get; set; }
            public virtual ICollection<ProductAuthorLink> ProductProductAuthorLink { get; set; }
            public virtual ICollection<ProductPreviousProductLink> ProductPreviousProductLink { get; set; }
        #endregion
    }
}
