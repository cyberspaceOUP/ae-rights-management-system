
using ACS.Core.Domain.Localization;
using ACS.Core.Domain.Contact;
using ACS.Core.Domain.User;
using ACS.Core.Domain.Master;


namespace ACS.Core
{
    /// <summary>
    /// Work context
    /// </summary>
    public interface IWorkContext
    {
      
        /// <summary>
        /// Get or set current Logged in User belongs to the UserMaster Table
        /// </summary>
        ExecutiveMaster  CurrentUser { get; set; }

       
        
        /// <summary>
        /// Get or set value indicating whether we're in admin area
        /// </summary>
        bool IsAdmin { get; set; }
    }
}
