//create by saddam 
//date : 24/05/2016
///purpose : Insert, Update, Delete Records for Custom Product insert data in two table ProprietorMaster, ProprietorAuthorLink
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ACS.Core.Domain.Master;
using ACS.Services.Logging;
using ACS.Services.Localization;
using ACS.Services.Master;
using ACS.Core;
using SLV.Model.Common;
using Autofac.Integration.WebApi;
using SLV.API.Controllers.JsonSerializer;
using ACS.Core.Infrastructure;
using System.Web.Http.Description;
using ACS.Core.Data;
using ACS.Data;
using System.Data;
using System.Data.SqlClient;
using ACS.Core.Domain.Master;
using ACS.Core.Domain.Product;
using ACS.Services.Product;
namespace SLV.API.Controllers.CustomProduct
{
    

    public class CustomProductController : ApiController
    {

        private readonly IProductMasterService _ProductMasterService;
        private readonly ICustomProductService _CustomProductService;
        private readonly ICommonListService _CommonListService;
        private readonly ILocalizationService _localizationService;
        private readonly IDbContext _dbContext;

        public CustomProductController(

               IProductMasterService ProductMasterService
           ,  ICustomProductService CustomProductService
          , ILocalizationService localizationService
            , ICommonListService CommonListService
                , IDbContext dbContext

            )
        {
            _ProductMasterService = ProductMasterService;
            _localizationService = localizationService;
            _CustomProductService = CustomProductService;
            _CommonListService = CommonListService;
            this._dbContext = dbContext;
        }

        ACS.Core.Domain.Master.CustomProduct _CustomProduct;

        ACS.Core.Domain.Product.ProprietorMaster mobj_proprietorMaster = new ProprietorMaster();

                 
        IList<ProprietorAuthorLink> _IProprietorAuthorLink = new List<ProprietorAuthorLink>();
        ProprietorAuthorLink _ProprietorAuthorLink = new ProprietorAuthorLink();

        [HttpGet]
        public IHttpActionResult getCustomProductList()
        {
            var query = _CustomProductService.GetAllCustomproduct().ToList().Select(i => new

            {

                Id = i.Id,
                ProprietorISBN = i.ProprietorISBN,
                ProprietorProduct = i.ProprietorProduct,
                ProprietorEdition = i.ProprietorEdition,
                ProprietorCopyrightYear = i.ProprietorCopyrightYear
            });

            return Json(query);
        }

