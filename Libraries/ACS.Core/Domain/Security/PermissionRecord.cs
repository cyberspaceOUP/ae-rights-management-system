using System.Collections.Generic;
//using ACS.Core.Domain.Contact;

namespace ACS.Core.Domain.Security
{
    /// <summary>
    /// Represents a permission record
    /// </summary>
    public class PermissionRecord : BaseEntity
    {
        //private ICollection<ContactRole> _contactRoles;

        /// <summary>
        /// Gets or sets the permission name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the permission system name
        /// </summary>
        public string SystemName { get; set; }
        
        /// <summary>
        /// Gets or sets the permission category
        /// </summary>
        public string Category { get; set; }
        
        /// <summary>
        /// Gets or sets discount usage history
        /// </summary>
        //public virtual ICollection<ContactRole> ContactRoles
        //{
        //    get { return _contactRoles ?? (_contactRoles = new List<ContactRole>()); }
        //    protected set { _contactRoles = value; }
        //}   
    }
}
