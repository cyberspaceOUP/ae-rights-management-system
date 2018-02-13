using ACS.Core.Domain.Master;
using ACS.Core.Domain.Product;
using ACS.Core.Domain.AuthorContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ACS.Services.Product
{
    public partial interface IProductMasterService
    {

        /// <summary>
        /// Insert Product 
        /// </summary>
        /// <param name="city">City class object</param>
        /// <returns></returns>
        int InsertProductMaster(ProductMaster Product);

         /// <summary>
        /// Update Product 
        /// </summary>
        /// <param name="city">City class object</param>
        /// <returns></returns>
        void UpdateProductMaster(ProductMaster Product);

        /// <summary>
        /// Update Product Proprietor
        /// </summary>
        /// <param name="city">City class object</param>
        /// <returns></returns>
        void UpdateProprietorMaster(ProprietorMaster Proprietor);


         /// <summary>
        /// Delete ProprietorMaster 
        /// </summary>
        /// <param name="city">City class object</param>
        /// <returns></returns>
        void DeleteProductProprietorMaster(ProprietorMaster Proprietor);


        /// <summary>
        /// Delete Proprietor Author link
        /// </summary>
        /// <param name="city">City class object</param>
        /// <returns></returns>
        void DeleteProprietorAuthorLink(ProprietorAuthorLink ProprietorAuthor);

        /// <summary>
        /// Delete Product Author link
        /// </summary>
        /// <param name="city">City class object</param>
        /// <returns></returns>
        void DeleteProductAuthorLink(ProductAuthorLink ProductAuthor);

        /// <summary>
        /// Check Project Code 
        /// </summary>
        /// <param name="city">Check Project Code</param>
        /// <returns></returns>
        string DuplicityProjectCodeCheck(ProductMaster Product);

        /// <summary>
        /// Check Project Code 
        /// </summary>
        /// <param name="city">Check Project Code</param>
        /// <returns></returns>
        string DuplicityISBNCheck(ProductMaster Product);

        /// <summary>
        /// Check Author Contract //add by Ankush 19/10/2016
        /// This list is used for Deriative Products if Author Contract not entered of previous product then not able to entered
        /// </summary>
        /// <param name="city">Check Project Code</param>
        /// <returns></returns>
        IList<AuthorContractOriginal> CheckAuthorContract(int ProductId);

        /// <summary>
        /// Check Product Exist in Product License //add by Prakash on 16 Aug, 2017
        /// This list is used for Deriative Products if Product License not entered of previous product then not able to entered
        /// </summary>
        /// <param name="city">Check Project Code</param>
        /// <returns></returns>
        IList<ProductLicense> CheckProductLicense(int ProductId);

        /// <summary>
        /// Get Product Code 
        /// </summary>
        /// <param name="city">Get Product Code</param>
        /// <returns></returns>
        string GetProductCode(int productCategoryId);

        /// <summary>
        /// Get Product Master By ProductId 
        /// </summary>
        /// <param name="city">Get Product Master Details</param>
        /// <returns></returns>
        ProductMaster GetProductById(ProductMaster Product);

        /// <summary>
        /// Get Product Master By ProductId 
        /// </summary>
        /// <param name="city">Get Product Master Details</param>
        /// <returns></returns>
        IList<ProprietorAuthorLink> GetProprietorAuthorByProprietorId(ProprietorMaster Proprietor);


        /// <summary>
        /// Get Product Master By ProductId 
        /// </summary>
        /// <param name="city">Get Product Master Details</param>
        /// <returns></returns>
        int ValidISBN(string ISBN);

        /// <summary>
        /// Get Product Master By ProductId 
        /// </summary>
        /// <param name="city">Get Product Master Details</param>
        /// <returns></returns>
        string GetProductISBNById(int ProductId);


        /// <summary>
        /// Delete Proprietor Author link
        /// </summary>
        /// <param name="city">City class object</param>
        /// <returns></returns>
        void DeletePreviousProductLink(ProductPreviousProductLink ProductPreviousProductLink);

        /// </summary>
        /// <param>NotEnteredSAPAgreementNoList</param>
        /// <returns></returns>
        IList<ProductSapAgreementTemp> NotEnteredSAPAgreementNoList();

        IList<ProductSapAgreementTemp> NotEnteredISBNForProductList();

        /// </summary> List of product whose author contract yet not signed
        /// <param>NotEnteredSAPAgreementNoList</param>
        /// <returns>Dheeraj lkumar sharma</returns>
        IEnumerable<object> AuthorProductNotSigned();

        /// </summary> List of product whose licence not maked
        /// <param>NotEnteredSAPAgreementNoList</param>
        /// <returns>Dheeraj lkumar sharma</returns>
        IEnumerable<object> LicenceProductNotSigned();


        void InsertProductSAPAggreentDetails(ProductSAPAgreementMaster ProductSAPAgreementMaster);


        /// <summary>
        /// Get Product Master By ProductId 
        /// </summary>
        /// <param name="city">Get Product Master Details</param>
        /// <returns></returns>
        IList<ProductAuthorLink> ProductAuthorList(ProductAuthorLink PAuthorLink);

          /// </summary>To insert the search Parameter
        /// <param>Insert search parameter to maintain the search history</param>
        /// <returns>Dheeraj lkumar sharma</returns>
        void InsertSearchHistory(SearchHistory _SearchParam);


        //Add By Ankush On 21/07/2016 For getting all list of Product Master
        /// <summary>
        /// GetAll ProductMaster List
        /// </summary>
        /// <param name="Geographical">GetAll ProductMaster Object</param>
        /// <returns>ProductMaster List</returns>
        IList<ProductMaster> GetProductMasterList();

        //Add By Ankush On 28/07/2016 For Check duplicates in ProductSAPAgreementMaster
        /// <summary>
        /// duplicates in ProductSAPAgreementMaster
        /// </summary>
        /// <returns>string</returns>
        string DuplicityProductSAPAgreementMasterCheck(ProductSAPAgreementMaster productSAPAgreement);

        //Added by dheeraj sharma for getting series details 
        /// <summary>
        /// duplicates in ProductSAPAgreementMaster
        /// </summary>
        /// <returns>string</returns>
        ProductMaster getSeriesDetails(int ProductId);

        //Added by ankush kumar for insert kit isbn 
        /// <summary>
        /// Insert kit 
        /// </summary>
        /// <param name="city">kit isbn</param>
        /// <returns></returns>
        int InsertKitMaster(KitISBN kitIsbndata);

        //Added by ankush kumar for get kit isbn 
        /// <summary>
        /// get kit by id
        /// </summary>
        /// <param name="city">get kit isbn</param>
        /// <returns></returns>
        KitISBN GetKitISBNById(KitISBN kitISBN);

        /// <summary>
        /// Check Project Code 
        /// </summary>
        /// <param name="city">Check Project Code</param>
        /// <returns></returns>
        string DuplicityKitISBNCheck(KitISBN kitIsbndata);

        /// <summary>
        /// Insert Product 
        /// </summary>
        /// <param name="city">kit isbn</param>
        /// <returns></returns>
        int DeleteKitMaster(KitISBN kitIsbndata);

        //Added by ankush kumar for get kit isbn 
        /// <summary>
        /// get kit by id
        /// </summary>
        /// <param name="city">get kit isbn</param>
        /// <returns></returns>
        KitProductLink GetKitProductLinkById(KitProductLink kitProductLink);

        /// <summary>
        /// Insert Product Proprietor
        /// </summary>
        /// <param name="city">City class object</param>
        /// <returns></returns>
        void InsertProprietorMaster(ProprietorMaster Proprietor);

        /// <summary>
        /// Get sap agreement Master By id 
        /// </summary>
        /// <param name="city">Get sap agreement Master Details</param>
        /// <returns></returns>
        ProductSAPAgreementMaster GetSapAgreementNumberById(ProductSAPAgreementMaster ProductSAPAgreement);

        //Added by Saddam on 28/08/2017 for SapAggrement Update
        void UpdateSapAggrement(ProductSAPAgreementMaster ProductSAPAgreement);
        //ended by Saddam



        //Added by Saddam on 12/10/2017 for Product License For Kit  Update
        void UpdateProductLicenseForKit(ProductLicense ProductLicense);
        //ended by Saddam



    }
   
}