        //Added by Saddam/
        public IHttpActionResult InsertProprietor(ACS.Core.Domain.Master.CustomProduct  Proprietor)
        {

            string status = "";
            try
            {
                mobj_proprietorMaster = new ProprietorMaster();

                mobj_proprietorMaster.Id = Proprietor.Id;
                mobj_proprietorMaster.ProprietorISBN = Proprietor.ProprietorISBN;

               status = _CustomProductService.DuplicityCheck(mobj_proprietorMaster);
                if (status == "Y")
                {
                    if (Proprietor.Id == 0)
                    {
                     
                        List<string> Author = new List<string>();
                        if (Proprietor.AuthorId != null && Proprietor.AuthorId != "0")
                        {
                            Author.AddRange(Proprietor.AuthorId.Split(',').Where(x => string.IsNullOrEmpty(x) == false).Distinct().ToList());
                        }
                       
                        foreach (var item in Author)
                        {
                            _ProprietorAuthorLink = new ProprietorAuthorLink();
                            _ProprietorAuthorLink.AuthorId = Convert.ToInt32(item);
                            _ProprietorAuthorLink.EntryDate = DateTime.Now;
                           _ProprietorAuthorLink.EnteredBy = Proprietor.EnteredBy;
                            _ProprietorAuthorLink.Deactivate = "N";
                            _ProprietorAuthorLink.ModifiedBy = null;
                            _ProprietorAuthorLink.ModifiedDate = null;
                            _ProprietorAuthorLink.DeactivateBy = null;
                            _ProprietorAuthorLink.DeactivateDate = null;
                            _IProprietorAuthorLink.Add(_ProprietorAuthorLink);

                        }
                       mobj_proprietorMaster.ProprietorAuthorLink = _IProprietorAuthorLink;

                        mobj_proprietorMaster.ProductId = Proprietor.ProductId;
                        mobj_proprietorMaster.ProprietorISBN = Proprietor.ProprietorISBN;

                        mobj_proprietorMaster.ProprietorProduct = Proprietor.ProprietorProduct;
                        mobj_proprietorMaster.ProprietorEdition = Proprietor.ProprietorEdition;

                        mobj_proprietorMaster.ProprietorCopyrightYear = Proprietor.ProprietorCopyrightYear;
                        mobj_proprietorMaster.PublishingCompanyId = Proprietor.PublishingCompanyId;

                        mobj_proprietorMaster.ProprietorPubCenterId = Proprietor.ProprietorPubCenterId;
                        mobj_proprietorMaster.ProprietorImPrintId = Proprietor.ProprietorImPrintId;

                        mobj_proprietorMaster.Deactivate = "N";
                        mobj_proprietorMaster.EnteredBy = Proprietor.EnteredBy;
                        mobj_proprietorMaster.EntryDate = DateTime.Now;
                        mobj_proprietorMaster.ModifiedBy = null;
                        mobj_proprietorMaster.ModifiedDate = null;
                        mobj_proprietorMaster.DeactivateBy = null;
                        mobj_proprietorMaster.DeactivateDate = null;
                        mobj_proprietorMaster.Main = "N";

                        _CustomProductService.insertcustomproduct(mobj_proprietorMaster);

                    }
                   // else
                    //{


                    //   // ACS.Core.Domain.Product.ProductMaster mobj_productMaster = _CustomProductService.getcustomproductbyid(Proprietor.Id);

                    ////    ACS.Core.Domain.Product.ProductMaster mobj_proprietorMaster = _CustomProductService.getcustomproductbyid(Proprietor.Id);

                    //  mobj_proprietorMaster = new  ProprietorMaster();
                    //    mobj_proprietorMaster.Id = Proprietor.Id;
                    //    mobj_proprietorMaster = _CustomProductService.getcustomproductbyid(mobj_proprietorMaster);

                    //    mobj_proprietorMaster.ProprietorAuthorLink
                    //        .Where(x => x.Deactivate == "N" && x.ProprietorId == mobj_proprietorMaster.Id)
                    //        .ToList()
                    //        .ForEach(x =>
                    //        {
                    //            x.Deactivate = "Y";
                    //            x.DeactivateBy = Proprietor.EnteredBy;
                    //            x.DeactivateDate = DateTime.Now;
                    //        });

                    //    _CustomProductService.updatecustomproduct(mobj_proprietorMaster);
                       

                    //    List<string> Author = new List<string>();
                    //    if (Proprietor.AuthorId != null && Proprietor.AuthorId != "0")
                    //    {
                    //        Author.AddRange(Proprietor.AuthorId.Split(',').Where(x => string.IsNullOrEmpty(x) == false).Distinct().ToList());
                    //    }
                    //    mobj_proprietorMaster.ProductId = Proprietor.ProductId;
                    //    mobj_proprietorMaster.ProprietorISBN = Proprietor.ProprietorISBN;

                    //    mobj_proprietorMaster.ProprietorProduct = Proprietor.ProprietorProduct;
                    //    mobj_proprietorMaster.ProprietorEdition = Proprietor.ProprietorEdition;

                    //    mobj_proprietorMaster.ProprietorCopyrightYear = Proprietor.ProprietorCopyrightYear;
                    //    mobj_proprietorMaster.PublishingCompanyId = Proprietor.PublishingCompanyId;

                    //    mobj_proprietorMaster.ProprietorPubCenterId = Proprietor.ProprietorPubCenterId;
                    //    mobj_proprietorMaster.ProprietorImPrintId = Proprietor.ProprietorImPrintId;

                    //    mobj_proprietorMaster.ModifiedBy = Proprietor.EnteredBy;
                    //    mobj_proprietorMaster.ModifiedDate = DateTime.Now;
             
                    //    foreach (var item in Author)
                    //    {
                    //        //_ProprietorAuthorLink = new ProprietorAuthorLink();
                    //        //_ProprietorAuthorLink.AuthorId = Convert.ToInt32(item);
                    //        //_ProprietorAuthorLink.ProprietorId = mobj_proprietorMaster.Id;

                    //        //_ProprietorAuthorLink.ModifiedBy = Proprietor.EnteredBy;
                    //        //_ProprietorAuthorLink.ModifiedDate = DateTime.Now;
                    //        //_IProprietorAuthorLink.Add(_ProprietorAuthorLink);

                    //        _ProprietorAuthorLink = new ProprietorAuthorLink();
                    //        _ProprietorAuthorLink.AuthorId = Convert.ToInt32(item);
                    //      _ProprietorAuthorLink.ProprietorId = mobj_proprietorMaster.Id;
                    //        _ProprietorAuthorLink.EntryDate = DateTime.Now;
                    //        _ProprietorAuthorLink.EnteredBy = Proprietor.EnteredBy;
                    //        _ProprietorAuthorLink.Deactivate = "N";
                    //        _ProprietorAuthorLink.ModifiedBy = null;
                    //        _ProprietorAuthorLink.ModifiedDate = null;
                    //        _ProprietorAuthorLink.DeactivateBy = null;
                    //        _ProprietorAuthorLink.DeactivateDate = null;
                    //        _IProprietorAuthorLink.Add(_ProprietorAuthorLink);
                    //    }

                    //    mobj_proprietorMaster.ProprietorAuthorLink = _IProprietorAuthorLink;

                    //  //  mobj_proprietorMaster.ProprietorAuthorLink = mobj_proprietorMaster.ProprietorAuthorLink;


                    //    _CustomProductService.updatecustomproduct(mobj_proprietorMaster);

                    //}
                    status = _localizationService.GetResource("Master.API.Success.Message");
                }
                else
                {
                    status = "Duplicate";
                }



            }
            catch (ACSException ex)
            {
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                status = ex.InnerException.Message;
            }

            return Json(status);
        }



