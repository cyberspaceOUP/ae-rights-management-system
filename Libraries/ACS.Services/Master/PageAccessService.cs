using ACS.Core.Data;
using ACS.Core.Domain.Master;
using ACS.Services.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ACS.Services.Security;

namespace ACS.Services.Master
{
   public partial class PageAccessService :IPageAccessService 
    {
        #region Fields
        private readonly IRepository<PageAccessMaster> _PageAccessRepository;
        private readonly IEncryptionService _encryptionService;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="GeographyType">Geography</param>        
        public PageAccessService(
            IRepository<PageAccessMaster> PageAccessRepository
            )
        {
            _PageAccessRepository = PageAccessRepository;
        }

       //public PageAccessService(IRepository<PageAccessMaster> PageAccessRepository)
       // {
       //     _PageAccessRepository = PageAccessRepository;

       // }
        public void InsertPageAccess(PageAccessMaster pageAccess)
        {
            pageAccess.Deactivate = "N";
           // pageAccess.EnteredBy = 10;
            pageAccess.EntryDate = DateTime.Now;
            pageAccess.ModifiedBy = null;
            pageAccess.ModifiedDate = null;
            pageAccess.DeactivateBy = null;
            pageAccess.DeactivateDate = null;

            _PageAccessRepository.Insert(pageAccess);
        }
        #endregion


    }
}
