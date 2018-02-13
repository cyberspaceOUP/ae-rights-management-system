//create by saddam 
//date : 24/05/2016
///purpose : Insert, Update, Delete Records for Custom Product insert data in two table ProprietorMaster, ProprietorAuthorLink
using ACS.Core.Data;
using ACS.Core.Domain.Product;
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Data;
using System.Data.SqlClient;
using ACS.Services.Security;
using System.Data;

namespace ACS.Services.Master
{
 

    public partial class CustomProductService : ICustomProductService
    {
        #region Fields
        private readonly IDbContext _dbContext;
        private readonly IRepository<ProprietorMaster> _ProprietorRepository;
      
        private readonly IEncryptionService _encryptionService;
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="GeographyType">Geography</param>        
        public CustomProductService(
         IRepository<ProprietorMaster> ProprietorRepository
            , IEncryptionService encryptionService
           
               , IDbContext dbContext
            )
        {
            _ProprietorRepository = ProprietorRepository;
         
            this._encryptionService = encryptionService;
            this._dbContext = dbContext;
        }



        #endregion

        #region Methods


        public virtual IList<ProprietorMaster> GetAllCustomproduct()
        {


            var Custom = _ProprietorRepository.Table.Where(d => d.ProprietorISBN != null && d.Deactivate == "N").OrderBy(c => c.ProprietorISBN).ToList();
            return Custom;

        }

        public string DuplicityCheck(ProprietorMaster Custom)
        {

            var dupes = _ProprietorRepository.Table.Where(x => x.ProprietorISBN == Custom.ProprietorISBN 
                                                     && x.Deactivate == "N"
                                                      && (Custom.Id != 0 ? x.Id : 0) != (Custom.Id != 0 ? Custom.Id : 1)).FirstOrDefault();
            if (dupes != null)
            {
                return "N";

            }
            else
            {
                return "Y";
            }
        }



        public void insertcustomproduct(ProprietorMaster Proprietor)
        {

            _ProprietorRepository.Insert(Proprietor);
        }

        public ProprietorMaster getcustomproductbyid(ProprietorMaster Proprietor)
        {
            return _ProprietorRepository.Table.Where(i => i.Id == Proprietor.Id).FirstOrDefault();
        }

        public void updatecustomproduct(ProprietorMaster Proprietor)
        {
            _ProprietorRepository.Update(Proprietor);
        }

        public void deletecustomproduct(ProprietorMaster Proprietor)
        {
            _ProprietorRepository.Delete(Proprietor);
        }

        #endregion


    

    }
}
