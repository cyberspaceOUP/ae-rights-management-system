//Create by Saddam on 26/7/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Master
{
   
    public partial class NomineeAuthorDocumentMaster : BaseEntity, ILocalizedEntity
    {
        public int AuhtorId { get; set; }
        public string DocumentName { get; set; }
        public string UploadFile { get; set; }
        public virtual AuthorMaster AuthorMaster { get; set; }
    }
}