        public IHttpActionResult UpdateProprietor(ProprietorMaster  Proprietor)
        {

            string status = "";
            try
            {

                status = _CustomProductService.DuplicityCheck(Proprietor);
                if (status == "Y")
                {
                    if (Proprietor.Id > 0)
                    {
                          ProprietorMaster mobj_proprietorMaster = _CustomProductService.getcustomproductbyid(Proprietor);
                         

                        List<ProprietorAuthorLink> _IProductOUPAuthor = mobj_proprietorMaster.ProprietorAuthorLink.ToList();


                        foreach (ProprietorAuthorLink prlink in _IProductOUPAuthor)
                        {
                            _ProductMasterService.DeleteProprietorAuthorLink(prlink);
                        }

                        ProprietorMaster propM = _ProprietorAuthorLink.ProprietorMaster;



                        foreach (ProprietorAuthorLink PropAuthor in Proprietor.ProprietorAuthorLink)
                        {
                      //    ProprietorAuthorLink _ProprietorAuthor = new ProprietorAuthorLink();

                            //_ProprietorAuthor.AuthorId = PropAuthor.AuthorId;

                            //_ProprietorAuthor.Deactivate = "N";
                            //_ProprietorAuthor.EnteredBy = Proprietor.EnteredBy;
                            //_ProprietorAuthor.EntryDate = DateTime.Now;
                            //_ProprietorAuthor.ModifiedBy = null;
                            //_ProprietorAuthor.ModifiedDate = null;
                            //_ProprietorAuthor.DeactivateBy = null;
                            //_ProprietorAuthor.DeactivateDate = null;
                            //_IProprietorAuthorLink.Add(_ProprietorAuthorLink);

                          _ProprietorAuthorLink = new ProprietorAuthorLink();
                          _ProprietorAuthorLink.AuthorId = PropAuthor.AuthorId;
                          _ProprietorAuthorLink.ProprietorId = mobj_proprietorMaster.Id;
                          _ProprietorAuthorLink.EntryDate = DateTime.Now;
                          _ProprietorAuthorLink.EnteredBy = Proprietor.EnteredBy;
                          _ProprietorAuthorLink.Deactivate = "N";
                          _ProprietorAuthorLink.ModifiedBy = null;
                          _ProprietorAuthorLink.ModifiedDate = null;
                          _ProprietorAuthorLink.DeactivateBy = null;
                          _ProprietorAuthorLink.DeactivateDate = null;
                          _IProprietorAuthorLink.Add(_ProprietorAuthorLink);
                        }



                        mobj_proprietorMaster.ProprietorAuthorLink = _IProprietorAuthorLink;

                      ///  mobj_proprietorMaster.ProprietorAuthorLink = mobj_proprietorMaster.ProprietorAuthorLink;

                        mobj_proprietorMaster.ProductId = Proprietor.ProductId;
                        mobj_proprietorMaster.ProprietorISBN = Proprietor.ProprietorISBN;

                        mobj_proprietorMaster.ProprietorProduct = Proprietor.ProprietorProduct;
                        mobj_proprietorMaster.ProprietorEdition = Proprietor.ProprietorEdition;

                        mobj_proprietorMaster.ProprietorCopyrightYear = Proprietor.ProprietorCopyrightYear;
                        mobj_proprietorMaster.PublishingCompanyId = Proprietor.PublishingCompanyId;

                        mobj_proprietorMaster.ProprietorPubCenterId = Proprietor.ProprietorPubCenterId;
                        mobj_proprietorMaster.ProprietorImPrintId = Proprietor.ProprietorImPrintId;

                        mobj_proprietorMaster.ModifiedBy = Proprietor.EnteredBy;
                        mobj_proprietorMaster.ModifiedDate = DateTime.Now;

                        _CustomProductService.updatecustomproduct(mobj_proprietorMaster);

                    }
                   
                    
                    status = _localizationService.GetResource("Master.API.Success.Message");
                }
                else
                {
                    status = "Duplicate";
                }



            }
            catch (ACSException ex)
            {
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                status = ex.InnerException.Message;
            }

            return Json(status);
        }


