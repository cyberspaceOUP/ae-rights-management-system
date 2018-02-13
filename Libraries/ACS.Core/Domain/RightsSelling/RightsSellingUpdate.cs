//Create By Saddam on 13/07/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.Master;
using ACS.Core.Domain.AuthorContract;
using ACS.Core.Domain.Product;
using ACS.Core.Domain.RightsSelling;

namespace ACS.Core.Domain.RightsSelling
{
 public partial  class RightsSellingUpdate : BaseEntity, ILocalizedEntity
    {
     public string ContractStatus { get; set; }
     public DateTime? Date_of_agreement { get; set; }
     public DateTime? Signed_Contract_sent_date { get; set; }
     public DateTime? Signed_Contract_receiveddate { get; set; }
     public DateTime? CancellationDate { get; set; }
     public string Cancellation_Reason { get; set; }
     public string Contributor_Agreement { get; set; }
     public int? RightsSellingID { get; set; }
     public string Remarks { get; set; }


     ///public DateTime? AgreementDate { get; set; }
     public DateTime? Effectivedate { get; set; }
     public int? Contractperiodinmonth { get; set; }
     public DateTime? Expirydate { get; set; }


     public virtual RightsSellingMaster RightsSellingMaster { get; set; }

     public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
     public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
     public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }


     public virtual ICollection<RightsSellingDocument> RightsSellingDocument { get; set; }
    }
}
