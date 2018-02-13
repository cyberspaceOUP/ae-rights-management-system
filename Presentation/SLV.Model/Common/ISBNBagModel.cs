//Create by Saddam on 30/06/2016
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;





namespace SLV.Model.Common
{
    public class ISBNBagModel
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string FileName { get; set; }
        public int EnteredBy { get; set; }
        public int? ProductType { get; set; }
        public string UploadFile { get; set; }
        public string DocumentName { get; set; }
        public int ISBNBagId { get; set; }
        public string Status { get; set; }
        public string typename { get; set; }
        public int ProductId { get; set; }

        public int ISBNId { get; set; }

        public string Path { get; set; }

        public string ProductCode { get; set; }

        public string FinalProductName { get; set; }
    }
    
}
