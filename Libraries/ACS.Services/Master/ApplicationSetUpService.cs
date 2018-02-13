//Create by Saddam on 30/05/2016
using ACS.Core.Data;
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ACS.Services.Security;

namespace ACS.Services.Master
{
   

    public partial class ApplicationSetUpService : IApplicationSetUpService 
    {
        #region Fields
        private readonly IRepository<ApplicationSetUp> _ApplicationSetUp;
        private readonly IEncryptionService _encryptionService;
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="GeographyType">Geography</param>        
        public ApplicationSetUpService(
        IRepository<ApplicationSetUp> ApplicationSetUp
           , IEncryptionService encryptionService
    )
        {
            _ApplicationSetUp = ApplicationSetUp;
            this._encryptionService = encryptionService;
        }



        #endregion

        #region Methods



        public void UpdateApplication(ApplicationSetUp ApplicationSetUp)
        {
            _ApplicationSetUp.Update(ApplicationSetUp);
        }

        public ApplicationSetUp GetApplicationSetUpById(ApplicationSetUp ApplicationSetUp)
        {
            return _ApplicationSetUp.Table.Where(i => i.Id == ApplicationSetUp.Id).FirstOrDefault();
        }
        #endregion

    }


}
