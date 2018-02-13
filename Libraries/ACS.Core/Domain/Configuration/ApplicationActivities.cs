using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.Localization;
namespace ACS.Core.Domain.Configuration
{
    public partial class ApplicationActivities : BaseEntity, ILocalizedEntity
    {

        private ICollection<ACS.Core.Domain.Configuration.ApplicationActivities> _childActivities;
        private ICollection<UserProfile> _userProfiles;

        //private ICollection<ACS.Core.Domain.Contact.ContactRole> _contactRoles;
        public string ActivityDesc { get; set; }
        public int ?ParentId { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public int SequenceNo { get; set; }
        public bool IsNew { get; set; } //ADDED BY AMAN KUMAR ON DATE 04/03/2016
        public string IconClass { get; set; } //ADDED BY AMAN KUMAR ON DATE 04/03/2016
        public bool DeactTag { get; set; }
        public DateTime? DeactDate { get; set; }
        public string QueryString{ get; set; } //ADDED BY VISHAL VERMA ON DATE 16/07/2016

        #region Navigation Properties

        public virtual ApplicationActivities ParentActivity { get; set; }

        public virtual ICollection<ACS.Core.Domain.Configuration.ApplicationActivities> ChildActivities
        {
            get { return _childActivities ?? (_childActivities = new List<ACS.Core.Domain.Configuration.ApplicationActivities>()); }
            set { _childActivities = value; }
        }

        public virtual ICollection<UserProfile> UserProfiles
        {
            get { return _userProfiles ?? (_userProfiles = new List<UserProfile>()); }
            set { _userProfiles = value; }
        }
        //public virtual ICollection<ACS.Core.Domain.Contact.ContactRole> ContactRoles
        //{
        //    get { return _contactRoles ?? (_contactRoles = new List<ACS.Core.Domain.Contact.ContactRole>()); }
        //    set { _contactRoles = value; }
        //}

        #endregion

    }
}
