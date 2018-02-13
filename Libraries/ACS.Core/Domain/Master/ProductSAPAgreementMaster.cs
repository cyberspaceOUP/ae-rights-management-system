using ACS.Core.Domain.Localization;
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Master
{
    public partial class ProductSAPAgreementMaster : BaseEntity, ILocalizedEntity
    {
       public string OUPISBN { get; set; }
       public string SAPagreementNo { get; set; }
       public string AuthorCode { get; set; }
       public int? AuthorId { get; set; }
       public int? ProprietorAuthorId { get; set; }
       public string ProductCategory { get; set; }
     

    }
}
