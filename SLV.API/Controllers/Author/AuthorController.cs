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
using ACS.Core.Domain.Product;
using SLV.Model.Common;
using ACS.Services.User;

using SLV.Model.AuthorContract;
using ACS.Services.AuthorContract;
using System.Web;
using ACS.Services.Product;
using System.Text;


namespace SLV.API.Controllers.Master
{
    public class AuthorController : ApiController
    {
        private readonly IAuthorService _AuthorTypeService;
        private readonly ILogger _loggerService;

        private readonly IApplicationSetUpService _ApplicationSetUpService;

        private readonly ICommonListService _CommonListService;
        private readonly IProductMasterService _IProductMasterService;
        private readonly ILocalizationService _localizationService;
        private readonly IDbContext _dbContext;
        private readonly IRepository<AuthorMaster> _AuthorRepository;
        private readonly IRepository<ApplicationSetUp> _ApplicationSetUp;

        private readonly IRepository<AuhtorDocument> _AuhtorDocument;

        private readonly IRepository<AuthorDepartment> _AuthorDepartment;


        //Apply for Author Contract only and replace 

        private readonly IAuthorContractService _IAuthorContractService;
        private readonly IRepository<NomineeAuthorDocumentMaster> _NomineeAuthorDocumentMaster;

        //
        public AuthorController(

             IAuthorService AuthorService
          , ILocalizationService localizationService
            , ICommonListService CommonListService
                , IDbContext dbContext
            , IRepository<AuthorMaster> AuthorRepository
            , IRepository<ApplicationSetUp> ApplicationSetUp
            , IApplicationSetUpService ApplicationSetUpService
            , IRepository<AuhtorDocument> AuhtorDocument
            , IRepository<AuthorDepartment> AuthorDepartment
            , IAuthorContractService IAuthorContractService
            , ILogger Ilogger,
            IProductMasterService IProductMasterService
            ,IRepository<NomineeAuthorDocumentMaster> NomineeAuthorDocumentMaster
            )
        {
            _AuthorRepository = AuthorRepository;
            _localizationService = localizationService;
            _AuthorTypeService = AuthorService;
            _CommonListService = CommonListService;
            _ApplicationSetUp = ApplicationSetUp;
            _ApplicationSetUpService = ApplicationSetUpService;
            _AuhtorDocument = AuhtorDocument;
            _AuthorDepartment = AuthorDepartment;
            this._dbContext = dbContext;
            this._IAuthorContractService = IAuthorContractService;
            this._loggerService = Ilogger;
            this._IProductMasterService = IProductMasterService;
            _NomineeAuthorDocumentMaster = NomineeAuthorDocumentMaster;
        }



        //[HttpGet]
        //public IHttpActionResult getAuthorList()
        //{
        //    return Json(_AuthorTypeService.GetAllAuthor().ToList());
        //}

