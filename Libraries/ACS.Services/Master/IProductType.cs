
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Master
{
    public partial interface IProductType
    {
        /// <summary>
        /// check Duplicity
        /// </summary>
        /// <param name="city">City class object</param>
        /// <returns></returns>

        String DuplicityCheck(ProductTypeMaster ProductType);
        /// <summary>
        /// Insert Division 
        /// </summary>
        /// <param name="city">City class object</param>
        /// <returns></returns>
        void InsertProductType(ProductTypeMaster ProductType);

        /// <summary>
        /// Get Division
        /// </summary>
        /// <param name="Geography city">Division as object</param>
        /// <returns>Division</returns>
        ProductTypeMaster GetProductTypeById(ProductTypeMaster ProductType);

        /// <summary>
        /// Update Division 
        /// </summary>
        /// <param name="Division">Division class object</param>
        /// <returns></returns>
        void UpdateProductType(ProductTypeMaster ProductType);


        /// <summary>
        /// Delete Division 
        /// </summary>
        /// <param name="Division">Delete Division Object</param>
        /// <returns></returns>
        void DeleteProductType(ProductTypeMaster ProductType);

        /// <summary>
        /// Gets all Product Type List
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns> Product Type collection</returns>
        IList<ProductTypeMaster> GetAllProductType();
        IList<ProductTypeMaster> GetAllProductTypeList();

        /// <summary>
        /// Gets all Sub Product Type List
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Sub Product Type collection</returns>
      //  IList<ProductTypeMaster> GetAllSubProductType(ProductTypeMaster ProductType);



        IList<ProductTypeMaster> GetSubProductType();

    }
}
