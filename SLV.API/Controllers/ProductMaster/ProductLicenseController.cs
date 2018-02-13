using ACS.Core;
using ACS.Core.Domain.Master;
using ACS.Core.Domain.Product;
using ACS.Data;
using ACS.Services.Localization;
using ACS.Services.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using ACS.Services.User;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using SLV.API.Controllers.JsonSerializer;
using ACS.Services.Master;
using ACS.Core.Data;
using Logger;

namespace SLV.API.Controllers.ProductMaster
{
    public class ProductLicenseController : ApiController
    {
        #region Variables
        private Logger.ILogger _ILog = LoggerFactory.getLogger();
        #endregion

        private readonly IProductLicenseService _ProductLicenseService;
        private readonly ILocalizationService _localizationService;
        private readonly IDbContext _dbContext;
        private readonly IProductMasterService _ProductMasterService;
        private readonly IProductType _IProductType;

        private readonly IRepository<ProductLicense> _ProductLicense;
        private readonly IRepository<PublishingCompanyMaster> _PublishingCompanyMaster;

        public ProductLicenseController(
                IProductLicenseService ProductLicenseService
              , ILocalizationService localizationService
              , IDbContext dbContext
            ,IProductMasterService ProductMasterService
            , IProductType IProductType

            , IRepository<ProductLicense> ProductLicense
            , IRepository<PublishingCompanyMaster> PublishingCompanyMaster
            )
        {

            _ProductLicenseService = ProductLicenseService;
            _localizationService = localizationService;
            this._dbContext = dbContext;
            _ProductMasterService = ProductMasterService;
            _IProductType = IProductType;

            _ProductLicense = ProductLicense;
            _PublishingCompanyMaster = PublishingCompanyMaster;
        }

