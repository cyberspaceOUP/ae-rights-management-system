using ACS.Core.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Product
{
    public partial interface IProductLicenseService
    {
        /// <summary>
        /// Insert Product 
        /// </summary>
        /// <param name="city">City class object</param>
        /// <returns></returns>
        ProductLicense InsertProductLicenseMaster(ProductLicense ProductLicense);

        void UpdateProductLicenseMaster(ProductLicense ProductLicense);

        /// <summary>
        /// Get Product Code 
        /// </summary>
        /// <param name="city">Get Product Code</param>
        /// <returns></returns>
        string GetProductLicenseCode(int ProductId);

        /// <summary>
        /// Get Product Master By ProductId 
        /// </summary>
        /// <param name="city">Get Product Master Details</param>
        /// <returns></returns>
        ProductLicense GetProductLicenseById(ProductLicense ProductLicense);

        ProductLicense GetProductLicenseById(int ProductLicenseId);

        AddendumDetails GetAddendumDetailById(int ProductLicenseId);

        /// <summary>
        /// Delete Proprietor Author link
        /// </summary>
        /// <param name="city">City class object</param>
        /// <returns></returns>
        void DeleteProDuctLicenseRoyality(ProductLicenseRoyality ProductLicenseRoyality);


        /// <summary>
        /// Delete Proprietor Author link
        /// </summary>
        /// <param name="city">City class object</param>
        /// <returns></returns>
        void DeleteProductLicenseSubsidiaryRights(ProductLicenseSubsidiaryRights ProductLicenseSubsidiaryRights);


        /// <summary>
        /// Update Product 
        /// </summary>
        /// <param name="city">City class object</param>
        /// <returns></returns>
        void UpdateProductLicense(ProductLicense ProductLicense);

        /// <summary>
        /// Delete Proprietor Author link
        /// </summary>
        /// <param name="city">City class object</param>
        /// <returns></returns>
        void DeleteProductLicenseFileDetails(ProductLicenseFileDetails FileDetails);

        /// <summary>
        /// Delete Proprietor Author link
        /// </summary>
        /// <param name="city">City class object</param>
        /// <returns></returns>
        void DeleteProductLicenseUpdateDetails(ProductLicenseUpdateDetails UpdateDetails);

        /// <summary>
        /// Get Product Master By ProductId 
        /// </summary>
        /// <param name="city">Get Product Master Details</param>
        /// <returns></returns>
        ProductLicenseFileDetails GetFileDetailsById(ProductLicenseFileDetails FileDetails);


        /// <summary>
        /// Insert Product 
        /// </summary>
        /// <param name="city">City class object</param>
        /// <returns></returns>
        void InsertProductLicenseUpdateDetails(ProductLicenseUpdateDetails ProductLicenseUpdateDetails);

        /// <summary>
        /// Check Project Code 
        /// </summary>
        /// <param name="city">Check Project Code</param>
        /// <returns></returns>
        string DuplicityLicenseUpdateDetails(ProductLicenseUpdateDetails LicenseUpdateDetails);

        /// <summary>
        /// Check Project Code 
        /// </summary>
        /// <param name="city">Check Project Code</param>
        /// <returns></returns>
        int CheckLicenseCode(ProductLicense ProductLicense);

        /// <summary>
        /// Check Project Code 
        /// </summary>
        /// <param name="city">Check Project Code</param>
        /// <returns></returns>
        
        void InsertMultipleProductLink(ProductLicenseAddendumLink AddendumLink);

        //Create by Saddam on 20/07/2016

        void InsertSearchHistory(ProductLicenseHistory _searchHistory);


        //Added By Ankush on 21/07/2016 For Get all List of ProductLicenseSubsidiaryRights
        /// <summary>
        /// GetAll ProductLicenseSubsidiaryRights List
        /// </summary>
        /// <param name="Geographical">GetAll ProductLicenseSubsidiaryRights Object</param>
        /// <returns>ProductLicenseSubsidiaryRights List</returns>
        IList<ProductLicenseSubsidiaryRights> GetProductLicenseSubsidiaryRightsList();

        //Added by prakash
        /// GetAll ProductLicense List
        /// </summary>
        /// <param name="Product">GetAll ProductLicense Object</param>
        /// <returns>ProductLicense List</returns>
        IList<ProductLicense> GetAllProductLicenseList();


        //Added by Ankush on 15/09/2016 For Get list ProductLicenseAddendumLink on the base of LicenseId
        /// GetProductLicenseAddendumLink List
        /// </summary>
        /// <param name="LicenseId">int</param>
        /// <returns>ProductLicenseAddendumLink List</returns>
        IList<ProductLicenseAddendumLink> GetProductLicenseAddendumLinkList(int LicenseId);


        //Added by Ankush on 23/09/2016
        IList<ProductLicenseRoyality> GetProductLicenseRoyalityList();

    }
}
