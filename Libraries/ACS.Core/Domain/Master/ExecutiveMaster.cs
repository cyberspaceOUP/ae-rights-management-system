//Create by Saddam 02/05/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Master
{
 public partial   class ExecutiveMaster : BaseEntity, ILocalizedEntity 
    {

     private ICollection<ExecutiveReporting> _ExecutiveReporting;
     private ICollection<ExecutiveDivisionLink> _ExecutiveDivisionLink;
     
     public string executiveName { get; set; }
     public string executivecode { get; set; }
     public string Emailid { get; set; }
     public string Password { get; set; }
     public string Mobile { get; set; }
     public string Phoneno { get; set; }
     //Added by Sanjeet
     public string block { get; set;}
     public string PwdChanged { get; set;}
     //
    // public string OldPassword { get; set; }
     public int DepartmentId { get; set; }
     public virtual DepartmentMaster DepartmentM { get; set; }


     public int ProcessTransferTo { get; set; }
     public virtual ExecutiveMaster ExecutiveM { get; set; }

     public int? Active { get; set; }

     public virtual ICollection<ExecutiveReporting> ExecutiveReportings
     {

         get { return _ExecutiveReporting ?? (_ExecutiveReporting = new List<ExecutiveReporting>()); }

         set { _ExecutiveReporting = value; }

     }

     public virtual ICollection<ExecutiveDivisionLink> ExecutiveDivisionLinks
     {

         get { return _ExecutiveDivisionLink ?? (_ExecutiveDivisionLink = new List<ExecutiveDivisionLink>()); }

         set { _ExecutiveDivisionLink = value; }

     }


     //public object Select(Func<TSource, TResult> func)
     //{
     //    throw new NotImplementedException();
     //}
    }
}
