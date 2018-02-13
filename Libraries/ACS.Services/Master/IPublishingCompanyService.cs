using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Master
{
    public partial interface IPublishingCompanyService
    {
        /// <summary>
        /// check Duplicity
        /// </summary>
        /// <returns></returns>
        String DuplicityCheck(PublishingCompanyMaster publishingCompany);

        /// <summary>
        /// Insert Publishing Company 
        /// </summary>
        /// <returns></returns>

        void InsertPublishingCompany(PublishingCompanyMaster publishingCompany);

        /// <summary>
        /// Get Publishing Company 
        /// </summary>
        /// <returns>Publishing Company </returns>
        /// 
        IList<PublishingCompanyMaster> GetAllPublishingCompany();
        /// <summary>
        /// Get Publishing Company 
        /// </summary>
        /// <returns>Publishing Company </returns>
        /// 
        PublishingCompanyMaster GetPublishingCompanyById(PublishingCompanyMaster publishingCompany);
        /// <summary>
        /// Update Publishing Company 
        /// </summary>
        /// <returns></returns>
       
        void UpdatePublishingCompany(PublishingCompanyMaster publishingCompany);

        /// <summary>
        /// Delete Publishing Company  
        /// </summary>
        /// <returns></returns>
       
        void DeletePublishingCompany(PublishingCompanyMaster publishingCompany);

    }
}
