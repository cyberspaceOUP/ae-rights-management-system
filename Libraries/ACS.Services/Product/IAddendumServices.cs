using ACS.Core.Domain.Master;
using ACS.Core.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Product
{
    public partial interface IAddendumServices
    {
        /// <summary>
        /// Insert Product 
        /// </summary>
        /// <param name="city">City class object</param>
        /// <returns></returns>
        int InsertAddendumDetails(AddendumDetails AddendumDetails);

        /// <summary>
        /// Get Product Code 
        /// </summary>
        /// <param name="city">Get Product Code</param>
        /// <returns></returns>
        string GetAddendumCode(int ProductId);


        /// <summary>
        /// Get Addendum Details By Id
        /// </summary>
        /// <param name="city">Get Product Code</param>
        /// <returns></returns>
        AddendumDetails GetAddendumDetailsById(AddendumDetails Addendum);

        /// <summary>
        /// Get Addendum Details By License Id
        /// </summary>
        /// <param name="city">Get Product Code</param>
        /// <returns></returns>
        IList<AddendumDetails> GetAddendumDetailsByLicenseId(int LicenseId);

        /// <summary>
        /// Delete Proprietor Author link
        /// </summary>
        /// <param name="city">City class object</param>
        /// <returns></returns>
        void DeleteAddendumRoyaltySlab(AddendumRoyaltySlab AddendumRoyaltySlab);

        /// <summary>
        /// Delete Proprietor Author link
        /// </summary>
        /// <param name="city">City class object</param>
        /// <returns></returns>
        void DeleteAddendumFileDetails(AddendumFileDetails AddendumFileDetails);

        /// <summary>
        /// Delete Proprietor Author link
        /// </summary>
        /// <param name="city">City class object</param>
        /// <returns></returns>
        void UpdateAddendumDetails(AddendumDetails Addendum);

        /// <summary>
        /// Get Product Master By ProductId 
        /// </summary>
        /// <param name="city">Get Product Master Details</param>
        /// <returns></returns>
        AddendumFileDetails GetFileDetailsById(AddendumFileDetails FileDetails);

        ISBNBag GetISBNBagById(ISBNBag ISBNBag);

        ISBNBag GetISBNBagByISBN(string ISBN);

        void UpdateISBNBag(ISBNBag ISBNBag);

        IList<ImpressionDetails> GetImpressionDetails(ImpressionDetails ImpressionDetails);

        void ImsertImpressionDetails(ImpressionDetails ImpressionDetails);


        void InsertAddendumFileDetails(AddendumFileDetails AddendumFileDetails);

    }
}
