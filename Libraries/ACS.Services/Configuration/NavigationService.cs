using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.Configuration;
using ACS.Core.Data;

namespace ACS.Services.Configuration
{
  public partial  class NavigationService : INavigationService 
    {
        private readonly IRepository<ApplicationActivities> _applicationactivitiesRepository;
        private readonly IRepository<UserProfile> _userProfileRepository;

        public NavigationService(   
             IRepository<ApplicationActivities> _applicationactivitiesRepository,
            IRepository<UserProfile> userProfileRepository
            )
        {
            this._applicationactivitiesRepository = _applicationactivitiesRepository;
            this._userProfileRepository = userProfileRepository;
        }
        //ADDED BY AMAN KUMAR ON DATE 07/03/2016
        public IList<ApplicationActivities> GetTopActivities(int ProfileId)
        {

            //return _applicationactivitiesRepository.Table.Where(a => a.Deactivate == "N").ToList();
            if (_userProfileRepository.Table.Any(up => up.Id == ProfileId))
            {
                return _userProfileRepository.Table
                .Where(up => up.Id == ProfileId).FirstOrDefault()
                .ApplicationActivities.Where(aa => aa.DeactTag == false && aa.ParentId == null && aa.Deactivate =="N").OrderBy(p=>p.SequenceNo)
                .ToList();
            }
            else
                return null;
            //return _applicationactivitiesRepository.Table.Where(ac => ac.DeactTag == false && ac.ParentId == null).ToList();    //.Where(ac => ac.UserProfiles.Where(up => up.Id == ProfileId)).ToList();
        }

        public ApplicationActivities GetSubMenus(int SubMenuId)
        {
            return _applicationactivitiesRepository.Table.Where(ac => ac.Id == SubMenuId && ac.DeactTag == false && ac.Deactivate == "N").OrderBy(p => p.SequenceNo).FirstOrDefault();
        }

    }
}
