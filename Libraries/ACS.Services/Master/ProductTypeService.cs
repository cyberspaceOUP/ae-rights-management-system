
using ACS.Core.Data;
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace ACS.Services.Master
{
    public partial class ProductTypeService : IProductType
    {
        #region Fields
        private readonly IRepository<ProductTypeMaster> _ProductTypeRepository;
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="GeographyType">Geography</param>        
        public ProductTypeService(
        IRepository<ProductTypeMaster> ProductTypeRepository
    )
        {
            _ProductTypeRepository = ProductTypeRepository;
        }



        #endregion

        #region Methods

        /// <summary>
        /// Check The duplicity of record before insertion and deletion
        /// </summary>
        /// <param name="GeographyType">Geography</param>        
        public string DuplicityCheck(ProductTypeMaster ProductType)
        {

            var dupes = _ProductTypeRepository.Table.Where(x => x.typeName == ProductType.typeName
                                                            && x.Deactivate == "N"
                                                            && (ProductType.Id != 0 ? x.Id : 0) != (ProductType.Id != 0 ? ProductType.Id : 1)).FirstOrDefault();
            if (dupes != null)
            {
                return "N";

            }
            else
            {
                return "Y";
            }
        }

        public void InsertProductType(ProductTypeMaster ProductType)
        {
            ProductType.Deactivate = "N";
            ProductType.EnteredBy = 10;
            ProductType.EntryDate = DateTime.Now;
            ProductType.ModifiedBy = null;
            ProductType.ModifiedDate = null;
            ProductType.DeactivateBy = null;
            ProductType.DeactivateDate = null;
            _ProductTypeRepository.Insert(ProductType);
        }

        public ProductTypeMaster GetProductTypeById(ProductTypeMaster ProductType)
        {
            return _ProductTypeRepository.Table.Where(i => i.Id == ProductType.Id).FirstOrDefault();
        }

        public void UpdateProductType(ProductTypeMaster ProductType)
        {
            _ProductTypeRepository.Update(ProductType);
        }

        public void DeleteProductType(ProductTypeMaster ProductType)
        {
            _ProductTypeRepository.Delete(ProductType);
        }

        public virtual IList<ProductTypeMaster> GetSubProductType()
        {
            return _ProductTypeRepository.Table.Where(d => d.parenttypeid != null && d.Deactivate == "N").OrderBy(c => c.typeName).ToList();

        }


        public virtual IList<ProductTypeMaster> GetAllProductType()
        {
            return _ProductTypeRepository.Table.Where(d => d.typelevel == 1 && d.Deactivate == "N").OrderBy(c => c.typeName).ToList();

        }

        public virtual IList<ProductTypeMaster> GetAllProductTypeList()
        {
            return _ProductTypeRepository.Table.Where(d=> d.Deactivate == "N").ToList();

        }

        #endregion

    }
}
