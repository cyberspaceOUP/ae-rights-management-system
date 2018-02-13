using ACS.Core.Domain.Localization;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ACS.Core.Domain.Configuration
{
    public partial class UserProfile : BaseEntity, ILocalizedEntity
    {
        private ICollection<ACS.Core.Domain.Configuration.ApplicationActivities> _applicationActivities;

        public string Name { get; set; }
        public string Code { get; set; }

        #region Navigation Properties

        public virtual ICollection<ACS.Core.Domain.Configuration.ApplicationActivities> ApplicationActivities
        {
            get { return _applicationActivities ?? (_applicationActivities = new List<ACS.Core.Domain.Configuration.ApplicationActivities>()); }
            set { _applicationActivities = value; }
        }

        #endregion
    }
}
