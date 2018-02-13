using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Product
{
    public partial class ImpressionSearch
    {


        public class ORSearchResult
        {
            public int ProductId { get; set; }
            public string ProductCode { get; set; }
            public string OUPISBN { get; set; }
            public string FinalProductName { get; set; }
            //public string AuthorCode { get; set; }
            public int AuthorContractId { get; set; }
            public string AuthorContractCode { get; set; }

        }

        public class NOSearchResult
        {
            public int ProductId { get; set; }
            public int LicenseId { get; set; }
            public string ProductLicenseCode { get; set; }
            public string AddendumCode { get; set; }
            public string ProductCode { get; set; }
            public string OUPISBN { get; set; }
            public string FinalProductName { get; set; }
            public string ProductCategoryName { get; set; }
            public string ExpiryDate { get; set; }
            public DateTime? ExpiryDateForSort { get; set; }

        }
        
        
        public string ProductLicesneCode { get; set; }
        public string LicenseAddendumCode { get; set; }
        public string FinalTitle { get; set; }
        public string OUPISBN { get; set; }
        public string ProductCategory { get; set; }
        public string ProductCode { get; set; }
    }
}