        public IHttpActionResult WebGetProprietorById(ACS.Core.Domain.Master.CustomProduct Proprietor)
        {
         
            mobj_proprietorMaster = new ProprietorMaster();

            mobj_proprietorMaster.Id = Proprietor.Id;
           
            mobj_proprietorMaster = _CustomProductService.getcustomproductbyid(mobj_proprietorMaster);

            Proprietor.Id = mobj_proprietorMaster.Id;

            Proprietor.ProprietorISBN = mobj_proprietorMaster.ProprietorISBN;

            Proprietor.ProprietorProduct = mobj_proprietorMaster.ProprietorProduct;
            Proprietor.ProprietorEdition = mobj_proprietorMaster.ProprietorEdition;
            Proprietor.ProprietorCopyrightYear = mobj_proprietorMaster.ProprietorCopyrightYear;



            Proprietor.PublishingCompanyId = mobj_proprietorMaster.PublishingCompanyId;
            Proprietor.ProprietorPubCenterId = mobj_proprietorMaster.ProprietorPubCenterId;
            Proprietor.ProprietorImPrintId = mobj_proprietorMaster.ProprietorImPrintId;

            Proprietor.AuthorId = mobj_proprietorMaster.ProprietorAuthorLink 
                                    .Where(x => x.Deactivate == "N")
                                    .Select(x => x.AuthorId ).FirstOrDefault().ToString();

            Proprietor.AuthorIdList = mobj_proprietorMaster.ProprietorAuthorLink
                            .Where(x => x.Deactivate == "N")
                             .AsEnumerable()
                             .Select(x => x.AuthorId.ToString())
                             .ToArray();

            Proprietor.AuthorId = string.Join(",", Proprietor.AuthorIdList);

          
       //     return Json(Proprietor);

            IList<ProprietorAuthorLink> _ProprietorAuthorList = mobj_proprietorMaster.ProprietorAuthorLink.ToList();
            var ProprietorAuthor = _ProprietorAuthorList.Select(pu => new
            {
                AuthorId = pu.AuthorId,
                AuthorName = pu.ProprietorAuthorLinkAuthor.FirstName + " " + pu.ProprietorAuthorLinkAuthor.LastName,

            });
            return Json(SerializeObj.SerializeObject(new { Proprietor, ProprietorAuthor }));

         
        }
        public IHttpActionResult CustomDelete(ProprietorMaster _Proprietor)
        {

            string status = string.Empty;
            try
            {
                mobj_proprietorMaster = _CustomProductService.getcustomproductbyid(_Proprietor);

                mobj_proprietorMaster.DeactivateBy  = _Proprietor.EnteredBy;
                mobj_proprietorMaster.DeactivateDate = null;
                mobj_proprietorMaster.Deactivate = "Y";


                mobj_proprietorMaster.ProprietorAuthorLink 
                            .Where(x => x.Deactivate == "N")
                            .ToList()
                            .ForEach(x =>
                            {
                                x.Deactivate = "Y";
                                x.DeactivateBy = _Proprietor.EnteredBy;
                                x.DeactivateDate = DateTime.Now;
                            });

                _CustomProductService.updatecustomproduct(mobj_proprietorMaster);
                status = "OK";


            }
            catch (ACSException ex)
            {
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                status = ex.InnerException.Message;
            }

            return Json(status);
        }


    }
}