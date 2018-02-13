using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.Master;
using ACS.Core.Domain.Product;
using ACS.Core.Domain.AuthorContract;

namespace ACS.Services.Product
{
    public partial interface ISeriesProductEntryService
    {
        /// <summary>
        /// Insert Series Product 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        int InsertSeriesProduct(ProductMaster product);
        /// <summary>
        /// Get Product Code 
        /// </summary>
        /// <param name="">Get Product Code</param>
        /// <returns></returns>
        string GetProductCode(int productCategoryId);

        void InsertProductAuthorLink(ProductAuthorLink ProductAuthorLink);

        /// <summary>
        /// Check ISBN Duplicity
        /// </summary>
        /// <param name="">Check ISBN Duplicity</param>
        /// <returns></returns>
        string DuplicateISBNNo(string ISBNno);

        /// <summary>
        /// Check WorkingProduct Duplicity
        /// </summary>
        /// <param name="">Check WorkingProduct Duplicity</param>
        /// <returns></returns>
        string DuplicateWorkingPro(string WorkingPro);

        /// <summary>
        /// Check Author Contract //add by Ankush 19/10/2016
        /// This list is used for Deriative Products if Author Contract not entered of previous product then not able to entered
        /// </summary>
        /// <param name="city">Check Project Code</param>
        /// <returns></returns>
        IList<AuthorContractOriginal> CheckAuthorContract(string ISBN);

        /// <summary>
        /// Insert Product Previous Product Link
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        void InsertProductPreviousProductLink(ProductPreviousProductLink ProductPreviousProductLink);
    }
}