        public IHttpActionResult InsertProductLicense(ACS.Core.Domain.Product.ProductLicense ProductLicense)
        {
            DateTime? value = null;
            int? nullablevalue = null;
            string status = "";
            int Id = 0;
            try
            {
                //status = _ProductMasterService.DuplicityProjectCodeCheck(Product);
                if (status != "duplicate")
                {
                    if (ProductLicense.Id == 0)
                    {
                        ACS.Core.Domain.Product.ProductLicense mobj_ProductLicense = new ACS.Core.Domain.Product.ProductLicense();

                        mobj_ProductLicense.productid = ProductLicense.productid;
                        mobj_ProductLicense.publishingcompanyid = ProductLicense.publishingcompanyid;
                        mobj_ProductLicense.ContactPerson = ProductLicense.ContactPerson;
                        mobj_ProductLicense.Address = ProductLicense.Address;
                        mobj_ProductLicense.CountryId = ProductLicense.CountryId;
                        mobj_ProductLicense.OtherCountry = ProductLicense.OtherCountry;
                        mobj_ProductLicense.Stateid = ProductLicense.Stateid;
                        mobj_ProductLicense.OtherState = ProductLicense.OtherState;
                        mobj_ProductLicense.Cityid = ProductLicense.Cityid;
                        mobj_ProductLicense.OtherCity = ProductLicense.OtherCity;
                        mobj_ProductLicense.Pincode = ProductLicense.Pincode;
                        mobj_ProductLicense.Mobile = ProductLicense.Mobile;
                        mobj_ProductLicense.Email = ProductLicense.Email;
                        mobj_ProductLicense.Requestdate = Convert.ToDateTime(ProductLicense.Requestdate);
                        
                        mobj_ProductLicense.ContractDate = null;
                        mobj_ProductLicense.effectivedate = null;
                        //mobj_ProductLicense.contractperiodinmonth = null;
                        mobj_ProductLicense.Expirydate = null;
                        mobj_ProductLicense.Impressionwithindate = ProductLicense.Impressionwithindate;
                        mobj_ProductLicense.noofimpressions = ProductLicense.noofimpressions;
                        mobj_ProductLicense.printquantitytype = ProductLicense.printquantitytype;
                        mobj_ProductLicense.printquantity = ProductLicense.printquantity;
                        mobj_ProductLicense.balanceqty = ProductLicense.printquantity;
                        mobj_ProductLicense.balanceqtycf = "N";
                        mobj_ProductLicense.RoyalityTerms = ProductLicense.RoyalityTerms;
                        mobj_ProductLicense.PaymentAmount = ProductLicense.PaymentAmount;
                        mobj_ProductLicense.AdvancedAmount = ProductLicense.AdvancedAmount;
                        mobj_ProductLicense.copiesforlicensor = ProductLicense.copiesforlicensor;
                        mobj_ProductLicense.pricetype = ProductLicense.pricetype;
                        mobj_ProductLicense.Currencyid = ProductLicense.Currencyid;
                        mobj_ProductLicense.price = ProductLicense.price;
                        mobj_ProductLicense.thirdpartypermission = ProductLicense.thirdpartypermission;
                        mobj_ProductLicense.Remarks = ProductLicense.Remarks;
                       

                        IList<ProductLicenseRoyality> _IProductLicenseRoyality = new List<ProductLicenseRoyality>();


                        
                        foreach (ProductLicenseRoyality prdcteRoyality in ProductLicense.PProductLicenseRoyality)
                        {
                            // Author Details Set
                            ProductLicenseRoyality _ProductLicenseRoyality = new ProductLicenseRoyality();
                            _ProductLicenseRoyality.ProductSubTypeId = prdcteRoyality.ProductSubTypeId;
                            _ProductLicenseRoyality.copiesfrom = prdcteRoyality.copiesfrom;
                            _ProductLicenseRoyality.copiesto = prdcteRoyality.copiesto;
                            _ProductLicenseRoyality.percentage = prdcteRoyality.percentage;
                            _ProductLicenseRoyality.Deactivate = "N";
                            _ProductLicenseRoyality.EnteredBy = ProductLicense.EnteredBy;
                            _ProductLicenseRoyality.EntryDate = DateTime.Now;
                            _ProductLicenseRoyality.ModifiedBy = null;
                            _ProductLicenseRoyality.ModifiedDate = null;
                            _ProductLicenseRoyality.DeactivateBy = null;
                            _ProductLicenseRoyality.DeactivateDate = null;
                            _IProductLicenseRoyality.Add(_ProductLicenseRoyality);
                        }


                        IList<ProductLicenseSubsidiaryRights> _IProductLicenseSubsidiaryRights = new List<ProductLicenseSubsidiaryRights>();



                        foreach (ProductLicenseSubsidiaryRights SubsidiaryRights in ProductLicense.PProductLicenseSubsidiaryRights)
                        {
                            // Author Details Set
                            ProductLicenseSubsidiaryRights _ProductLicenseSubsidiaryRights = new ProductLicenseSubsidiaryRights();
                            _ProductLicenseSubsidiaryRights.publisherpercentage = SubsidiaryRights.publisherpercentage;
                            _ProductLicenseSubsidiaryRights.ouppercentage = SubsidiaryRights.ouppercentage;
                            _ProductLicenseSubsidiaryRights.subsidiaryrightsid = SubsidiaryRights.subsidiaryrightsid;
                            _ProductLicenseSubsidiaryRights.Deactivate = "N";
                            _ProductLicenseSubsidiaryRights.EnteredBy = ProductLicense.EnteredBy;
                            _ProductLicenseSubsidiaryRights.EntryDate = DateTime.Now;
                            _ProductLicenseSubsidiaryRights.ModifiedBy = null;
                            _ProductLicenseSubsidiaryRights.ModifiedDate = null;
                            _ProductLicenseSubsidiaryRights.DeactivateBy = null;
                            _ProductLicenseSubsidiaryRights.DeactivateDate = null;
                            _IProductLicenseSubsidiaryRights.Add(_ProductLicenseSubsidiaryRights);
                        }

                        IList<ProductLicenseAddendumLink> _IProductLicenseAddendumLink = new List<ProductLicenseAddendumLink>();


                        // Addendum Details Set
                        ProductLicenseAddendumLink _ProductLicenseAddendumLink = new ProductLicenseAddendumLink();
                        _ProductLicenseAddendumLink.ProductId = ProductLicense.productid;                        
                        _ProductLicenseAddendumLink.Active = "Y";
                        _ProductLicenseAddendumLink.Deactivate = "N";
                        _ProductLicenseAddendumLink.EnteredBy = ProductLicense.EnteredBy;
                        _ProductLicenseAddendumLink.EntryDate = DateTime.Now;
                        _ProductLicenseAddendumLink.ModifiedBy = null;
                        _ProductLicenseAddendumLink.ModifiedDate = null;
                        _ProductLicenseAddendumLink.DeactivateBy = null;
                        _ProductLicenseAddendumLink.DeactivateDate = null;
                        _IProductLicenseAddendumLink.Add(_ProductLicenseAddendumLink);
                       


                        mobj_ProductLicense.PProductLicenseRoyality = _IProductLicenseRoyality;
                        mobj_ProductLicense.PProductLicenseSubsidiaryRights = _IProductLicenseSubsidiaryRights;
                        mobj_ProductLicense.ProductLicenseAddendumLink = _IProductLicenseAddendumLink;
                        mobj_ProductLicense.LicenseStatus = "P";
                        mobj_ProductLicense.Deactivate = "N";
                        mobj_ProductLicense.EnteredBy = ProductLicense.EnteredBy;
                        mobj_ProductLicense.EntryDate = DateTime.Now;
                        mobj_ProductLicense.ModifiedBy = null;
                        mobj_ProductLicense.ModifiedDate = null;
                        mobj_ProductLicense.DeactivateBy = null;
                        mobj_ProductLicense.DeactivateDate = null;
                        mobj_ProductLicense.Territoryrightsid = ProductLicense.Territoryrightsid;

                        ProductLicense = _ProductLicenseService.InsertProductLicenseMaster(mobj_ProductLicense);

                    }
                    status = "OK";
                }
                else
                {
                    status = "Duplicate";
                }



            }
            catch (ACSException ex)
            {
                status = ex.ToString();
            }
            catch (Exception ex)
            {
                status = ex.ToString();
            }

            return Json(new { status, ProductLicense.Id, ProductLicense.ProductLicensecode });
        }

