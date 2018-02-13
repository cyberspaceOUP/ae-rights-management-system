using System.Collections.Generic;
using ACS.Core.Domain.Security;

namespace ACS.Core.Domain.Contact
{
    public partial class ContactRole : BaseEntity
    {
        private ICollection<PermissionRecord> _permissionRecords;
        private ICollection<ACS.Core.Domain.Configuration.ApplicationActivities> _applicationActivities;

        /// <summary>
        /// Gets or sets the customer role name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer role is active
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer role is system
        /// </summary>
        public bool IsSystemRole { get; set; }

        /// <summary>
        /// Gets or sets the customer role system name
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// Gets or sets the permission records
        /// </summary>
        public virtual ICollection<PermissionRecord> PermissionRecords
        {
            get { return _permissionRecords ?? (_permissionRecords = new List<PermissionRecord>()); }
            protected set { _permissionRecords = value; }
        }

        public virtual ICollection<ACS.Core.Domain.Configuration.ApplicationActivities> ApplicationActivities
        {
            get { return _applicationActivities ?? (_applicationActivities = new List<ACS.Core.Domain.Configuration.ApplicationActivities>()); }
            protected set { _applicationActivities = value; }
        }
    }
}