        //Added by Saddam/
        public IHttpActionResult InsertAuthor(AuthorModel Author)
        {

            string status = "";
            string StatusType = "";
            string Author_CodeValue = string.Empty;
            int AuthorIdId = 0;
         
            try
            {
                //------------start Capital First Letter
                string AuthorFirstName = "";
                string AuthorLastName = "";
                StringBuilder sb;
                
                if (Author.FirstName != null)
                {
                    string str_firstname = Author.FirstName.Trim();
                    sb = new StringBuilder(str_firstname.Length);
                    bool capitalize = true;
                    foreach (char c in str_firstname)
                    {
                        sb.Append(capitalize ? Char.ToUpper(c) : Char.ToLower(c));
                        //capitalize = !Char.IsLetter(c);
                        capitalize = Char.IsWhiteSpace(c);
                    }
                    AuthorFirstName = sb.ToString().Trim();
                }

                if (Author.LastName != null)
                {
                    string str_lastname = Author.LastName.Trim();
                    sb = new StringBuilder(str_lastname.Length);
                    bool capitalize = true;
                    foreach (char c in str_lastname)
                    {
                        sb.Append(capitalize ? Char.ToUpper(c) : Char.ToLower(c));
                        //capitalize = !Char.IsLetter(c);
                        capitalize = Char.IsWhiteSpace(c);
                    }
                    AuthorLastName = sb.ToString().Trim();
                
                }
                //-----------------------end 


                //  status = _AuthorTypeService.DuplicityCheck(Author);

                if (Author.ResidencyStatus.ToLower() == "resident")
                {

                   // var Duplicate = _AuthorRepository.Table.Where(x => x.PANNo == Author.PANNo   &&  (Author.Id !=0 ? x.Id : 0 ) && x.Deactivate == "N"  ).FirstOrDefault();

                   
                   
                        var Duplicate = _AuthorRepository.Table.Where(x => x.PANNo == Author.PANNo
                                                                       && x.Deactivate == "N"
                                                                       && (Author.Id != 0 ? x.Id : 0) != (Author.Id != 0 ? Author.Id : 1)).FirstOrDefault();


                        if (Duplicate != null)
                        {
                            status = "N";
                            StatusType = "PAN";

                        }
                        else
                        {
                             if (Author.AccountNo != null && Author.AccountNo !="")
                    {
                        var DuplicateAccount = _AuthorRepository.Table.Where(x => x.AccountNo == Author.AccountNo
                                                                   && x.Deactivate == "N"
                                                                   && (Author.Id != 0 ? x.Id : 0) != (Author.Id != 0 ? Author.Id : 1)).FirstOrDefault();



                        if (DuplicateAccount != null)
                        {
                            status = "N";
                            StatusType = "AccountNo";

                        }
                        else
                        {

                            status = "Y";
                        }



                    }
                             else
                             {
                               status = "Y";
                             }
                          
                        }
                    
                

                    

                }
                else if (Author.ResidencyStatus.ToLower() == "non-resident")
                {


                    ///var Duplicate = _AuthorRepository.Table.Where(x => x.AccountNo == Author.AccountNo  && x.Deactivate == "N").FirstOrDefault();

                        var Duplicate = _AuthorRepository.Table.Where(x => x.AccountNo == Author.AccountNo
                                                                    && x.Deactivate == "N"
                                                                    && (Author.Id != 0 ? x.Id : 0) != (Author.Id != 0 ? Author.Id : 1)).FirstOrDefault();


                        if (Duplicate != null)
                        {
                            status = "N";
                            StatusType = "AccountNo";
                        }
                        else
                        {
                            if (Author.PANNo != null && Author.PANNo != "")
                            {
                                var DuplicatePANNo = _AuthorRepository.Table.Where(x => x.PANNo == Author.PANNo
                                                                            && x.Deactivate == "N"
                                                                            && (Author.Id != 0 ? x.Id : 0) != (Author.Id != 0 ? Author.Id : 1)).FirstOrDefault();




                                if (DuplicatePANNo != null)
                                {
                                    status = "N";
                                    StatusType = "PAN";
                                }
                                else
                                {

                                    status = "Y";
                                }
                            }
                            else
                            {
                                status = "Y";
                            }
                           
                        }
                  


                   

                  
                    
                }



                if (status == "Y")
                {
                    if (Author.Id == 0)
                    {

                        IList<ApplicationSetUp> _ApplicationSetUpList = _ApplicationSetUp.Table.Where(x => x.key == "AuthorCode" && x.Deactivate == "N").ToList();
                        var AuthorSuggesation = _ApplicationSetUpList.Select(Au => new
                        {
                            AuthorCodeValue = Au.keyValue,
                            Id = Au.Id
                        });

                        // Author.AuthorCode = "AU" + Author.LastName.Substring(0, 1).ToString() + AuthorSuggesation.FirstOrDefault().AuthorCodeValue;

                        AuthorMaster _Author = new AuthorMaster();

                        string AuthorName = string.Empty;
                        if (Author.LastName != null)
                        {
                            AuthorName = Author.LastName.Substring(0, 1);
                        }
                        else
                        {
                            AuthorName = Author.FirstName.Substring(0, 1);
                        }


                        _Author.AccountNo = Author.AccountNo;
                        _Author.Address = Author.Address;
                        _Author.AdharCardNo = Author.AdharCardNo;
                        _Author.AffiliationAddress = Author.AffiliationAddress;
                        //_Author.AffiliationCityId = Author.AffiliationCityId;
                        _Author.AffiliationCityId = Author.AffiliationCityId;
                        _Author.AffiliationCountryId = Author.AffiliationCountryId;
                        _Author.AffiliationDepartment = Author.AffiliationDepartment;
                        _Author.AffiliationDesignation = Author.AffiliationDesignation;
                        _Author.AffiliationEmail = Author.AffiliationEmail;
                        _Author.AffiliationOtherCity = Author.AffiliationOtherCity;
                        _Author.AffiliationOtherCountry = Author.AffiliationOtherCountry;
                        _Author.AffiliationOtherState = Author.AffiliationOtherState;
                        _Author.AffiliationPhone = Author.AffiliationPhone;
                        _Author.AffiliationPinCode = Author.AffiliationPinCode;
                        _Author.AffiliationStateId = Author.AffiliationStateId;
                        _Author.AffiliationWebSite = Author.AffiliationWebSite;
                              


                     //   _Author.AuthorCode = "AU" + Author.LastName.Substring(0, 1).ToString() + AuthorSuggesation.FirstOrDefault().AuthorCodeValue;

                        _Author.AuthorCode = "AU" + AuthorName + AuthorSuggesation.FirstOrDefault().AuthorCodeValue;

                        _Author.AuthorCode = _Author.AuthorCode.ToString().ToUpper();
                        Author_CodeValue = _Author.AuthorCode.ToString().ToUpper();

                        _Author.BankName = Author.BankName;
                        _Author.BeneficiaryAccountNo = Author.BeneficiaryAccountNo;
                        _Author.BeneficiaryAddress = Author.BeneficiaryAddress;
                        _Author.BeneficiaryBankName = Author.BeneficiaryBankName;
                        _Author.BeneficiaryBranchName = Author.BeneficiaryBranchName;
                        _Author.BeneficiaryCityId = Author.BeneficiaryCityId;
                        _Author.BeneficiaryCountryId = Author.BeneficiaryCountryId;
                        _Author.BeneficiaryEmail = Author.BeneficiaryEmail;
                        _Author.BeneficiaryFax = Author.BeneficiaryFax;
                        _Author.BeneficiaryIFSECode = Author.BeneficiaryIFSECode;
                        _Author.BeneficiaryMobile = Author.BeneficiaryMobile;
                        _Author.BeneficiaryName = Author.BeneficiaryName;
                        _Author.BeneficiaryOtherCity = Author.BeneficiaryOtherCity;
                        _Author.BeneficiaryOtherCountry = Author.BeneficiaryOtherCountry;
                        _Author.BeneficiaryOtherState = Author.BeneficiaryOtherState;
                        _Author.BeneficiaryPanNo = Author.BeneficiaryPanNo;
                        _Author.BeneficiaryPhone = Author.BeneficiaryPhone;
                        _Author.BeneficiaryPinCode = Author.BeneficiaryPinCode;
                        _Author.BeneficiaryRelation = Author.BeneficiaryRelation;
                        _Author.BeneficiaryStateId = Author.BeneficiaryStateId;
                        _Author.BranchName = Author.BranchName;
                        _Author.CityId = Author.CityId;
                        _Author.CountryId = Author.CountryId;
                        _Author.DateOfBirth = Author.DateOfBirth;
                        _Author.Deactivate = "N";
                        _Author.DeactivateBy = null;
                        _Author.DeactivateDate = null;
                        _Author.DeathDate = Author.DeathDate;
                        _Author.Email = Author.Email;
                        _Author.EnteredBy = Author.EnteredBy;
                        _Author.EntryDate = DateTime.Now;
                        _Author.Fax = Author.Fax;
                        _Author.FirstName = AuthorFirstName; //Author.FirstName;
                        _Author.IFSECode = Author.IFSECode;
                        _Author.InstituteCompanyName = Author.InstituteCompanyName;
                        _Author.LastName = AuthorLastName; //Author.LastName;
                        _Author.Mobile = Author.Mobile;
                        _Author.ModifiedBy = null;
                        _Author.ModifiedDate = null;
                        _Author.NomineeAddress = Author.NomineeAddress;
                        _Author.NomineeCityId = Author.NomineeCityId;
                        _Author.NomineeCountryId = Author.NomineeCountryId;
                        _Author.NomineeEmail = Author.NomineeEmail;
                        _Author.NomineeFax = Author.NomineeFax;
                        _Author.NomineeMobile = Author.NomineeMobile;
                        _Author.NomineeName = Author.NomineeName;
                        _Author.NomineeOtherCity = Author.NomineeOtherCity;
                        _Author.NomineeOtherCountry = Author.NomineeOtherCountry;
                        _Author.NomineeOtherState = Author.NomineeOtherState;
                        _Author.NomineePanNo = Author.NomineePanNo;
                        _Author.NomineePhone = Author.NomineePhone;
                        _Author.NomineePinCode = Author.NomineePinCode;
                        _Author.NomineeRelation = Author.NomineeRelation;
                        _Author.NomineeStateId = Author.NomineeStateId;
                        _Author.OtherCity = Author.OtherCity;
                        _Author.OtherCountry = Author.OtherCountry;
                        _Author.OtherState = Author.OtherState;
                        _Author.PANNo = Author.PANNo;
                        _Author.Phone = Author.Phone;
                        _Author.PinCode = Author.PinCode;
                        _Author.ResidencyStatus = Author.ResidencyStatus;
                        _Author.StateId = Author.StateId;
                        _Author.Type = Author.Type;
                        _Author.Remark = Author.Remark;

                        _Author.AuthorSAPCode = Author.AuthorSAPCode;

                        _Author.NomineeAccountNo = Author.NomineeAccountNo;
                        _Author.NomineeBranchName = Author.NomineeBranchName;
                        _Author.NomineeBankName = Author.NomineeBankName;
                        _Author.NomineeIFSECode = Author.NomineeIFSECode;
                        
                        AuthorIdId = _AuthorTypeService.InsertAuthor(_Author);

                        if (AuthorIdId != 0)
                        {
                            AuhtorDocument _AuhtorDoc = new AuhtorDocument();

                            string[] docurl = Author.UploadFile.Split(',');
                            int i = 0;
                            foreach (string doc in Author.DocumentName)
                            {
                                AuhtorDocument Link = new AuhtorDocument();
                                Link.AuhtorId = AuthorIdId;
                                Link.DocumentName = doc;
                                Link.UploadFile = docurl[i];
                                Link.EnteredBy = Author.EnteredBy;
                                _AuthorTypeService.InsertAuthorDocumentLinking(Link);
                                i++;
                            }


                            NomineeAuthorDocumentMaster _NomineeAuhtorDoc = new NomineeAuthorDocumentMaster();

                            string[] docurlNominee = Author.NomineeUploadFile.Split(',');
                            int j = 0;
                            foreach (string doc in Author.NomineeDocumentName)
                            {
                                NomineeAuthorDocumentMaster Link = new NomineeAuthorDocumentMaster();
                                Link.AuhtorId = AuthorIdId;
                                Link.DocumentName = doc;
                                Link.UploadFile = docurlNominee[j];
                                Link.EnteredBy = Author.EnteredBy;
                                _AuthorTypeService.InsertNomineeAuthorDocumentLinking(Link);
                               
                                j++;
                            }



                        }





                        ApplicationSetUp Mobj_ApplicationSetUp = new ApplicationSetUp();

                        Mobj_ApplicationSetUp.Id = AuthorSuggesation.FirstOrDefault().Id;

                        ApplicationSetUp _ApplicationSetUpUpdate = _ApplicationSetUpService.GetApplicationSetUpById(Mobj_ApplicationSetUp);

                        _ApplicationSetUpUpdate.Id = AuthorSuggesation.FirstOrDefault().Id;
                        int Value = Int32.Parse(AuthorSuggesation.FirstOrDefault().AuthorCodeValue) + 1;

                        _ApplicationSetUpUpdate.keyValue = Value.ToString().PadLeft(4, '0');

                        _ApplicationSetUpUpdate.ModifiedBy = Author.EnteredBy;
                        _ApplicationSetUpUpdate.ModifiedDate = DateTime.Now;

                        _ApplicationSetUpService.UpdateApplication(_ApplicationSetUpUpdate);
                    }
                    else
                    {

                        AuthorMaster mobj_AuhtorMaster = new AuthorMaster();
                        mobj_AuhtorMaster.Id = Author.Id;

                        AuthorMaster mobj_Author = _AuthorTypeService.GetAuthorById(mobj_AuhtorMaster);


                        mobj_Author.Type = Author.Type;
                        mobj_Author.FirstName = AuthorFirstName; // Author.FirstName;
                        mobj_Author.LastName = AuthorLastName; // Author.LastName;
                        mobj_Author.Address = Author.Address;
                        mobj_Author.ResidencyStatus = Author.ResidencyStatus;
                        mobj_Author.CountryId = Author.CountryId;
                        mobj_Author.OtherCountry = Author.OtherCountry;

                        mobj_Author.StateId = Author.StateId;
                        mobj_Author.OtherState = Author.OtherState;
                        mobj_Author.CityId = Author.CityId;
                        mobj_Author.OtherCity = Author.OtherCity;
                        mobj_Author.PinCode = Author.PinCode;
                        mobj_Author.Email = Author.Email;
                        mobj_Author.Phone = Author.Phone;
                        mobj_Author.Mobile = Author.Mobile;


                        mobj_Author.Fax = Author.Fax;
                        mobj_Author.PANNo = Author.PANNo;
                        mobj_Author.AdharCardNo = Author.AdharCardNo;
                        mobj_Author.DateOfBirth = Author.DateOfBirth;
                        mobj_Author.DeathDate = Author.DeathDate;
                        mobj_Author.AccountNo = Author.AccountNo;
                        mobj_Author.BankName = Author.BankName;
                        mobj_Author.BranchName = Author.BranchName;



                        mobj_Author.IFSECode = Author.IFSECode;
                        mobj_Author.InstituteCompanyName = Author.InstituteCompanyName;
                        mobj_Author.AffiliationDesignation = Author.AffiliationDesignation;
                        mobj_Author.AffiliationDepartment = Author.AffiliationDepartment;
                        mobj_Author.AffiliationAddress = Author.AffiliationAddress;
                        mobj_Author.AffiliationCountryId = Author.AffiliationCountryId;
                        mobj_Author.AffiliationOtherCountry = Author.AffiliationOtherCountry;
                        mobj_Author.AffiliationStateId = Author.AffiliationStateId;

                        mobj_Author.AuthorSAPCode = Author.AuthorSAPCode;

                        mobj_Author.AffiliationOtherState = Author.AffiliationOtherState;
                        mobj_Author.AffiliationCityId = Author.AffiliationCityId;
                        mobj_Author.AffiliationOtherCity = Author.AffiliationOtherCity;
                        mobj_Author.AffiliationPinCode = Author.AffiliationPinCode;
                        mobj_Author.AffiliationPhone = Author.AffiliationPhone;
                        mobj_Author.AffiliationEmail = Author.AffiliationEmail;
                        mobj_Author.AffiliationWebSite = Author.AffiliationWebSite;
                        mobj_Author.BeneficiaryName = Author.BeneficiaryName;


                        mobj_Author.BeneficiaryRelation = Author.BeneficiaryRelation;
                        mobj_Author.BeneficiaryAddress = Author.BeneficiaryAddress;
                        mobj_Author.BeneficiaryCountryId = Author.BeneficiaryCountryId;
                        mobj_Author.BeneficiaryOtherCountry = Author.BeneficiaryOtherCountry;
                        mobj_Author.BeneficiaryStateId = Author.BeneficiaryStateId;
                        mobj_Author.BeneficiaryOtherState = Author.BeneficiaryOtherState;
                        mobj_Author.BeneficiaryCityId = Author.BeneficiaryCityId;
                        mobj_Author.BeneficiaryOtherCity = Author.BeneficiaryOtherCity;

                        mobj_Author.BeneficiaryPinCode = Author.BeneficiaryPinCode;
                        mobj_Author.BeneficiaryEmail = Author.BeneficiaryEmail;
                        mobj_Author.BeneficiaryPhone = Author.BeneficiaryPhone;
                        mobj_Author.BeneficiaryMobile = Author.BeneficiaryMobile;
                        mobj_Author.BeneficiaryFax = Author.BeneficiaryFax;
                        mobj_Author.BeneficiaryPanNo = Author.BeneficiaryPanNo;
                        mobj_Author.BeneficiaryAccountNo = Author.BeneficiaryAccountNo;
                        mobj_Author.BeneficiaryBankName = Author.BeneficiaryBankName;


                        mobj_Author.BeneficiaryBranchName = Author.BeneficiaryBranchName;
                        mobj_Author.BeneficiaryIFSECode = Author.BeneficiaryIFSECode;
                        mobj_Author.NomineeName = Author.NomineeName;
                        mobj_Author.NomineeRelation = Author.NomineeRelation;
                        mobj_Author.NomineeAddress = Author.NomineeAddress;
                        mobj_Author.NomineeCountryId = Author.NomineeCountryId;
                        mobj_Author.NomineeOtherCountry = Author.NomineeOtherCountry;
                        mobj_Author.NomineeStateId = Author.NomineeStateId;



                        mobj_Author.NomineeOtherState = Author.NomineeOtherState;
                        mobj_Author.NomineeCityId = Author.NomineeCityId;
                        mobj_Author.NomineeOtherCity = Author.NomineeOtherCity;
                        mobj_Author.NomineePinCode = Author.NomineePinCode;
                        mobj_Author.NomineeEmail = Author.NomineeEmail;
                        mobj_Author.NomineePhone = Author.NomineePhone;
                        mobj_Author.NomineeMobile = Author.NomineeMobile;
                        mobj_Author.NomineeFax = Author.NomineeFax;
                        mobj_Author.NomineePanNo = Author.NomineePanNo;

                        mobj_Author.Remark = Author.Remark;


                        mobj_Author.ModifiedBy = Author.EnteredBy;
                        mobj_Author.ModifiedDate = System.DateTime.Now;


                        mobj_Author.NomineeAccountNo = Author.NomineeAccountNo;
                        mobj_Author.NomineeBranchName = Author.NomineeBranchName;
                        mobj_Author.NomineeBankName = Author.NomineeBankName;
                        mobj_Author.NomineeIFSECode = Author.NomineeIFSECode;


                        _AuthorTypeService.UpdateAuthor(mobj_Author);




                        if (Author.DocumentId != null)
                        {

                            AuhtorDocument _AuhtorDoc = new AuhtorDocument();
                            string[] docurl = Author.UploadFile.Split(',');
                            int i = 0;
                            List<string> AuthorDocumetId = new List<string>();
                            if (Author.DocumentId != null && Author.DocumentId != "0")
                            {
                                AuthorDocumetId.AddRange(Author.DocumentId.Split(',').Where(x => string.IsNullOrEmpty(x) == false).Distinct().ToList());
                            }

                            foreach (var item in AuthorDocumetId)
                            {

                                if (docurl[i] == (item))
                                {
                                    _AuthorTypeService.DeavtivateAuthorDocument(Author.Id, Author.EnteredBy);
                                }



                            }



                            foreach (string doc in Author.DocumentName)
                            {
                                AuhtorDocument Link = new AuhtorDocument();
                                Link.AuhtorId = Author.Id;
                                Link.DocumentName = doc;
                                Link.UploadFile = docurl[i];
                                Link.EnteredBy = Author.EnteredBy;
                                _AuthorTypeService.InsertAuthorDocumentLinking(Link);
                                i++;
                            }

                        }



                        if (Author.NomineeDocumentId != null)
                        {

                            NomineeAuthorDocumentMaster _NomineeAuhtorDoc = new NomineeAuthorDocumentMaster();

                            string[] docurlNominee = Author.NomineeUploadFile.Split(',');
                            int j = 0;
                            foreach (string doc in Author.NomineeDocumentName)
                            {
                                NomineeAuthorDocumentMaster Link = new NomineeAuthorDocumentMaster();
                                Link.AuhtorId = Author.Id;
                                Link.DocumentName = doc;
                                Link.UploadFile = docurlNominee[j];
                                Link.EnteredBy = Author.EnteredBy;
                                _AuthorTypeService.InsertNomineeAuthorDocumentLinking(Link);

                                j++;
                            }

                        }







                    }
                    status = _localizationService.GetResource("Master.API.Success.Message");
                }
                else
                {

                    if (StatusType == "PAN")
                    {
                        status = "PANDuplicate";
                    }
                    else if (StatusType == "AccountNo")
                    {
                        status = "AccountNoDuplicate";
                    }

                   
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

            return Json(SerializeObj.SerializeObject(new { status, Author_CodeValue, AuthorIdId }));
        }


        public IHttpActionResult AuthorDelete(AuthorMaster _Author)
        {

            string status = string.Empty;
            try
            {
                AuthorMaster _author = _AuthorTypeService.GetAuthorById(_Author);
                _author.Deactivate = "Y";
                _author.ModifiedBy = _Author.EnteredBy;
                _author.ModifiedDate = DateTime.Now;
                _AuthorTypeService.UpdateAuthor(_author);
                _AuthorTypeService.DeavtivateAuthorDocument(_Author.Id, _Author.EnteredBy);
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


        public IHttpActionResult AuthorSerch(AuthorSearchHistory SearchParam)
        {

            if (SearchParam.SessionId == "")
            {
                return Json("NOK");
            }
            else
            {
                var status = "";
                _AuthorTypeService.InsertSearchHistory(SearchParam);
                status = "OK";
                return Json(status);
            }



            //SqlParameter[] parameters = new SqlParameter[73];



            //parameters[0] = new SqlParameter("AuthorCode", SqlDbType.VarChar, 50);
            //if (Author.AuthorCode == null || Author.AuthorCode == "")
            //{
            //    parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[0].Value = "'" + Author.AuthorCode + "'";
            //}

            //parameters[1] = new SqlParameter("Type", SqlDbType.VarChar, 50);
            //if (Author.Type == null || Author.Type == "")
            //{
            //    parameters[1].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[1].Value = Author.Type;
            //}

            //parameters[2] = new SqlParameter("FirstName", SqlDbType.VarChar, 50);
            //if (Author.FirstName == null || Author.FirstName == "")
            //{
            //    parameters[2].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[2].Value = "'" + Author.FirstName + "'";
            //}

            //parameters[3] = new SqlParameter("LastName", SqlDbType.VarChar, 50);
            //if (Author.LastName == null || Author.LastName == "")
            //{
            //    parameters[3].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[3].Value = "'" + Author.LastName + "'";
            //}


            //parameters[4] = new SqlParameter("Address", SqlDbType.VarChar, 50);
            //if (Author.Address == null || Author.Address == "")
            //{
            //    parameters[4].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[4].Value = "'" + Author.Address + "'";
            //}
            //parameters[5] = new SqlParameter("ResidencyStatus", SqlDbType.VarChar, 50);
            //if (Author.ResidencyStatus == null || Author.ResidencyStatus == "")
            //{
            //    parameters[5].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[5].Value = Author.ResidencyStatus;
            //}

            //parameters[6] = new SqlParameter("CountryId", SqlDbType.VarChar, 50);
            //if (Author.CountryId == 0 || Author.CountryId == null)
            //{
            //    parameters[6].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[6].Value = Author.CountryId;
            //}

            //parameters[7] = new SqlParameter("OtherCountry", SqlDbType.VarChar, 50);
            //if (Author.OtherCountry == null || Author.OtherCountry == "")
            //{
            //    parameters[7].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[7].Value = "'" + Author.OtherCountry + "'";
            //}
            //parameters[8] = new SqlParameter("StateId", SqlDbType.VarChar, 50);
            //if (Author.StateId == 0 || Author.StateId == null)
            //{
            //    parameters[8].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[8].Value = Author.StateId;
            //}


            //parameters[9] = new SqlParameter("OtherState", SqlDbType.VarChar, 50);
            //if (Author.OtherState == null || Author.OtherState == "")
            //{
            //    parameters[9].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[9].Value = "'" + Author.OtherState + "'";
            //}

            //parameters[10] = new SqlParameter("CityId", SqlDbType.VarChar, 50);
            //if (Author.CityId == 0 || Author.CityId == null)
            //{
            //    parameters[10].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[10].Value = Author.CityId;
            //}

            //parameters[11] = new SqlParameter("OtherCity", SqlDbType.VarChar, 50);
            //if (Author.OtherCity == null || Author.OtherCity == "")
            //{
            //    parameters[11].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[11].Value = "'" + Author.OtherCity + "'";
            //}

            //parameters[12] = new SqlParameter("PinCode", SqlDbType.VarChar, 50);
            //if (Author.PinCode == null || Author.PinCode == "")
            //{
            //    parameters[12].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[12].Value = Author.PinCode;
            //}


            //parameters[13] = new SqlParameter("Email", SqlDbType.VarChar, 50);
            //if (Author.Email == null || Author.Email == "")
            //{
            //    parameters[13].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[13].Value = "'" + Author.Email + "'";
            //}
            //parameters[14] = new SqlParameter("Phone", SqlDbType.VarChar, 50);
            //if (Author.Phone == null || Author.Phone == "")
            //{
            //    parameters[14].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[14].Value = Author.Phone;
            //}


            //parameters[15] = new SqlParameter("Mobile", SqlDbType.VarChar, 50);
            //if (Author.Mobile == null || Author.Mobile == "")
            //{
            //    parameters[15].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[15].Value = "'" + Author.Mobile + "'";
            //}

            //parameters[16] = new SqlParameter("Fax", SqlDbType.VarChar, 50);
            //if (Author.Fax == null || Author.Fax == "")
            //{
            //    parameters[16].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[16].Value = Author.Fax;
            //}

            //parameters[17] = new SqlParameter("PANNo", SqlDbType.VarChar, 50);
            //if (Author.PANNo == null || Author.PANNo == "")
            //{
            //    parameters[17].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[17].Value = Author.PANNo;
            //}


            //parameters[18] = new SqlParameter("AdharCardNo", SqlDbType.VarChar, 50);
            //if (Author.AdharCardNo == null || Author.AdharCardNo == "")
            //{
            //    parameters[18].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[18].Value = Author.AdharCardNo;
            //}

            //parameters[19] = new SqlParameter("ADateOfBirth", SqlDbType.VarChar, 50);
            //if (Author.DateOfBirth == null || Author.DateOfBirth == "")
            //{
            //    parameters[19].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[19].Value += "'" + Author.DateOfBirth + "'";
            //}

            //parameters[20] = new SqlParameter("DeathDate", SqlDbType.VarChar, 50);
            //if (Author.DeathDate == null || Author.DeathDate == "")
            //{
            //    parameters[20].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[20].Value += "'" + Author.DeathDate + "'";
            //}

            //parameters[21] = new SqlParameter("AccountNo", SqlDbType.VarChar, 50);
            //if (Author.AccountNo == null || Author.AccountNo == "")
            //{
            //    parameters[21].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[21].Value = Author.AccountNo;
            //}

            //parameters[22] = new SqlParameter("BankName", SqlDbType.VarChar, 50);
            //if (Author.BankName == null || Author.BankName == "")
            //{
            //    parameters[22].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[22].Value = "'" + Author.BankName + "'";
            //}

            //parameters[23] = new SqlParameter("BranchName", SqlDbType.VarChar, 50);
            //if (Author.BranchName == null || Author.BranchName == "")
            //{
            //    parameters[23].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[23].Value = "'" + Author.BranchName + "'";
            //}

            //parameters[24] = new SqlParameter("IFSECode", SqlDbType.VarChar, 50);
            //if (Author.IFSECode == null || Author.IFSECode == "")
            //{
            //    parameters[24].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[24].Value = Author.IFSECode;
            //}


            //parameters[25] = new SqlParameter("InstituteCompanyName", SqlDbType.VarChar, 50);
            //if (Author.InstituteCompanyName == null || Author.InstituteCompanyName == "")
            //{
            //    parameters[25].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[25].Value = "'" + Author.InstituteCompanyName + "'";
            //}


            //parameters[26] = new SqlParameter("AffiliationDesignation", SqlDbType.VarChar, 50);
            //if (Author.AffiliationDesignation == null || Author.AffiliationDesignation == "")
            //{
            //    parameters[26].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[26].Value = "'" + Author.AffiliationDesignation + "'";
            //}

            //parameters[27] = new SqlParameter("AffiliationDepartment", SqlDbType.VarChar, 50);
            //if (Author.AffiliationDepartment == null || Author.AffiliationDepartment == "")
            //{
            //    parameters[27].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[27].Value = "'" + Author.AffiliationDepartment + "'";
            //}


            //parameters[28] = new SqlParameter("AffiliationAddress", SqlDbType.VarChar, 50);
            //if (Author.AffiliationAddress == null || Author.AffiliationAddress == "")
            //{
            //    parameters[28].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[28].Value = "'" + Author.AffiliationAddress + "'";
            //}

            //parameters[29] = new SqlParameter("AffiliationCountryId", SqlDbType.VarChar, 50);
            //if (Author.AffiliationCountryId == 0 || Author.AffiliationCountryId == null)
            //{
            //    parameters[29].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[29].Value = Author.AffiliationCountryId;
            //}

            //parameters[30] = new SqlParameter("AffiliationOtherCountry", SqlDbType.VarChar, 50);
            //if (Author.AffiliationOtherCountry == null || Author.AffiliationOtherCountry == "")
            //{
            //    parameters[30].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[30].Value = "'" + Author.AffiliationOtherCountry + "'";
            //}

            //parameters[31] = new SqlParameter("AffiliationStateId", SqlDbType.VarChar, 50);
            //if (Author.AffiliationStateId == 0 || Author.AffiliationStateId == null)
            //{
            //    parameters[31].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[31].Value = Author.AffiliationStateId;
            //}


            //parameters[32] = new SqlParameter("AffiliationOtherState", SqlDbType.VarChar, 50);
            //if (Author.AffiliationOtherState == null || Author.AffiliationOtherState == "")
            //{
            //    parameters[32].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[32].Value = "'" + Author.AffiliationOtherState + "'";
            //}


            //parameters[33] = new SqlParameter("AffiliationCityId", SqlDbType.VarChar, 50);
            //if (Author.AffiliationCityId == 0 || Author.AffiliationCityId == null)
            //{
            //    parameters[33].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[33].Value = Author.AffiliationCityId;
            //}

            //parameters[34] = new SqlParameter("AffiliationOtherCity", SqlDbType.VarChar, 50);
            //if (Author.AffiliationOtherCity == null || Author.AffiliationOtherCity == "")
            //{
            //    parameters[34].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[34].Value = "'" + Author.AffiliationOtherCity + "'";
            //}

            //parameters[35] = new SqlParameter("AffiliationPinCode", SqlDbType.VarChar, 50);
            //if (Author.AffiliationPinCode == null || Author.AffiliationPinCode == "")
            //{
            //    parameters[35].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[35].Value = Author.AffiliationPinCode;
            //}

            //parameters[36] = new SqlParameter("AffiliationPhone", SqlDbType.VarChar, 50);
            //if (Author.AffiliationPhone == null || Author.AffiliationPhone == "")
            //{
            //    parameters[36].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[36].Value = Author.AffiliationPhone;
            //}

            //parameters[37] = new SqlParameter("AffiliationEmail", SqlDbType.VarChar, 50);
            //if (Author.AffiliationEmail == null || Author.AffiliationEmail == "")
            //{
            //    parameters[37].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[37].Value = "'" + Author.AffiliationEmail + "'";
            //}

            //parameters[38] = new SqlParameter("AffiliationWebSite", SqlDbType.VarChar, 50);
            //if (Author.AffiliationWebSite == null || Author.AffiliationWebSite == "")
            //{
            //    parameters[38].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[38].Value = "'" + Author.AffiliationWebSite + "'";
            //}

            //parameters[39] = new SqlParameter("BeneficiaryName", SqlDbType.VarChar, 50);
            //if (Author.BeneficiaryName == null || Author.BeneficiaryName == "")
            //{
            //    parameters[39].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[39].Value = "'" + Author.BeneficiaryName + "'";
            //}

            //parameters[40] = new SqlParameter("BeneficiaryRelation", SqlDbType.VarChar, 50);
            //if (Author.BeneficiaryRelation == null || Author.BeneficiaryRelation == "")
            //{
            //    parameters[40].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[40].Value = "'" + Author.BeneficiaryRelation + "'";
            //}

            //parameters[41] = new SqlParameter("BeneficiaryAddress", SqlDbType.VarChar, 50);
            //if (Author.BeneficiaryAddress == null || Author.BeneficiaryAddress == "")
            //{
            //    parameters[41].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[41].Value = "'" + Author.BeneficiaryAddress + "'";
            //}

            //parameters[42] = new SqlParameter("BeneficiaryCountryId", SqlDbType.VarChar, 50);
            //if (Author.BeneficiaryCountryId == 0 || Author.BeneficiaryCountryId == null)
            //{
            //    parameters[42].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[42].Value = Author.BeneficiaryCountryId;
            //}

            //parameters[43] = new SqlParameter("BeneficiaryOtherCountry", SqlDbType.VarChar, 50);
            //if (Author.BeneficiaryOtherCountry == null || Author.BeneficiaryOtherCountry == "")
            //{
            //    parameters[43].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[43].Value = "'" + Author.BeneficiaryOtherCountry + "'";
            //}

            //parameters[44] = new SqlParameter("BeneficiaryStateId", SqlDbType.VarChar, 50);
            //if (Author.BeneficiaryStateId == 0 || Author.BeneficiaryStateId == null)
            //{
            //    parameters[44].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[44].Value = Author.BeneficiaryStateId;
            //}

            //parameters[45] = new SqlParameter("BeneficiaryOtherState", SqlDbType.VarChar, 50);
            //if (Author.BeneficiaryOtherState == null || Author.BeneficiaryOtherState == "")
            //{
            //    parameters[45].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[45].Value = "'" + Author.BeneficiaryOtherState + "'";
            //}

            //parameters[46] = new SqlParameter("BeneficiaryCityId", SqlDbType.VarChar, 50);
            //if (Author.BeneficiaryCityId == 0 || Author.BeneficiaryCityId == null)
            //{
            //    parameters[46].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[46].Value = Author.BeneficiaryCityId;
            //}

            //parameters[47] = new SqlParameter("BeneficiaryOtherCity", SqlDbType.VarChar, 50);
            //if (Author.BeneficiaryOtherCity == null || Author.BeneficiaryOtherCity == "")
            //{
            //    parameters[47].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[47].Value = "'" + Author.BeneficiaryOtherCity + "'";
            //}


            //parameters[48] = new SqlParameter("BeneficiaryPinCode", SqlDbType.VarChar, 50);
            //if (Author.BeneficiaryPinCode == null || Author.BeneficiaryPinCode == "")
            //{
            //    parameters[48].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[48].Value = Author.BeneficiaryPinCode;
            //}

            //parameters[49] = new SqlParameter("BeneficiaryEmail", SqlDbType.VarChar, 50);
            //if (Author.BeneficiaryEmail == null || Author.BeneficiaryEmail == "")
            //{
            //    parameters[49].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[49].Value = "'" + Author.BeneficiaryEmail + "'";
            //}

            //parameters[50] = new SqlParameter("BeneficiaryPhone", SqlDbType.VarChar, 50);
            //if (Author.BeneficiaryPhone == null || Author.BeneficiaryPhone == "")
            //{
            //    parameters[50].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[50].Value = Author.BeneficiaryPhone;
            //}

            //parameters[51] = new SqlParameter("BeneficiaryMobile", SqlDbType.VarChar, 50);
            //if (Author.BeneficiaryMobile == null || Author.BeneficiaryMobile == "")
            //{
            //    parameters[51].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[51].Value = "'" + Author.BeneficiaryMobile + "'";
            //}

            //parameters[52] = new SqlParameter("BeneficiaryFax", SqlDbType.VarChar, 50);
            //if (Author.BeneficiaryFax == null || Author.BeneficiaryFax == "")
            //{
            //    parameters[52].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[52].Value = Author.BeneficiaryFax;
            //}

            //parameters[53] = new SqlParameter("BeneficiaryPanNo", SqlDbType.VarChar, 50);
            //if (Author.BeneficiaryPanNo == null || Author.BeneficiaryPanNo == "")
            //{
            //    parameters[53].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[53].Value = Author.BeneficiaryPanNo;
            //}
            //parameters[54] = new SqlParameter("BeneficiaryAccountNo", SqlDbType.VarChar, 50);
            //if (Author.BeneficiaryAccountNo == null || Author.BeneficiaryAccountNo == "")
            //{
            //    parameters[54].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[54].Value = Author.BeneficiaryAccountNo;
            //}

            //parameters[55] = new SqlParameter("BeneficiaryBankName", SqlDbType.VarChar, 50);
            //if (Author.BeneficiaryBankName == null || Author.BeneficiaryBankName == "")
            //{
            //    parameters[55].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[55].Value = "'" + Author.BeneficiaryBankName + "'";
            //}

            //parameters[56] = new SqlParameter("BeneficiaryBranchName", SqlDbType.VarChar, 50);
            //if (Author.BeneficiaryBranchName == null || Author.BeneficiaryBranchName == "")
            //{
            //    parameters[56].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[56].Value = "'" + Author.BeneficiaryBranchName + "'";
            //}

            //parameters[57] = new SqlParameter("BeneficiaryIFSECode", SqlDbType.VarChar, 50);
            //if (Author.BeneficiaryIFSECode == null || Author.BeneficiaryIFSECode == "")
            //{
            //    parameters[57].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[57].Value = Author.BeneficiaryIFSECode;
            //}

            //parameters[58] = new SqlParameter("NomineeName", SqlDbType.VarChar, 50);
            //if (Author.NomineeName == null || Author.NomineeName == "")
            //{
            //    parameters[58].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[58].Value = "'" + Author.NomineeName + "'";
            //}

            //parameters[59] = new SqlParameter("NomineeRelation", SqlDbType.VarChar, 50);
            //if (Author.NomineeRelation == null || Author.NomineeRelation == "")
            //{
            //    parameters[59].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[59].Value = "'" + Author.NomineeRelation + "'";
            //}


            //parameters[60] = new SqlParameter("NomineeAddress", SqlDbType.VarChar, 50);
            //if (Author.NomineeAddress == null || Author.NomineeAddress == "")
            //{
            //    parameters[60].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[60].Value = "'" + Author.NomineeAddress + "'";
            //}

            //parameters[61] = new SqlParameter("NomineeCountryId", SqlDbType.VarChar, 50);
            //if (Author.NomineeCountryId == 0 || Author.NomineeCountryId == null)
            //{
            //    parameters[61].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[61].Value = Author.NomineeCountryId;
            //}


            //parameters[62] = new SqlParameter("NomineeOtherCountry", SqlDbType.VarChar, 50);
            //if (Author.NomineeOtherCountry == null || Author.NomineeOtherCountry == "")
            //{
            //    parameters[62].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[62].Value = "'" + Author.NomineeOtherCountry + "'";
            //}
            //parameters[63] = new SqlParameter("NomineeStateId", SqlDbType.VarChar, 50);
            //if (Author.NomineeStateId == 0 || Author.NomineeStateId == null)
            //{
            //    parameters[63].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[63].Value = Author.NomineeStateId;
            //}

            //parameters[64] = new SqlParameter("NomineeOtherState", SqlDbType.VarChar, 50);
            //if (Author.NomineeOtherState == null || Author.NomineeOtherState == "")
            //{
            //    parameters[64].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[64].Value = "'" + Author.NomineeOtherState + "'";
            //}

            //parameters[65] = new SqlParameter("NomineeCityId", SqlDbType.VarChar, 50);
            //if (Author.NomineeCityId == 0 || Author.NomineeCityId == null)
            //{
            //    parameters[65].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[65].Value = Author.NomineeCityId;
            //}

            //parameters[66] = new SqlParameter("NomineeOtherCity", SqlDbType.VarChar, 50);
            //if (Author.NomineeOtherCity == null || Author.NomineeOtherCity == "")
            //{
            //    parameters[66].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[66].Value = "'" + Author.NomineeOtherCity + "'";
            //}

            //parameters[67] = new SqlParameter("NomineePinCode", SqlDbType.VarChar, 50);
            //if (Author.NomineePinCode == null || Author.NomineePinCode == "")
            //{
            //    parameters[67].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[67].Value = Author.NomineePinCode;
            //}

            //parameters[68] = new SqlParameter("NomineeEmail", SqlDbType.VarChar, 50);
            //if (Author.NomineeEmail == null || Author.NomineeEmail == "")
            //{
            //    parameters[68].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[68].Value = "'" + Author.NomineeEmail + "'";
            //}

            //parameters[69] = new SqlParameter("NomineePhone", SqlDbType.VarChar, 50);
            //if (Author.NomineePhone == null || Author.NomineePhone == "")
            //{
            //    parameters[69].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[69].Value = Author.NomineePhone;
            //}
            //parameters[70] = new SqlParameter("NomineeMobile", SqlDbType.VarChar, 50);
            //if (Author.NomineeMobile == null || Author.NomineeMobile == "")
            //{
            //    parameters[70].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[70].Value = "'" + Author.NomineeMobile + "'";
            //}

            //parameters[71] = new SqlParameter("NomineeFax", SqlDbType.VarChar, 50);
            //if (Author.NomineeFax == null || Author.NomineeFax == "")
            //{
            //    parameters[71].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[71].Value = Author.NomineeFax;
            //}


            //parameters[72] = new SqlParameter("NomineePanNo", SqlDbType.VarChar, 50);
            //if (Author.NomineePanNo == null || Author.NomineePanNo == "")
            //{
            //    parameters[72].Value = System.Data.SqlTypes.SqlInt32.Null;
            //}
            //else
            //{
            //    parameters[72].Value = Author.NomineePanNo;
            //}



            //var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<AuthorMasterDetail>("Proc_AuthorSerchReport_get", parameters).ToList();


            //return Json(_GetAuthorReport);

        }




        [HttpGet]
        public IHttpActionResult GetAuthorSearchList(String SessionId)
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
                    var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<AuthorMasterDetail>("Proc_AuthorSerchReport_get", parameters).ToList();
                    return Json(_GetAuthorReport);
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        //Added by Saddam/

        public IHttpActionResult WebGetaAuthorById(AuthorMaster Author)
        {

            string mstr_DateOfBirth = string.Empty;
            string mstr_DateOfDeath = string.Empty;
            AuthorMaster _AuthorList = _AuthorTypeService.GetAuthorById(Author);
            if (_AuthorList.DateOfBirth != null)
            {
                mstr_DateOfBirth = Convert.ToDateTime(_AuthorList.DateOfBirth).toDDMMYYYY();
            }
            if (_AuthorList.DeathDate != null)
            {
                mstr_DateOfDeath = Convert.ToDateTime(_AuthorList.DeathDate).toDDMMYYYY();
            }
            var AuthorList = new
            {
                Id = _AuthorList.Id,
                AuthorCode = _AuthorList.AuthorCode,
                AuthorSAPCode = _AuthorList.AuthorSAPCode,
                AuthorType = _AuthorList.Type,
                FirstName = _AuthorList.FirstName,
                LastName = _AuthorList.LastName,
                Address = _AuthorList.Address,
                ResidencyStatus = _AuthorList.ResidencyStatus,
                CountryId = _AuthorList.CountryId,
                OtherCountry = _AuthorList.OtherCountry,
                StateId = _AuthorList.StateId,
                OtherState = _AuthorList.OtherState,
                CityId = _AuthorList.CityId,
                OtherCity = _AuthorList.OtherCity,
                PinCode = _AuthorList.PinCode,
                Email = _AuthorList.Email,
                Phone = _AuthorList.Phone,
                Mobile = _AuthorList.Mobile,
                Fax = _AuthorList.Fax,
                PANNo = _AuthorList.PANNo,
                AdharCardNo = _AuthorList.AdharCardNo,
                DateOfBirth = mstr_DateOfBirth,
                DeathDate = mstr_DateOfDeath,
                AccountNo = _AuthorList.AccountNo,
                BankName = _AuthorList.BankName,
                BranchName = _AuthorList.BranchName,
                IFSECode = _AuthorList.IFSECode,
                InstituteCompanyName = _AuthorList.InstituteCompanyName,
                AffiliationDesignation = _AuthorList.AffiliationDesignation,
                AffiliationDepartment = _AuthorList.AffiliationDepartment,
                AffiliationAddress = _AuthorList.AffiliationAddress,
                AffiliationCountryId = _AuthorList.AffiliationCountryId,
                AffiliationOtherCountry = _AuthorList.AffiliationOtherCountry,

                AffiliationStateId = _AuthorList.AffiliationStateId,
                AffiliationOtherState = _AuthorList.AffiliationOtherState,
                AffiliationCityId = _AuthorList.AffiliationCityId,
                AffiliationOtherCity = _AuthorList.AffiliationOtherCity,
                AffiliationPinCode = _AuthorList.AffiliationPinCode,
                AffiliationPhone = _AuthorList.AffiliationPhone,
                AffiliationEmail = _AuthorList.AffiliationEmail,
                AffiliationWebSite = _AuthorList.AffiliationWebSite,
                BeneficiaryName = _AuthorList.BeneficiaryName,
                BeneficiaryRelation = _AuthorList.BeneficiaryRelation,
                BeneficiaryAddress = _AuthorList.BeneficiaryAddress,
                BeneficiaryCountryId = _AuthorList.BeneficiaryCountryId,
                BeneficiaryOtherCountry = _AuthorList.BeneficiaryOtherCountry,
                BeneficiaryStateId = _AuthorList.BeneficiaryStateId,
                BeneficiaryOtherState = _AuthorList.BeneficiaryOtherState,
                BeneficiaryCityId = _AuthorList.BeneficiaryCityId,
                BeneficiaryOtherCity = _AuthorList.BeneficiaryOtherCity,
                BeneficiaryPinCode = _AuthorList.BeneficiaryPinCode,
                BeneficiaryEmail = _AuthorList.BeneficiaryEmail,
                BeneficiaryPhone = _AuthorList.BeneficiaryPhone,
                BeneficiaryMobile = _AuthorList.BeneficiaryMobile,
                BeneficiaryFax = _AuthorList.BeneficiaryFax,
                BeneficiaryPanNo = _AuthorList.BeneficiaryPanNo,
                BeneficiaryAccountNo = _AuthorList.BeneficiaryAccountNo,
                BeneficiaryBankName = _AuthorList.BeneficiaryBankName,
                BeneficiaryBranchName = _AuthorList.BeneficiaryBranchName,
                BeneficiaryIFSECode = _AuthorList.BeneficiaryIFSECode,
                NomineeName = _AuthorList.NomineeName,
                NomineeRelation = _AuthorList.NomineeRelation,
                NomineeAddress = _AuthorList.NomineeAddress,
                NomineeCountryId = _AuthorList.NomineeCountryId,
                NomineeOtherCountry = _AuthorList.NomineeOtherCountry,


                NomineeStateId = _AuthorList.NomineeStateId,
                NomineeOtherState = _AuthorList.NomineeOtherState,
                NomineeCityId = _AuthorList.NomineeCityId,
                NomineeOtherCity = _AuthorList.NomineeOtherCity,
                NomineePinCode = _AuthorList.NomineePinCode,
                NomineeEmail = _AuthorList.NomineeEmail,
                NomineePhone = _AuthorList.NomineePhone,
                NomineeMobile = _AuthorList.NomineeMobile,
                NomineeFax = _AuthorList.NomineeFax,
                NomineePanNo = _AuthorList.NomineePanNo,
                Remark = _AuthorList.Remark,
                NomineeAccountNo = _AuthorList.NomineeAccountNo,
                NomineeBranchName = _AuthorList.NomineeBranchName,
                NomineeBankName = _AuthorList.NomineeBankName,
                NomineeIFSECode = _AuthorList.NomineeIFSECode,

            };


            AuthorModel AuthorDocument = new AuthorModel();

            var documents = _AuhtorDocument.Table.Where(x => x.AuhtorId == _AuthorList.Id && x.Deactivate == "N").ToList();

            AuthorDocument.DocumentIds = documents.Select(i => i.Id).ToArray();


            AuthorDocument.DocumentName = documents.Select(i => i.DocumentName).ToArray();
            foreach (var docs in documents)
                AuthorDocument.UploadFile = AuthorDocument.UploadFile + docs.UploadFile + ",";





            AuthorModel NomineeAuthorDocument = new AuthorModel();

            var Nomineedocuments = _NomineeAuthorDocumentMaster.Table.Where(x => x.AuhtorId == _AuthorList.Id && x.Deactivate == "N").ToList();

            NomineeAuthorDocument.NomineeDocumentIds = Nomineedocuments.Select(i => i.Id).ToArray();


            NomineeAuthorDocument.NomineeDocumentName = Nomineedocuments.Select(i => i.DocumentName).ToArray();
            foreach (var Nomineedocs in Nomineedocuments)
                NomineeAuthorDocument.NomineeUploadFile = NomineeAuthorDocument.NomineeUploadFile + Nomineedocs.UploadFile + ",";




            //   IList<AuthorMaster> _ProductAuthorList = ProductM1.ProductProductAuthorLink.ToList();


            return Json(SerializeObj.SerializeObject(new { AuthorList, AuthorDocument, NomineeAuthorDocument }));



        }


        public IHttpActionResult AuthorList(AuthorMaster Auhtor)
        {
            IList<AuthorMaster> _AuthorList = _AuthorTypeService.GetAuthorist(Auhtor).ToList();
            var AuthorSuggesation = _AuthorList.Select(Au => new
            {
                AuthorId = Au.Id,
                AuthorName = Au.FirstName + " " + Au.LastName
            });
            return Json(SerializeObj.SerializeObject(new { AuthorSuggesation }));

        }

        //public IHttpActionResult WebGetaAuthorSearch(AuthorMaster Author)
        //{
        //    //var AuthorSearch = _AuthorRepository.Table.Where(x => x.Id == Author.Id).Select(x => x.FirstName).FirstOrDefault().FirstOrDefault();
        //    //return AuthorSearch;


        //    //  var Custom = _AuthorRepository.Table.Where(x => x.Id == Author.Id).Select(x => x.FirstName).FirstOrDefault().FirstOrDefault();
        //    //return Custom
        //}

        //Added by sanjeet 


        public IHttpActionResult ProductSearch(SearchHistory SearchParam)
        {
            if (SearchParam.SessionId == "")
            {
                return Json("NOK");
            }
            else
            {
                var status = "";
                _IProductMasterService.InsertSearchHistory(SearchParam);
                status = "OK";
                return Json(status);
            }

          

        }

        [HttpGet]
        public IHttpActionResult GetSerachProductListing(String SessionId)
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
                    var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<ProductSearchDetails>("Proc_ProductSerchReport_get", parameters).ToList();
                    return Json(_GetAuthorReport);
                }
            }
            catch( Exception ex)
            {
                return Json("error");
                //return null;
            }
            
        }



        //SqlParameter[] parameters = new SqlParameter[37];

        //try
        //{
        //    if (productDetail != null)
        //    {

        //        parameters[0] = new SqlParameter("ProductType", SqlDbType.VarChar, 50);
        //        if (productDetail.ProductType == null || productDetail.ProductType == "")
        //        {
        //            parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[0].Value = productDetail.ProductType;
        //        }

        //        parameters[1] = new SqlParameter("SubProductType", SqlDbType.VarChar, 50);
        //        if (productDetail.SubProductType == null || productDetail.SubProductType == "")
        //        {
        //            parameters[1].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[1].Value = productDetail.SubProductType;
        //        }

        //        parameters[2] = new SqlParameter("ProductCategory", SqlDbType.VarChar, 50);
        //        if (productDetail.ProductCategory == null || productDetail.ProductCategory == "")
        //        {
        //            parameters[2].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[2].Value = productDetail.ProductCategory;
        //        }

        //        parameters[3] = new SqlParameter("DivisionId", SqlDbType.VarChar, 50);
        //        if (productDetail.DivisionId == 0)
        //        {
        //            parameters[3].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[3].Value = productDetail.DivisionId;
        //        }

        //        parameters[4] = new SqlParameter("SubDivision", SqlDbType.VarChar, 50);
        //        if (productDetail.SubDivision == "" || productDetail.SubDivision ==null)
        //        {
        //            parameters[4].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[4].Value = productDetail.SubDivision;
        //        }

        //        parameters[5] = new SqlParameter("ProjectCode", SqlDbType.VarChar, 50);
        //        if (productDetail.ProjectCode == null || productDetail.ProjectCode == "")
        //        {
        //            parameters[5].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[5].Value = "'"+productDetail.ProjectCode+"'";
        //        }

        //        parameters[6] = new SqlParameter("SapAgreementNo", SqlDbType.VarChar, 50);
        //        if (productDetail.SapAgreementNo == null || productDetail.SapAgreementNo == "")
        //        {
        //            parameters[6].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[6].Value = productDetail.SapAgreementNo;
        //        }

        //        parameters[7] = new SqlParameter("OupIsbn", SqlDbType.VarChar, 50);
        //        if (productDetail.OupIsbn == null || productDetail.OupIsbn == "")
        //        {
        //            parameters[7].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[7].Value = productDetail.OupIsbn;
        //        }

        //        parameters[8] = new SqlParameter("WorkingProduct", SqlDbType.VarChar, 50);
        //        if (productDetail.WorkingProduct == null || productDetail.WorkingProduct == "")
        //        {
        //            parameters[8].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[8].Value = productDetail.WorkingProduct;
        //        }

        //        parameters[9] = new SqlParameter("WorkingSubProduct", SqlDbType.VarChar, 50);
        //        if (productDetail.WorkingSubProduct == null || productDetail.WorkingSubProduct == "")
        //        {
        //            parameters[9].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[9].Value = productDetail.WorkingSubProduct;
        //        }

        //        parameters[10] = new SqlParameter("OupEdition", SqlDbType.VarChar, 50);
        //        if (productDetail.OupEdition == null || productDetail.OupEdition == "")
        //        {
        //            parameters[10].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[10].Value = productDetail.OupEdition;
        //        }


        //        parameters[11] = new SqlParameter("Volume", SqlDbType.VarChar, 50);
        //        if (productDetail.Volume == null || productDetail.Volume == "")
        //        {
        //            parameters[11].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[11].Value = productDetail.Volume;
        //        }

        //        parameters[12] = new SqlParameter("CopyrightYear", SqlDbType.VarChar, 50);
        //        if (productDetail.CopyrightYear == null || productDetail.CopyrightYear == "")
        //        {
        //            parameters[12].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[12].Value = productDetail.CopyrightYear;
        //        }

        //        parameters[13] = new SqlParameter("Imprint", SqlDbType.VarChar, 50);
        //        if (productDetail.Imprint == null || productDetail.Imprint == "")
        //        {
        //            parameters[13].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[13].Value = productDetail.Imprint;
        //        }

        //        parameters[14] = new SqlParameter("Language", SqlDbType.VarChar, 50);
        //        if (productDetail.Language == null || productDetail.Language == "")
        //        {
        //            parameters[14].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[14].Value = productDetail.Language;
        //        }

        //        parameters[15] = new SqlParameter("Series", SqlDbType.VarChar, 200);
        //        if (productDetail.Series == null || productDetail.Series == "")
        //        {
        //            parameters[15].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[15].Value = "'"+productDetail.Series+"'";
        //        }

        //        parameters[16] = new SqlParameter("AuthorName", SqlDbType.VarChar, 50);
        //        if (productDetail.AuthorName == null || productDetail.AuthorName == "")
        //        {
        //            parameters[16].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[16].Value = productDetail.AuthorName;
        //        }

        //        parameters[17] = new SqlParameter("Derivatives", SqlDbType.VarChar, 50);
        //        if (productDetail.Derivatives == null || productDetail.Derivatives == "")
        //        {
        //            parameters[17].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[17].Value = productDetail.Derivatives;
        //        }

        //        parameters[18] = new SqlParameter("ProjectedDate", SqlDbType.VarChar, 50);
        //        if (productDetail.ProjectedDate == null )
        //        {
        //            parameters[18].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {

        //            parameters[18].Value += "'" + productDetail.ProjectedDate + "'";
        //        }

        //        parameters[19] = new SqlParameter("ProjectedPrice", SqlDbType.VarChar, 50);
        //        if (productDetail.ProjectedPrice == null || productDetail.ProjectedPrice == "")
        //        {
        //            parameters[19].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[19].Value = productDetail.ProjectedPrice;
        //        }

        //        parameters[20] = new SqlParameter("ProjectedCurrency", SqlDbType.VarChar, 50);
        //        if (productDetail.ProjectedCurrency == null || productDetail.ProjectedCurrency == "")
        //        {
        //            parameters[20].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[20].Value = productDetail.ProjectedCurrency;
        //        }

        //        parameters[21] = new SqlParameter("ProprietorAuthorName", SqlDbType.VarChar, 50);
        //        if (productDetail.ProprietorAuthorName == null || productDetail.ProprietorAuthorName == "")
        //        {
        //            parameters[21].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[21].Value = productDetail.ProprietorAuthorName;
        //        }

        //        parameters[22] = new SqlParameter("ProprietorPubCenter", SqlDbType.VarChar, 50);
        //        if (productDetail.ProprietorPubCenter == null || productDetail.ProprietorPubCenter == "")
        //        {
        //            parameters[22].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[22].Value = productDetail.ProprietorPubCenter;
        //        }

        //        parameters[23] = new SqlParameter("ProprietorPublishingCompany", SqlDbType.VarChar, 50);
        //        if (productDetail.ProprietorPublishingCompany == null || productDetail.ProprietorPublishingCompany == "")
        //        {
        //            parameters[23].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[23].Value = productDetail.ProprietorPublishingCompany;
        //        }

        //        parameters[24] = new SqlParameter("ProprietorProduct", SqlDbType.VarChar, 50);
        //        if (productDetail.ProprietorProduct == null || productDetail.ProprietorProduct == "")
        //        {
        //            parameters[24].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[24].Value = productDetail.ProprietorProduct;
        //        }

        //        parameters[25] = new SqlParameter("ProprietorImprint", SqlDbType.VarChar, 50);
        //        if (productDetail.ProprietorImprint == null || productDetail.ProprietorImprint == "")
        //        {
        //            parameters[25].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[25].Value = productDetail.ProprietorImprint;
        //        }



        //        parameters[26] = new SqlParameter("SapAuthorCode", SqlDbType.VarChar, 50);
        //        if (productDetail.SapAuthorCode == null || productDetail.SapAuthorCode == "")
        //        {
        //            parameters[26].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[26].Value = productDetail.SapAuthorCode;
        //        }

        //        parameters[27] = new SqlParameter("FinalProductEntered", SqlDbType.VarChar, 50);
        //        if (productDetail.FinalProductEntered == null || productDetail.FinalProductEntered == "")
        //        {
        //            parameters[27].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[27].Value = productDetail.FinalProductEntered;
        //        }

        //        parameters[28] = new SqlParameter("FinalProduct", SqlDbType.VarChar, 50);
        //        if (productDetail.FinalProduct == null || productDetail.FinalProduct == "")
        //        {
        //            parameters[28].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[28].Value = productDetail.FinalProduct;
        //        }

        //        parameters[29] = new SqlParameter("FinalPublishingDate", SqlDbType.VarChar, 50);
        //        if (productDetail.FinalPublishingDate == null)
        //        {
        //            parameters[29].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {

        //            parameters[29].Value += "'" + productDetail.FinalPublishingDate + "'";

        //        }

        //        parameters[30] = new SqlParameter("ProprietorIsbn", SqlDbType.VarChar, 50);
        //        if (productDetail.ProprietorIsbn == null || productDetail.ProprietorIsbn == "")
        //        {
        //            parameters[30].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[30].Value = productDetail.ProprietorIsbn;
        //        }

        //        parameters[31] = new SqlParameter("ProprietorEdition", SqlDbType.VarChar, 50);
        //        if (productDetail.ProprietorEdition == null || productDetail.ProprietorEdition == "")
        //        {
        //            parameters[31].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[31].Value = productDetail.ProprietorEdition;
        //        }

        //        parameters[32] = new SqlParameter("ProprietorCopyright", SqlDbType.VarChar, 50);
        //        if (productDetail.ProprietorCopyright == null || productDetail.ProprietorCopyright == "")
        //        {
        //            parameters[32].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[32].Value = productDetail.ProprietorCopyright;
        //        }
        //        parameters[33] = new SqlParameter("DepartmentId", SqlDbType.Int, 2);
        //        if (productDetail.DepartmentId == 0)
        //        {
        //            parameters[33].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[33].Value = productDetail.DepartmentId;
        //        }
        //        parameters[34] = new SqlParameter("TypeFor", SqlDbType.VarChar, 50);
        //        if (productDetail.TypeFor == null || productDetail.TypeFor =="")
        //        {
        //            parameters[34].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[34].Value = productDetail.TypeFor;
        //        }
        //        parameters[35] = new SqlParameter("AuthorCategory", SqlDbType.VarChar, 50);
        //        if (productDetail.AuthorCategory == null || productDetail.AuthorCategory == "")
        //        {
        //            parameters[35].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[35].Value = productDetail.AuthorCategory;
        //        }
        //        parameters[36] = new SqlParameter("ProprietorAuthorType", SqlDbType.VarChar, 50);
        //        if (productDetail.ProprietorAuthorCategory == null || productDetail.ProprietorAuthorCategory == "")
        //        {
        //            parameters[36].Value = System.Data.SqlTypes.SqlInt32.Null;
        //        }
        //        else
        //        {
        //            parameters[36].Value = productDetail.ProprietorAuthorCategory;
        //        }

        //    }
        //    var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<ProductSearchDetails>("Proc_ProductSerchReport_get", parameters).ToList();




        [HttpPost]
        public IHttpActionResult AuthorSerchView(AuthorMasterDetail Author)
        {

            SqlParameter[] parameters = new SqlParameter[1];

            parameters[0] = new SqlParameter("AuthorId", SqlDbType.VarChar, 50);
            if (Author.Id == 0)
            {
                parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[0].Value = Author.Id;
            }




            var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<AuthorMasterDetail>("Proc_AuthorSerchView_get", parameters).ToList();



      return Json(_GetAuthorReport);

           

        }




        [HttpPost]
        public IHttpActionResult ViewAuthorDetials(AuthorMasterDetail Author)
        {

            SqlParameter[] parameters = new SqlParameter[1];

            parameters[0] = new SqlParameter("AuthorId", SqlDbType.VarChar, 50);
            if (Author.Id == 0)
            {
                parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[0].Value = Author.Id;
            }




            var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<AuthorMasterDetail>("Proc_AuthorSerchView_get", parameters).ToList();


            AuthorModel AuthorDocument = new AuthorModel();

            var documents = _AuhtorDocument.Table.Where(x => x.AuhtorId == Author.Id && x.Deactivate == "N").ToList();

            AuthorDocument.DocumentIds = documents.Select(i => i.Id).ToArray();


            AuthorDocument.DocumentName = documents.Select(i => i.DocumentName).ToArray();
            foreach (var docs in documents)
                AuthorDocument.UploadFile = AuthorDocument.UploadFile + docs.UploadFile + ",";





            AuthorModel NomineeAuthorDocument = new AuthorModel();

            var Nomineedocuments = _NomineeAuthorDocumentMaster.Table.Where(x => x.AuhtorId == Author.Id && x.Deactivate == "N").ToList();

            NomineeAuthorDocument.NomineeDocumentIds = Nomineedocuments.Select(i => i.Id).ToArray();


            NomineeAuthorDocument.NomineeDocumentName = Nomineedocuments.Select(i => i.DocumentName).ToArray();
            foreach (var Nomineedocs in Nomineedocuments)
                NomineeAuthorDocument.NomineeUploadFile = NomineeAuthorDocument.NomineeUploadFile + Nomineedocs.UploadFile + ",";


            //  return Json(_GetAuthorReport);

            return Json(SerializeObj.SerializeObject(new { _GetAuthorReport, AuthorDocument, NomineeAuthorDocument }));

        }



        public IHttpActionResult RemoveAuhtorDocument(AuthorModel Author)
        {
            AuhtorDocument document = _AuthorTypeService.getAuhtorDocumentDetail(Author.Id);



            string status = string.Empty;
            try
            {

                _AuthorTypeService.DeavtivateAuthorDocumentById(Author.Id, Author.EnteredBy);


                status = _localizationService.GetResource("Master.API.Success.Message");

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





        public IHttpActionResult RemoveNomineeAuhtorDocument(AuthorModel Author)
        {
            NomineeAuthorDocumentMaster document = _AuthorTypeService.getNomineeAuhtorDocumentDetail(Author.Id);



            string status = string.Empty;
            try
            {

                _AuthorTypeService.DeavtivateNomineeAuthorDocumentById(Author.Id, Author.EnteredBy);


                status = _localizationService.GetResource("Master.API.Success.Message");

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


        public IHttpActionResult getAuthorDepartmentList(AuthorDepartment AuhtorDept)
        {

            IList<AuthorDepartment> _AuthorList = _AuthorDepartment.Table.Where(d => d.DepartmentName != null && d.Deactivate == "N").OrderBy(c => c.DepartmentName).ToList();
            var query = _AuthorList.Select(Au => new
            {
                Id = Au.Id,
                DepartmentName = Au.DepartmentName
            });
            return Json(SerializeObj.SerializeObject(new { query }));

        }






    }
}