        public IHttpActionResult UpdateProductLicense(ACS.Core.Domain.Product.ProductLicense ProductLicense)
        {
            string status = "";
            int Id = 0;
            try
            {
                //status = _ProductMasterService.DuplicityProjectCodeCheck(Product);
                if (status != "duplicate")
                {
                    if (ProductLicense.Id > 0)
                    {
                        ACS.Core.Domain.Product.ProductLicense mobj_ProductLicense = _ProductLicenseService.GetProductLicenseById(ProductLicense);
                        List<ProductLicenseRoyality> _IOldProductLicenseRoyality = mobj_ProductLicense.PProductLicenseRoyality.ToList();
                        List<ProductLicenseSubsidiaryRights> _IOldProductLicenseSubsidiaryRights = mobj_ProductLicense.PProductLicenseSubsidiaryRights.ToList();
                        List<ProductLicenseUpdateDetails> _IOldProductLicenseUpdateDetails = mobj_ProductLicense.IProductLicenseUpdateDetails.ToList();
                        //List<ProductLicenseFileDetails> _IOldProductLicenseFileDetails = mobj_ProductLicense.IProductLicenseUpdateDetails..ToList();


                        foreach (ProductLicenseRoyality Royality in _IOldProductLicenseRoyality)
                        {
                            _ProductLicenseService.DeleteProDuctLicenseRoyality(Royality);
                        }

                        foreach (ProductLicenseSubsidiaryRights SubsidiaryRights in _IOldProductLicenseSubsidiaryRights)
                        {
                            _ProductLicenseService.DeleteProductLicenseSubsidiaryRights(SubsidiaryRights);
                        }

                        foreach (ProductLicenseUpdateDetails UpdateDetails in _IOldProductLicenseUpdateDetails)
                        {

                            List<ProductLicenseFileDetails> _IOldProductLicenseFileDetails = UpdateDetails.ILicenseUpdateFileDetails.ToList();
                            foreach (ProductLicenseFileDetails FileDetails in _IOldProductLicenseFileDetails)
                            {
                               _ProductLicenseService.DeleteProductLicenseFileDetails(FileDetails);
                            }
                             //UpdateDetails.ILicenseUpdateFileDetails = null;
                        }

                        foreach (ProductLicenseUpdateDetails UpdateDetails in _IOldProductLicenseUpdateDetails)
                        {
                            _ProductLicenseService.DeleteProductLicenseUpdateDetails(UpdateDetails);
                        }

                        mobj_ProductLicense.publishingcompanyid = ProductLicense.publishingcompanyid;
                        mobj_ProductLicense.ContactPerson = ProductLicense.ContactPerson;
                        mobj_ProductLicense.Address = ProductLicense.Address;
                        mobj_ProductLicense.CountryId = ProductLicense.CountryId;
                        mobj_ProductLicense.OtherCountry = ProductLicense.OtherCountry;
                        mobj_ProductLicense.Stateid = ProductLicense.Stateid;
                        mobj_ProductLicense.OtherState = ProductLicense.OtherState;
                        mobj_ProductLicense.Cityid = ProductLicense.Cityid;
                        mobj_ProductLicense.OtherCity = ProductLicense.OtherCity;
                        mobj_ProductLicense.Pincode = ProductLicense.Pincode;
                        mobj_ProductLicense.Mobile = ProductLicense.Mobile;
                        mobj_ProductLicense.Email = ProductLicense.Email;
                        mobj_ProductLicense.Requestdate = Convert.ToDateTime(ProductLicense.Requestdate);
                        mobj_ProductLicense.ContractDate = ProductLicense.ContractDate;
                        mobj_ProductLicense.effectivedate = ProductLicense.effectivedate;
                        //mobj_ProductLicense.contractperiodinmonth = ProductLicense.contractperiodinmonth;
                        mobj_ProductLicense.Expirydate = Convert.ToDateTime(ProductLicense.Expirydate);
                        mobj_ProductLicense.Impressionwithindate = ProductLicense.Impressionwithindate;
                        mobj_ProductLicense.noofimpressions = ProductLicense.noofimpressions;
                        mobj_ProductLicense.printquantitytype = ProductLicense.printquantitytype;
                        mobj_ProductLicense.printquantity = ProductLicense.printquantity;
                        mobj_ProductLicense.RoyalityTerms = ProductLicense.RoyalityTerms;
                        mobj_ProductLicense.PaymentAmount = ProductLicense.PaymentAmount;
                        mobj_ProductLicense.AdvancedAmount = ProductLicense.AdvancedAmount;
                        mobj_ProductLicense.copiesforlicensor = ProductLicense.copiesforlicensor;
                        mobj_ProductLicense.pricetype = ProductLicense.pricetype;
                        mobj_ProductLicense.Currencyid = ProductLicense.Currencyid;
                        mobj_ProductLicense.price = ProductLicense.price;
                        mobj_ProductLicense.thirdpartypermission = ProductLicense.thirdpartypermission;
                        mobj_ProductLicense.Remarks = ProductLicense.Remarks;


                        IList<ProductLicenseRoyality> _IProductLicenseRoyality = new List<ProductLicenseRoyality>();



                        foreach (ProductLicenseRoyality prdcteRoyality in ProductLicense.PProductLicenseRoyality)
                        {
                            // Author Details Set
                            ProductLicenseRoyality _ProductLicenseRoyality = new ProductLicenseRoyality();
                            _ProductLicenseRoyality.ProductSubTypeId = prdcteRoyality.ProductSubTypeId;
                            _ProductLicenseRoyality.copiesfrom = prdcteRoyality.copiesfrom;
                            _ProductLicenseRoyality.copiesto = prdcteRoyality.copiesto;
                            _ProductLicenseRoyality.percentage = prdcteRoyality.percentage;
                            _ProductLicenseRoyality.Deactivate = "N";
                            _ProductLicenseRoyality.EnteredBy = ProductLicense.EnteredBy;
                            _ProductLicenseRoyality.EntryDate = DateTime.Now;
                            _ProductLicenseRoyality.ModifiedBy = null;
                            _ProductLicenseRoyality.ModifiedDate = null;
                            _ProductLicenseRoyality.DeactivateBy = null;
                            _ProductLicenseRoyality.DeactivateDate = null;
                            _IProductLicenseRoyality.Add(_ProductLicenseRoyality);
                        }


                        IList<ProductLicenseSubsidiaryRights> _IProductLicenseSubsidiaryRights = new List<ProductLicenseSubsidiaryRights>();



                        foreach (ProductLicenseSubsidiaryRights SubsidiaryRights in ProductLicense.PProductLicenseSubsidiaryRights)
                        {
                            // Author Details Set
                            ProductLicenseSubsidiaryRights _ProductLicenseSubsidiaryRights = new ProductLicenseSubsidiaryRights();
                            _ProductLicenseSubsidiaryRights.publisherpercentage = SubsidiaryRights.publisherpercentage;
                            _ProductLicenseSubsidiaryRights.ouppercentage = SubsidiaryRights.ouppercentage;
                            _ProductLicenseSubsidiaryRights.subsidiaryrightsid = SubsidiaryRights.subsidiaryrightsid;
                            _ProductLicenseSubsidiaryRights.Deactivate = "N";
                            _ProductLicenseSubsidiaryRights.EnteredBy = ProductLicense.EnteredBy;
                            _ProductLicenseSubsidiaryRights.EntryDate = DateTime.Now;
                            _ProductLicenseSubsidiaryRights.ModifiedBy = null;
                            _ProductLicenseSubsidiaryRights.ModifiedDate = null;
                            _ProductLicenseSubsidiaryRights.DeactivateBy = null;
                            _ProductLicenseSubsidiaryRights.DeactivateDate = null;
                            _IProductLicenseSubsidiaryRights.Add(_ProductLicenseSubsidiaryRights);
                        }


                        IList<ProductLicenseUpdateDetails> _IProductLicenseUpdateDetails = new List<ProductLicenseUpdateDetails>();



                        foreach (ProductLicenseUpdateDetails UpdateDetails in ProductLicense.IProductLicenseUpdateDetails)
                        {

                            IList<ProductLicenseFileDetails> _IProductLicenseFileDetails = new List<ProductLicenseFileDetails>();



                            foreach (ProductLicenseFileDetails FileDetails in UpdateDetails.ILicenseUpdateFileDetails)
                            {

                                // Author Details Set
                                ProductLicenseFileDetails _ProductLicenseFileDetails = new ProductLicenseFileDetails();
                                _ProductLicenseFileDetails.LicenseId = FileDetails.LicenseId;
                                _ProductLicenseFileDetails.FileName = FileDetails.FileName;
                                _ProductLicenseFileDetails.UploadFileName = FileDetails.UploadFileName;
                                _ProductLicenseFileDetails.Deactivate = "N";
                                _ProductLicenseFileDetails.EnteredBy = ProductLicense.EnteredBy;
                                _ProductLicenseFileDetails.EntryDate = DateTime.Now;
                                _ProductLicenseFileDetails.ModifiedBy = null;
                                _ProductLicenseFileDetails.ModifiedDate = null;
                                _ProductLicenseFileDetails.DeactivateBy = null;
                                _ProductLicenseFileDetails.DeactivateDate = null;
                                _IProductLicenseFileDetails.Add(_ProductLicenseFileDetails);
                            }
                            
                            
                            
                            // Author Details Set
                            ProductLicenseUpdateDetails _ProductLicenseUpdateDetails = new ProductLicenseUpdateDetails();

                            _ProductLicenseUpdateDetails.LicensorCopiesSentDate = UpdateDetails.LicensorCopiesSentDate;
                            _ProductLicenseUpdateDetails.EFilesCost = UpdateDetails.EFilesCost;
                            _ProductLicenseUpdateDetails.EFilesRequestDate = UpdateDetails.EFilesRequestDate;
                            _ProductLicenseUpdateDetails.EFilesReceivedDate = UpdateDetails.EFilesReceivedDate;
                            _ProductLicenseUpdateDetails.Mode = UpdateDetails.Mode;
                            _ProductLicenseUpdateDetails.ILicenseUpdateFileDetails = _IProductLicenseFileDetails;
                            _ProductLicenseUpdateDetails.Deactivate = "N";
                            _ProductLicenseUpdateDetails.EnteredBy = ProductLicense.EnteredBy;
                            _ProductLicenseUpdateDetails.EntryDate = DateTime.Now;
                            _ProductLicenseUpdateDetails.ModifiedBy = null;
                            _ProductLicenseUpdateDetails.ModifiedDate = null;
                            _ProductLicenseUpdateDetails.DeactivateBy = null;
                            _ProductLicenseUpdateDetails.DeactivateDate = null;
                            _ProductLicenseUpdateDetails.AgreementDate = UpdateDetails.AgreementDate;
                            _ProductLicenseUpdateDetails.Effectivedate = UpdateDetails.Effectivedate;
                            //_ProductLicenseUpdateDetails.Contractperiodinmonth = UpdateDetails.Contractperiodinmonth;
                            _ProductLicenseUpdateDetails.Expirydate = Convert.ToDateTime(UpdateDetails.Expirydate);
                            _IProductLicenseUpdateDetails.Add(_ProductLicenseUpdateDetails);
                        }




                        mobj_ProductLicense.PProductLicenseRoyality = _IProductLicenseRoyality;
                        mobj_ProductLicense.PProductLicenseSubsidiaryRights = _IProductLicenseSubsidiaryRights;
                        mobj_ProductLicense.IProductLicenseUpdateDetails = _IProductLicenseUpdateDetails;
                        //mobj_ProductLicense.Deactivate = "N";
                        //mobj_ProductLicense.EnteredBy = ProductLicense.EnteredBy;
                        //mobj_ProductLicense.EntryDate = DateTime.Now;
                        mobj_ProductLicense.ModifiedBy = ProductLicense.EnteredBy;
                        mobj_ProductLicense.ModifiedDate = DateTime.Now;
                        mobj_ProductLicense.DeactivateBy = null;
                        mobj_ProductLicense.DeactivateDate = null;
                        mobj_ProductLicense.Territoryrightsid = ProductLicense.Territoryrightsid;

                         _ProductLicenseService.UpdateProductLicense(mobj_ProductLicense);

                    }
                    status = "OK";
                }
                else
                {
                    status = "Duplicate";
                }



            }
            catch (ACSException ex)
            {
                status = ex.ToString();
            }
            catch (Exception ex)
            {
                status = ex.ToString();
            }

            return Json(new { status, Id });
        }

