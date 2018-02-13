using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLV.Model.Common
{
    public  class ExecutiveModel
    {
        public string executiveName { get; set; }
        public string executivecode { get; set; }
        public string Emailid { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string Phoneno { get; set; }
        public string block { get; set; }
        public string PwdChanged { get; set; }
        public int DepartmentId { get; set; }
        public int ReportingId { get; set; }
        public int[] Division { get; set; }
        public int Id { get; set; }
        public int EnteredBy { get; set; }

        public string DepartmentName { get; set; }
        public string RoleName { get; set; }
        public string ReportingTo { get; set; }
        public string ReportingFrom { get; set; }
        public string ReportingToEmailId { get; set; }
        public string ReportingToPhoneno { get; set; }
        public string ReportingFromEmailId { get; set; }
        public string ReportingFromPhoneno { get; set; }
        public string ReportingToExecutivecode { get; set; }

        public int? Active { get; set; }
        public int ProcessTransferTo { get; set; }
    }
}
