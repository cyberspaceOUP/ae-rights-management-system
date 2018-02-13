using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLV.Model.Report
{
    public class InvoiceReportModel
    {
        public string EntryDate { get; set; }
        public string InvoiceFromDate { get; set; }
        public string InvoiceToDate { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceValue { get; set; }
        public string InvoiceStatus { get; set; }
        public string LicenseeName { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        public int? LicenseeId { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }
        public int? PermissionsOutboundId { get; set; }

        public string PermissionsOutboundCode { get; set; }
        public string InvoiceDescription { get; set; }
        public string LicenseeOrganizationName { get; set; }

        public string SessionId { get; set; }
        public string ReturnList { get; set; }

        public string CurrencyName { get; set; }
        public string DateOfInvoice { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string URL { get; set; }

        public string WorkingProduct { get; set; }
        public string WorkingSubProduct { get; set; }
        public string OUPISBN { get; set; }
        public string AuthorName { get; set; }

    }

}