        public IHttpActionResult ProductLicenseSearch(ACS.Core.Domain.Product.ProductLicenseHistory SearchParam)
        {

            if (SearchParam.SessionId == "")
            {
                return Json("NOK");
            }
            else
            {
                var status = "";
                _ProductLicenseService.InsertSearchHistory(SearchParam);
                status = "OK";
                return Json(status);
            }
        }


        public IHttpActionResult GetProductLicenseSearchList(String SessionId)
        {
            try
            {
                if (SessionId == "")
                {
                    return Json("NOK");
                }
                else
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("sessionId", SqlDbType.VarChar, 200);
                    parameters[0].Value = "'" + SessionId + "'";
                    var _GetAuthorReport1 = _dbContext.ExecuteStoredProcedureListNewData<ProductLicenseSearch.ClsSearchReport>("Proc_ProductLicenseDetails_get", parameters).ToList();
                    if (_GetAuthorReport1.Count == 0)
                    {
                        parameters = new SqlParameter[1];
                        parameters[0] = new SqlParameter("sessionId", SqlDbType.VarChar, 200);
                        parameters[0].Value = "'" + SessionId + "'";

                        _GetAuthorReport1 = _dbContext.ExecuteStoredProcedureListNewData<ProductLicenseSearch.ClsSearchReport>("Proc_ProductLicenseDetailsByChild_get", parameters).ToList();
                    }

                    var royalty = _ProductLicenseService.GetProductLicenseRoyalityList();
                    var ProductTypeMaster = _IProductType.GetAllProductTypeList();

                    var _GetAuthorReport = (from data in _GetAuthorReport1
                                            select new
                                            {
                                                LicenseId = data.LicenseId,
                                                ProductId = data.ProductId,
                                                productlicensecode = data.productlicensecode,
                                                productcode = data.productcode,
                                                projectcode = data.projectcode,
                                                WorkingTitle = data.WorkingTitle,
                                                OUPISBN = data.OUPISBN,
                                                ContractDate = data.ContractDate,
                                                Expirydate = data.Expirydate,
                                                AddendumId = data.AddendumId,
                                                AddendumCode = data.AddendumCode,
                                                flag = data.flag,
                                                AuthorName = data.AuthorName,
                                                ProprietorCompany = data.ProprietorCompany,
                                                WorkingSubProduct = data.WorkingSubProduct,
                                                AgreementDate = data.AgreementDate,

                                                AgreementDateForSort = data.AgreementDateForSort,
                                                ExpirydateForSort = data.ExpirydateForSort, 

                                                Royalty = from rol in royalty.Where(a => a.ProductLicenseid == data.LicenseId).OrderBy(a=> a.ProductSubTypeId)
                                                          join type in ProductTypeMaster
                                                          on rol.ProductSubTypeId equals type.Id
                                                          select new
                                                          {
                                                              SubProductType = type.typeName,
                                                              CopiesFrom = rol.copiesfrom,
                                                              CopiesTo = rol.copiesto
                                                          }
                                            });

                    return Json(JsonSerializer.SerializeObj.SerializeObject(_GetAuthorReport));
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public IHttpActionResult ProductLicenseUpdateDetails(ACS.Core.Domain.Product.ProductLicense ProductLicense)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            SqlParameter[] parameters1 = new SqlParameter[2];
            SqlParameter[] parameters2 = new SqlParameter[2];
            SqlParameter[] parameters3 = new SqlParameter[2];

            try
            {
                if (ProductLicense != null)
                {

                    parameters[0] = new SqlParameter("productLicenseId", SqlDbType.VarChar, 50);
                    if (ProductLicense.Id == 0)
                    {
                        parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[0].Value = ProductLicense.Id;
                    }
                    parameters[1] = new SqlParameter("type", SqlDbType.VarChar, 50);
                    parameters[1].Value = "1";

                    parameters1[0] = new SqlParameter("productLicenseId", SqlDbType.VarChar, 50);
                    if (ProductLicense.Id == 0)
                    {
                        parameters1[0].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters1[0].Value = ProductLicense.Id;
                    }
                    parameters1[1] = new SqlParameter("type", SqlDbType.VarChar, 50);
                    parameters1[1].Value = "2";


                    parameters2[0] = new SqlParameter("productLicenseId", SqlDbType.VarChar, 50);
                    if (ProductLicense.Id == 0)
                    {
                        parameters2[0].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters2[0].Value = ProductLicense.Id;
                    }
                    parameters2[1] = new SqlParameter("type", SqlDbType.VarChar, 50);
                    parameters2[1].Value = "3";

                    parameters3[0] = new SqlParameter("productLicenseId", SqlDbType.VarChar, 50);
                    if (ProductLicense.Id == 0)
                    {
                        parameters3[0].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters3[0].Value = ProductLicense.Id;
                    }
                    parameters3[1] = new SqlParameter("type", SqlDbType.VarChar, 50);
                    parameters3[1].Value = "4";

                }


            }
            catch (Exception ex)
            {
            }
            var ProductLicenseDetails = _dbContext.ExecuteStoredProcedureListNewData<ProductLicenseUpdate>("Proc_ProductLicenseUpdateDetails_get", parameters).ToList();
            var ProductLicenseRoyalslab = _dbContext.ExecuteStoredProcedureListNewData<ProductLicenseUpdate.ClsRoyaltySlab>("Proc_ProductLicenseUpdateDetails_get", parameters1).ToList();
            var ProductLicenseSubsidiaryRights = _dbContext.ExecuteStoredProcedureListNewData<ProductLicenseUpdate.ClsSubsidiaryRights>("Proc_ProductLicenseUpdateDetails_get", parameters2).ToList();
            var FileDetails = _dbContext.ExecuteStoredProcedureListNewData<ProductLicenseUpdate.ClsFileDetails>("Proc_ProductLicenseUpdateDetails_get", parameters3).ToList();

            return Json(SerializeObj.SerializeObject(new { ProductLicenseDetails, ProductLicenseRoyalslab, ProductLicenseSubsidiaryRights, FileDetails }));
        }

        [HttpPost]
        public IHttpActionResult getProductLicenseById(ACS.Core.Domain.Product.ProductLicense ProductLicense)
        {
            ACS.Core.Domain.Product.ProductLicense ProductLicenseM1 = _ProductLicenseService.GetProductLicenseById(ProductLicense);

            ACS.Core.Domain.Product.ProductLicense ProductLicenseM = new ACS.Core.Domain.Product.ProductLicense();
            ProductLicenseM.Id = ProductLicenseM1.Id;
            ProductLicenseM.productid = ProductLicenseM1.productid;
            ProductLicenseM.ProductLicensecode = ProductLicenseM1.ProductLicensecode;
            ProductLicenseM.publishingcompanyid = ProductLicenseM1.publishingcompanyid;
            ProductLicenseM.ContactPerson = ProductLicenseM1.ContactPerson;
            ProductLicenseM.Address = ProductLicenseM1.Address;
            ProductLicenseM.CountryId = ProductLicenseM1.CountryId;
            ProductLicenseM.OtherCountry = ProductLicenseM1.OtherCountry;
            ProductLicenseM.Stateid = ProductLicenseM1.Stateid;
            ProductLicenseM.OtherState = ProductLicenseM1.OtherState;
            ProductLicenseM.Cityid = ProductLicenseM1.Cityid;
            ProductLicenseM.OtherCity = ProductLicenseM1.OtherCity;
            ProductLicenseM.Pincode = ProductLicenseM1.Pincode;
            ProductLicenseM.Mobile = ProductLicenseM1.Mobile;
            ProductLicenseM.Email = ProductLicenseM1.Email;
            ProductLicenseM.Requestdate = ProductLicenseM1.Requestdate;
            ProductLicenseM.ContractDate = ProductLicenseM1.ContractDate;
            ProductLicenseM.effectivedate = ProductLicenseM1.effectivedate;
            //ProductLicenseM.contractperiodinmonth = ProductLicenseM1.contractperiodinmonth;
            ProductLicenseM.Expirydate = ProductLicenseM1.Expirydate;
            ProductLicenseM.Impressionwithindate = ProductLicenseM1.Impressionwithindate;
            ProductLicenseM.noofimpressions = ProductLicenseM1.noofimpressions;
            ProductLicenseM.printquantitytype = ProductLicenseM1.printquantitytype;
            ProductLicenseM.printquantity = ProductLicenseM1.printquantity;
            ProductLicenseM.RoyalityTerms = ProductLicenseM1.RoyalityTerms;
            ProductLicenseM.PaymentAmount = ProductLicenseM1.PaymentAmount;
            ProductLicenseM.AdvancedAmount = ProductLicenseM1.AdvancedAmount;
            ProductLicenseM.copiesforlicensor = ProductLicenseM1.copiesforlicensor;
            ProductLicenseM.pricetype = ProductLicenseM1.pricetype;
            ProductLicenseM.Currencyid = ProductLicenseM1.Currencyid;
            ProductLicenseM.price = ProductLicenseM1.price;
            ProductLicenseM.thirdpartypermission = ProductLicenseM1.thirdpartypermission;
            ProductLicenseM.Remarks = ProductLicenseM1.Remarks;
            ProductLicenseM.Deactivate = ProductLicenseM1.Deactivate;
            ProductLicenseM.Territoryrightsid = ProductLicenseM1.Territoryrightsid;
            ProductLicenseM.PProductLicenseRoyality = ProductLicenseM1.PProductLicenseRoyality;
            ProductLicenseM.PProductLicenseSubsidiaryRights = ProductLicenseM1.PProductLicenseSubsidiaryRights;
            ProductLicenseM.IProductLicenseUpdateDetails = ProductLicenseM1.IProductLicenseUpdateDetails;

            //-----get publishing company name
            var publishingcompanyName = _PublishingCompanyMaster.Table.Where(x => x.Deactivate == "N" && x.Id == ProductLicenseM1.publishingcompanyid).Select(x => x.CompanyName).FirstOrDefault();
            
            return Json(SerializeObj.SerializeObject(new { ProductLicenseM , publishingcompanyName }));

        }

        [HttpPost]
        public IHttpActionResult DeleteFile(ACS.Core.Domain.Product.ProductLicenseFileDetails FileDetails)
        {
            string status = "";
            try
            {
               ProductLicenseFileDetails mobj_FileDetails  = _ProductLicenseService.GetFileDetailsById(FileDetails);
                if (FileDetails.Id > 0)
                {
                    _ProductLicenseService.DeleteProductLicenseFileDetails(mobj_FileDetails);
                    status = "OK";
                }
                else
                {
                    status = "Opps";

                }

            }
            catch (ACSException ex)
            {
                status = ex.ToString();
            }
            catch (Exception ex)
            {
                status = ex.ToString();
            }

            return Json(status);
        }

        public IHttpActionResult InsertProductLicenseUpdateDetails(ACS.Core.Domain.Product.ProductLicenseUpdateDetails ProductLicenseUpdateDetails)
        {
            string status = "";
            int Id = 0;
            try
            {
                //status = _ProductLicenseService.DuplicityLicenseUpdateDetails(ProductLicenseUpdateDetails);
                //if (status != "duplicate")
                //{
                    if (ProductLicenseUpdateDetails.LicenseId > 0)
                    {

                        IList<ProductLicenseFileDetails> _IProductLicenseFileDetails = new List<ProductLicenseFileDetails>();

                        foreach (ProductLicenseFileDetails FileDetails in ProductLicenseUpdateDetails.ILicenseUpdateFileDetails)
                        {

                            // Author Details Set
                            ProductLicenseFileDetails _ProductLicenseFileDetails = new ProductLicenseFileDetails();
                            _ProductLicenseFileDetails.LicenseId = FileDetails.LicenseId;
                            _ProductLicenseFileDetails.FileName = FileDetails.FileName;
                            _ProductLicenseFileDetails.UploadFileName = FileDetails.UploadFileName;
                            _ProductLicenseFileDetails.Deactivate = "N";
                            _ProductLicenseFileDetails.EnteredBy = ProductLicenseUpdateDetails.EnteredBy;
                            _ProductLicenseFileDetails.EntryDate = DateTime.Now;
                            _ProductLicenseFileDetails.ModifiedBy = null;
                            _ProductLicenseFileDetails.ModifiedDate = null;
                            _ProductLicenseFileDetails.DeactivateBy = null;
                            _ProductLicenseFileDetails.DeactivateDate = null;
                            _IProductLicenseFileDetails.Add(_ProductLicenseFileDetails);
                        }



                        ProductLicenseUpdateDetails _ProductLicenseUpdateDetails = new ProductLicenseUpdateDetails();

                        _ProductLicenseUpdateDetails.LicenseId = ProductLicenseUpdateDetails.LicenseId;
                        _ProductLicenseUpdateDetails.LicensorCopiesSentDate = ProductLicenseUpdateDetails.LicensorCopiesSentDate;
                        _ProductLicenseUpdateDetails.EFilesCost = ProductLicenseUpdateDetails.EFilesCost;
                        _ProductLicenseUpdateDetails.EFilesRequestDate = ProductLicenseUpdateDetails.EFilesRequestDate;
                        _ProductLicenseUpdateDetails.EFilesReceivedDate = ProductLicenseUpdateDetails.EFilesReceivedDate;
                        _ProductLicenseUpdateDetails.Mode = ProductLicenseUpdateDetails.Mode;
                        _ProductLicenseUpdateDetails.ILicenseUpdateFileDetails = _IProductLicenseFileDetails;
                        _ProductLicenseUpdateDetails.Deactivate = "N";
                        _ProductLicenseUpdateDetails.EnteredBy = ProductLicenseUpdateDetails.EnteredBy;
                        _ProductLicenseUpdateDetails.EntryDate = DateTime.Now;
                        _ProductLicenseUpdateDetails.ModifiedBy = null;
                        _ProductLicenseUpdateDetails.ModifiedDate = null;
                        _ProductLicenseUpdateDetails.DeactivateBy = null;
                        _ProductLicenseUpdateDetails.DeactivateDate = null;
                        _ProductLicenseUpdateDetails.AgreementDate = ProductLicenseUpdateDetails.AgreementDate;
                        _ProductLicenseUpdateDetails.Effectivedate = ProductLicenseUpdateDetails.Effectivedate;
                        //_ProductLicenseUpdateDetails.Contractperiodinmonth = ProductLicenseUpdateDetails.Contractperiodinmonth;
                        _ProductLicenseUpdateDetails.Expirydate = ProductLicenseUpdateDetails.Expirydate;
                        _ProductLicenseService.InsertProductLicenseUpdateDetails(_ProductLicenseUpdateDetails);

                    }
                    status = "OK";
                //}
                //else
                //{
                    //status = "Duplicate";
                //}



            }
            catch (ACSException ex)
            {
                status = ex.ToString();
            }
            catch (Exception ex)
            {
                status = ex.ToString();
            }

            return Json(new { status, Id });
        }

        [HttpPost]
        public IHttpActionResult DeleteProductLicenseDetails(ACS.Core.Domain.Product.ProductLicense ProductLicense)
        {
            string status = "";
            int Id = 0;
            try
            {
                
                    if (ProductLicense.Id > 0)
                    {
                        ACS.Core.Domain.Product.ProductLicense mobj_ProductLicense = _ProductLicenseService.GetProductLicenseById(ProductLicense);
                        mobj_ProductLicense.PProductLicenseRoyality = null;
                        mobj_ProductLicense.PProductLicenseSubsidiaryRights = null;
                        mobj_ProductLicense.IProductLicenseUpdateDetails = null;
                        mobj_ProductLicense.Deactivate = "Y";
                        mobj_ProductLicense.DeactivateBy = ProductLicense.EnteredBy;
                        mobj_ProductLicense.DeactivateDate = ProductLicense.DeactivateDate;
                        mobj_ProductLicense.DeactivateRemarks = ProductLicense.DeactivateRemarks;

                        _ProductLicenseService.UpdateProductLicense(mobj_ProductLicense);

                    }
                    status = "OK";                

            }
            catch (ACSException ex)
            {
                status = ex.ToString();
            }
            catch (Exception ex)
            {
                status = ex.ToString();
            }

            return Json(new { status, Id });
        }


        [HttpPost]
        public IHttpActionResult checkLicenseCode(ACS.Core.Domain.Product.ProductLicense ProductLicense)
        {
           var LicenseId = _ProductLicenseService.CheckLicenseCode(ProductLicense);

           return Json(LicenseId);
        }


        [HttpPost]
        public IHttpActionResult InsertMultipleProductLinking(ACS.Core.Domain.Product.ProductLicense ProductLicense)
        {

            foreach (ProductLicenseAddendumLink AddendumLink in ProductLicense.ProductLicenseAddendumLink)
            {

                // Addendum Details Set
                ProductLicenseAddendumLink _ProductLicenseAddendumLink = new ProductLicenseAddendumLink();
                _ProductLicenseAddendumLink.ProductId = AddendumLink.ProductId;
                _ProductLicenseAddendumLink.LicenseId = AddendumLink.LicenseId;
                _ProductLicenseAddendumLink.Active = AddendumLink.Active;
                _ProductLicenseAddendumLink.Deactivate = "N";
                _ProductLicenseAddendumLink.EnteredBy = ProductLicense.EnteredBy;
                _ProductLicenseAddendumLink.EntryDate = DateTime.Now;
                _ProductLicenseAddendumLink.ModifiedBy = null;
                _ProductLicenseAddendumLink.ModifiedDate = null;
                _ProductLicenseAddendumLink.DeactivateBy = null;
                _ProductLicenseAddendumLink.DeactivateDate = null;
                _ProductLicenseService.InsertMultipleProductLink(_ProductLicenseAddendumLink);
            }

            return Json("OK");
        }

        [HttpPost]
        public IHttpActionResult getLicenseDetailsByLicenseId(ACS.Core.Domain.Product.ProductLicense ProductLicense)
        {
            ProductLicense _ProductLicense = _ProductLicenseService.GetProductLicenseById(ProductLicense);
            DateTime? ProductLicenseContractDate= _ProductLicense.ContractDate;
            DateTime? ProductLicenseExpirydate = _ProductLicense.Expirydate;
            string format = "dd/MM/yyyy";
            string ContractDate = Convert.ToDateTime(ProductLicenseContractDate).ToString(format);
            string ExpiryDate = Convert.ToDateTime(ProductLicenseExpirydate).ToString(format);
            var LicenseData = new
            {
                Id = _ProductLicense.Id,
                ProductId = _ProductLicense.productid,
                ProductLicensecode = _ProductLicense.ProductLicensecode,
                Company = _ProductLicense.LicensePublishing.CompanyName,
                ContactPerson = _ProductLicense.ContactPerson,
                RequestDate = _ProductLicense.Requestdate.Date.ToString("dd/MM/yyyy"),
                //ContractDate = _ProductLicense.ContractDate.Date.ToString("dd/MM/yyyy"),
                //ExpiryDate = _ProductLicense.Expirydate.Date.ToString("dd/MM/yyyy")
                ContractDate = ContractDate,
                ExpiryDate = ExpiryDate 
                
            };
            return Json(LicenseData);
            
        }


        public IHttpActionResult ImpressionSearch(ACS.Core.Domain.Product.ImpressionSearch ImpressionSearch)
        {
            SqlParameter[] parameters = new SqlParameter[4];

            try
            {
                if (ImpressionSearch != null)
                {
                    //parameters[0] = new SqlParameter("ProductLicesneCode", SqlDbType.VarChar, 50);
                    //if (ImpressionSearch.ProductLicesneCode == null)
                    //{
                    //    parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
                    //}
                    //else
                    //{
                    //    parameters[0].Value = "'" + ImpressionSearch.ProductLicesneCode + "'";
                    //}

                    //parameters[1] = new SqlParameter("LicenseAddendumCode", SqlDbType.VarChar, 50);
                    //if (ImpressionSearch.LicenseAddendumCode == null)
                    //{
                    //    parameters[1].Value = System.Data.SqlTypes.SqlInt32.Null;
                    //}
                    //else
                    //{
                    //    parameters[1].Value = "'" + ImpressionSearch.LicenseAddendumCode +"'" ;
                    //}

                    parameters[0] = new SqlParameter("ProductCode", SqlDbType.VarChar, 50);
                    if (ImpressionSearch.ProductCode == null)
                    {
                        parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[0].Value = "'" + ImpressionSearch.ProductCode + "'";
                    }

                    parameters[1] = new SqlParameter("FinalTitle", SqlDbType.VarChar, 50);
                    if (ImpressionSearch.FinalTitle == null)
                    {
                        parameters[1].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[1].Value = "'" + ImpressionSearch.FinalTitle + "'";
                    }

                    parameters[2] = new SqlParameter("OUPISBN", SqlDbType.VarChar, 50);
                    if (ImpressionSearch.OUPISBN == null)
                    {
                        parameters[2].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[2].Value = "'" + ImpressionSearch.OUPISBN +"'" ;
                    }

                    parameters[3] = new SqlParameter("ProductCategory", SqlDbType.VarChar, 50);
                    if (ImpressionSearch.ProductCategory == null)
                    {
                        parameters[3].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[3].Value = "'" + ImpressionSearch.ProductCategory +"'";
                    }

                }


            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "ProductLicenseController.cs", "ImpressionSearch", ex);
                return Json(ex.InnerException);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "ProductLicenseController.cs", "ImpressionSearch", ex);
                return Json(ex.InnerException);
            }


            if (ImpressionSearch.ProductCategory == "OR")
            {
                var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<ImpressionSearch.ORSearchResult>("Proc_ImpressionSerchReport_get", parameters).ToList();
                return Json(_GetAuthorReport);
            }
            else
            {
                var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<ImpressionSearch.NOSearchResult>("Proc_ImpressionSerchReport_get", parameters).ToList();
                return Json(_GetAuthorReport);
            }
           
        }

        public IHttpActionResult TopSearch(String Code)
        {

            ProductLicense ProductLicenseMaster = _ProductLicense.Table.Where(a => a.ProductLicensecode == Code && a.Deactivate == "N").FirstOrDefault();

            if (ProductLicenseMaster != null)
            {
                var _ProductLicenseMasterValue = new
                {
                    Id = ProductLicenseMaster.Id
                };

                return Json(new { _ProductLicenseMasterValue });
            }
            else
            {
                string _ProductLicenseMasterValue = string.Empty;
                return Json(new { _ProductLicenseMasterValue });
            }

        }
    }
}
