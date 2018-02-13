//Create by Saddam on 24/05/2016
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Master
{
    
    public partial class CustomProduct
    {
        public int Id { get; set; }

        public int  ProductId { get; set; }
        public string ProprietorISBN { get; set; }
        public string ProprietorProduct { get; set; }

        public string ProprietorEdition { get; set; }
        public string ProprietorCopyrightYear { get; set; }




        public int PublishingCompanyId { get; set; }
        public int ProprietorPubCenterId { get; set; }

        public int ProprietorImPrintId { get; set; }

        public int ProprietorId { get; set; }

        public string   AuthorId { get; set; }

        public string[] AuthorIdList { get; set; }


        public string Deactivate { get; set; }
        public int EnteredBy { get; set; }
        public DateTime EntryDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? DeactivateBy { get; set; }
        public DateTime? DeactivateDate { get; set; }

        public string Main { get; set; }

    }
}
