//create by saddam 
//date : 01/07/2016
//purpose : Insert ISBN Bag
using ACS.Core.Domain.Master;

using ACS.Core.Data;
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
using ACS.Core.Domain.OtherContract;

namespace ACS.Services.Master
{
    public partial class ISBNService : IISBNService
    {

        #region Fields
        private readonly IDbContext _dbContext;
        private readonly IRepository<ISBNBag> _ISBNBagMaster;
        private readonly IRepository<Upload_ISBN_Back> _Upload_ISBN_Back;
        #endregion

        public ISBNService(
           IRepository<ISBNBag> ISBNBag
             , IRepository<Upload_ISBN_Back> Upload_ISBN_Back
                  , IDbContext dbContext

               )
        {
            _ISBNBagMaster = ISBNBag;
            _Upload_ISBN_Back = Upload_ISBN_Back;
            this._dbContext = dbContext;
        }


        public void InsertISBNBag(IList<ISBNBag> ISBNBagList)
        {
            foreach (var lst in ISBNBagList)
            {
                  _ISBNBagMaster.Insert(lst);
                
            }
        }
        public int GetIsbnByIsbn(string isbn)
        {
            var query = _ISBNBagMaster.Table.Where(i => i.Deactivate == "N" && i.ISBN == isbn).FirstOrDefault();
            if(query!=null)
            {
               return query.Id;
            }
            else
            {
                return 0;
            }
        }

        //public void InsertUpload_ISBN_Back(Upload_ISBN_Back Upload_ISBN_Back)
        //{
        //    Upload_ISBN_Back.Deactivate = "N";
        //    Upload_ISBN_Back.EntryDate = DateTime.Now;
        //    Upload_ISBN_Back.ModifiedBy = null;
        //    Upload_ISBN_Back.ModifiedDate = null;
        //    Upload_ISBN_Back.DeactivateBy = null;
        //    Upload_ISBN_Back.DeactivateDate = null;
        //    _Upload_ISBN_Back.Insert(Upload_ISBN_Back);
        //}
    }
}
