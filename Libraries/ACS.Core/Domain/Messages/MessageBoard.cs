using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using ACS.Core.Domain.Localization;
//using ACS.Core.Domain.Organization;

namespace ACS.Core.Domain.Messages
{
    /// <summary>
    /// Represents Message Board
    /// </summary>
    public partial class MessageBoard : BaseEntity, ILocalizedEntity
    {
        public string Heading{get;set;}
        public string Body { get; set; }
        public int Sequence { get; set; }
        public DateTime  FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string LinkUrl { get; set; }
        public string  ImageUrl { get; set; }
        public int EnteredBy { get; set; }

        [DefaultValue("true")]
        public bool DeactTag { get; set; }
        public DateTime? DeactDate { get; set; }
       
        #region Navigation Properties

        //public virtual Employee MessageEnteredBy { get; set; }
        
        #endregion
    }
}   
 