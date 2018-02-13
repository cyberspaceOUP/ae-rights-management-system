using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Core;
using ACS.Services.Master;
using System.Text;
using ACS.Data;
using ACS.Core;
using ACS.Core.Data;
using ACS.Core.Domain.AuthorContract;
using ACS.Core.Domain.Master;

namespace SLV.Web.Areas.List.Controllers
{
    public class ListController : Controller
    {
        //Added by Ankush on 15/07/2016 List Controller

        #region PrivateProperty
        private readonly IWorkContext _workContext;
        private readonly IPublishingCompanyService _publishingCompanyService;
        private readonly IDbContext _dbContext;
        private readonly ILicenseeService _mobjLicenseeService;
        private readonly ICopyrightHolderService _mobjCopyrightHolderService;
        private readonly IGeographicalService _geographicalService;

        private readonly IRepository<ACS.Core.Domain.Product.ProductMaster> _mobjProductMasterRepository;
        private readonly IRepository<SeriesMaster> _mobjSeriesMasterRepository;
        private readonly IRepository<DivisionMaster> _mobjDivisionMasterRepository;
        private readonly IRepository<AuthorContractOriginal> _mobjAuthorContractRepository;
        private readonly IRepository<GeographicalMaster> _mobjGeographicalMasterRepository;
        private readonly IRepository<LicenseeMaster> _mobjLicenseeMasterRepository;
        private readonly IRepository<CopyRightHolderMaster> _mobjCopyRightHolderMasterRepository;

        #endregion

        #region Constructor
        public ListController(IWorkContext workContext,
            IPublishingCompanyService publishingCompanyService,
            IDbContext dbContext,
            ILicenseeService mobjLicenseeService,
            ICopyrightHolderService mobjCopyrightHolderService,
            IGeographicalService geographicalService,

            IRepository<SeriesMaster> mobjSeriesMasterRepository,
            IRepository<DivisionMaster> mobjDivisionMasterRepository,
            IRepository<ACS.Core.Domain.Product.ProductMaster> mobjProductMasterRepository,
            IRepository<AuthorContractOriginal> mobjAuthorContractRepository,
            IRepository<LicenseeMaster> mobjLicenseeMasterRepository,
            IRepository<GeographicalMaster> mobjGeographicalMasterRepository,
            IRepository<CopyRightHolderMaster> mobjCopyRightHolderMasterRepository
            )
        {
            _workContext = workContext;
            _publishingCompanyService = publishingCompanyService;
            _dbContext = dbContext;
            _mobjLicenseeService = mobjLicenseeService;
            _mobjCopyrightHolderService = mobjCopyrightHolderService;
            _geographicalService = geographicalService;

            _mobjSeriesMasterRepository = mobjSeriesMasterRepository;
            _mobjDivisionMasterRepository = mobjDivisionMasterRepository;
            _mobjProductMasterRepository = mobjProductMasterRepository;
            _mobjAuthorContractRepository = mobjAuthorContractRepository;
            _mobjLicenseeMasterRepository = mobjLicenseeMasterRepository;
            _mobjGeographicalMasterRepository = mobjGeographicalMasterRepository;
            _mobjCopyRightHolderMasterRepository = mobjCopyRightHolderMasterRepository;
        }
        #endregion

        #region Methods
        //Added by Ankush on 15/07/2016 Series List
        #region SeriesList

        public ActionResult SeriesList()
        {
            if (_workContext.CurrentUser == null || Session["UserId"]==null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                return View();
            }
        }

        public IList<SLV.Model.Common.SeriesByDividionSubdivisionModel> GetAllSeries()
        {

            var mvarSeriesList = (from S in _mobjSeriesMasterRepository.Table.Where(a => a.Deactivate == "N")

                                  join division in _mobjDivisionMasterRepository.Table.Where(a => a.Deactivate == "N")
                                     on S.divisionid equals division.Id into divisionGroup
                                  from D in divisionGroup.DefaultIfEmpty()

                                  join subdivision in _mobjDivisionMasterRepository.Table.Where(a => a.Deactivate == "N")
                                     on S.Subdivisionid equals subdivision.Id into subdivisionGroup
                                  from Su in subdivisionGroup.DefaultIfEmpty()

                                  select new SLV.Model.Common.SeriesByDividionSubdivisionModel
                                  {
                                      Id = S.Id,
                                      DivisionName = D.divisionName,
                                      SubdivisionName = Su.divisionName,
                                      SeriesName = S.Seriesname,
                                  }).ToList();

            return mvarSeriesList;

            //var mvarSeriesList = _dbContext.ExecuteStoredProcedureListNewData<SLV.Model.Common.SeriesByDividionSubdivisionModel>("Proc_Get_SeriesByDivisionSubdivision").ToList();
            //return mvarSeriesList;
        }


        public ActionResult exportToExcelSeriesList()
        {
            try
            {
                var _mobjReportList = GetAllSeries().Distinct();

                string SearchParameter = string.Empty;


                StringBuilder mstr_searchparameter = new StringBuilder();
                mstr_searchparameter.Append("<table width='100%'>");
                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'>" + "<b>Series List</b>" + "</td>");
                mstr_searchparameter.Append("</tr>");

                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td  style='width: 50%;' valign='top' align=left >" + "<b>Number of Records:</b> " + _mobjReportList.Count() + "</td>");
                mstr_searchparameter.Append("<td  style='width: 50%;' valign='top' align=right >" + "<b>List Created Date:</b> " + String.Format("{0:dd MMM yy HH:mm:ss}", DateTime.Now) + "</td>");
                mstr_searchparameter.Append("</tr>");


                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'></td>");
                mstr_searchparameter.Append("</tr>");

                mstr_searchparameter.Append("</table>");
                mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td colspan='2'>");
                mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td><b>SNo.</b></td>");
                mstr_searchparameter.Append("<td ><b>Division Name</b></td>");
                mstr_searchparameter.Append("<td ><b>Subdivision Name</b></td>");
                mstr_searchparameter.Append("<td ><b>Series Name</b></td>");

                mstr_searchparameter.Append("</tr>");
                mstr_searchparameter.Append("</td>");
                int mint_Counter = 1;
                foreach (var data in _mobjReportList)
                {
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td align='right'>" + mint_Counter + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.DivisionName + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.SubdivisionName + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.SeriesName + "</td>");
                    mstr_searchparameter.Append("</tr>");
                    mint_Counter++;
                }

                mstr_searchparameter.Append("</table></td></tr></table>");

                string sFileName = "SeriesList_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls";

                HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
                this.Response.ContentType = "application/excel";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(mstr_searchparameter.ToString());
                return File(buffer, "application/vnd.ms-excel");
            }
            catch (Exception ex)
            {

                return null;
                throw ex;
            }
        }

        #endregion

        //Added by Ankush on 18/07/2016 Publishing Company List
        #region PublishingCompanyList

        public ActionResult PublishingCompanyList()
        {
            if (_workContext.CurrentUser == null || Session["UserId"]==null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                return View();
            }
        }


        public IList<SLV.Model.Common.PublishingCompanyModel> GetAllPublishingCompany()
        {
            IList<SLV.Model.Common.PublishingCompanyModel> list = _publishingCompanyService.GetAllPublishingCompany().ToList().Select(i => new SLV.Model.Common.PublishingCompanyModel
            {
                CompanyName = i.CompanyName,
                ContactPerson = i.ContactPerson,
                Website = i.Website,
                Email = i.Email,
                Mobile = i.Mobile,
                Phone = i.Phone,
                CountryName = i.PublishingCompanyCountry.geogName,
                StateName = i.PublishingCompanyState.geogName,
                CityName = i.PublishingCompanyCity.geogName
            }).ToList();

            return list;
        }

        public ActionResult exportToExcelPublishingCompanyList()
        {
            try
            {
                var _mobjReportList = GetAllPublishingCompany();

                string SearchParameter = string.Empty;


                StringBuilder mstr_searchparameter = new StringBuilder();
                mstr_searchparameter.Append("<table width='100%'>");
                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'>" + "<b>Proprietor Company List</b>" + "</td>");
                mstr_searchparameter.Append("</tr>");

                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td  style='width: 50%;' valign='top' align=left >" + "<b>Number of Records:</b> " + _mobjReportList.Count() + "</td>");
                mstr_searchparameter.Append("<td  style='width: 50%;' valign='top' align=right >" + "<b>List Created Date:</b> " + String.Format("{0:dd MMM yy HH:mm:ss}", DateTime.Now) + "</td>");
                mstr_searchparameter.Append("</tr>");


                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'></td>");
                mstr_searchparameter.Append("</tr>");

                mstr_searchparameter.Append("</table>");
                mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td colspan='2'>");
                mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td><b>SNo.</b></td>");
                mstr_searchparameter.Append("<td ><b>Company Name</b></td>");
                mstr_searchparameter.Append("<td ><b>Contact Person</b></td>");
                mstr_searchparameter.Append("<td ><b>Country</b></td>");
                mstr_searchparameter.Append("<td ><b>State</b></td>");
                mstr_searchparameter.Append("<td ><b>City</b></td>");
                mstr_searchparameter.Append("<td ><b>Phone</b></td>");
                mstr_searchparameter.Append("<td ><b>Mobile</b></td>");
                mstr_searchparameter.Append("<td ><b>Email</b></td>");
                mstr_searchparameter.Append("<td ><b>Website</b></td>");

                mstr_searchparameter.Append("</tr>");
                mstr_searchparameter.Append("</td>");
                int mint_Counter = 1;
                foreach (var data in _mobjReportList)
                {
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td align='right'>" + mint_Counter + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.CompanyName + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.ContactPerson + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.CountryName + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.StateName + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.CityName + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.Phone + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.Mobile + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.Email + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.Website + "</td>");
                    mstr_searchparameter.Append("</tr>");
                    mint_Counter++;
                }

                mstr_searchparameter.Append("</table></td></tr></table>");

                string sFileName = "ProprietorCompanyList_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls";

                HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
                this.Response.ContentType = "application/excel";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(mstr_searchparameter.ToString());
                return File(buffer, "application/vnd.ms-excel");
            }
            catch (Exception ex)
            {

                return null;
                throw ex;
            }
        }

        #endregion

        //Added by Ankush on 18/07/2016 Pub Center List
        #region Pub Center List

        public ActionResult PubCenterList()
        {
            if (_workContext.CurrentUser == null || Session["UserId"]==null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                return View();
            }
        }

        public IList<SLV.Model.Common.PubCenterWithPublishingCompanyModel> GetAllPubCenter()
        {

            var mvarPubCenterList = _dbContext.ExecuteStoredProcedureListNewData<SLV.Model.Common.PubCenterWithPublishingCompanyModel>("Proc_Get_PubCenterWithPublishingCompany").ToList();
            return mvarPubCenterList;
        }


        public ActionResult exportToExcelPubCenterList()
        {
            try
            {
                var _mobjReportList = GetAllPubCenter();

                string SearchParameter = string.Empty;


                StringBuilder mstr_searchparameter = new StringBuilder();
                mstr_searchparameter.Append("<table width='100%'>");
                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'>" + "<b>Pub Center List</b>" + "</td>");
                mstr_searchparameter.Append("</tr>");

                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td  style='width: 50%;' valign='top' align=left >" + "<b>Number of Records:</b> " + _mobjReportList.Count() + "</td>");
                mstr_searchparameter.Append("<td  style='width: 50%;' valign='top' align=right >" + "<b>List Created Date:</b> " + String.Format("{0:dd MMM yy HH:mm:ss}", DateTime.Now) + "</td>");
                mstr_searchparameter.Append("</tr>");


                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'></td>");
                mstr_searchparameter.Append("</tr>");

                mstr_searchparameter.Append("</table>");
                mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td colspan='2'>");
                mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td><b>SNo.</b></td>");
                mstr_searchparameter.Append("<td ><b>Publishing Company</b></td>");
                mstr_searchparameter.Append("<td ><b>Center Name</b></td>");
                mstr_searchparameter.Append("<td ><b>Country</b></td>");
                mstr_searchparameter.Append("<td ><b>State</b></td>");
                mstr_searchparameter.Append("<td ><b>City</b></td>");
                mstr_searchparameter.Append("<td ><b>Phone</b></td>");
                mstr_searchparameter.Append("<td ><b>Mobile</b></td>");
                mstr_searchparameter.Append("<td ><b>Fax</b></td>");
                mstr_searchparameter.Append("<td ><b>Email</b></td>");

                mstr_searchparameter.Append("</tr>");
                mstr_searchparameter.Append("</td>");
                int mint_Counter = 1;
                foreach (var data in _mobjReportList)
                {
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td align='right'>" + mint_Counter + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.PublishingCompanyName + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.CenterName + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.CountryName + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.StateName + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.CityName + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.Phone + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.Mobile + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.Fax + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.Email + "</td>");

                    mstr_searchparameter.Append("</tr>");
                    mint_Counter++;
                }

                mstr_searchparameter.Append("</table></td></tr></table>");

                string sFileName = "PubCenterList_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls";

                HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
                this.Response.ContentType = "application/excel";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(mstr_searchparameter.ToString());
                return File(buffer, "application/vnd.ms-excel");
            }
            catch (Exception ex)
            {

                return null;
                throw ex;
            }
        }

        #endregion Pub Center List

        //Added by Ankush on 18/07/2016 Licensee List
        #region Licensee List

        public ActionResult LicenseeList()
        {
            if (_workContext.CurrentUser == null || Session["UserId"]==null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                return View();
            }
        }

        public IList<SLV.Model.Common.LicenseeModel> GetAllLicenseeList()
        {

            var mvarLicenseeList = (from L in _mobjLicenseeMasterRepository.Table.Where(a => a.Deactivate == "N")

                                    join Country in _mobjGeographicalMasterRepository.Table.Where(a => a.Deactivate == "N")
                                       on L.CountryId equals Country.Id into countryGroup
                                    from c in countryGroup.DefaultIfEmpty()

                                    join State in _mobjGeographicalMasterRepository.Table.Where(a => a.Deactivate == "N")
                                       on L.Stateid equals State.Id into stateGroup
                                    from s in stateGroup.DefaultIfEmpty()

                                    join City in _mobjGeographicalMasterRepository.Table.Where(a => a.Deactivate == "N")
                                       on L.Cityid equals City.Id into cityGroup
                                    from t in cityGroup.DefaultIfEmpty()

                                    select new SLV.Model.Common.LicenseeModel
                                    {
                                        OrganizationName = L.OrganizationName,
                                        ContactPerson = L.ContactPerson,
                                        Address = L.Address,
                                        CountryName = c.geogName,
                                        StateName = s.geogName,
                                        CityName = t.geogName,
                                        Mobile = L.Mobile,
                                        Email = L.Email,
                                        URL = L.URL,
                                    }).ToList();

            return mvarLicenseeList;

            //var mvarLicenseeList = _dbContext.ExecuteStoredProcedureListNewData<SLV.Model.Common.LicenseeModel>("Proc_Get_LicenseeList").ToList();
            //return mvarLicenseeList;
        }

        public ActionResult exportToExcelLicenseeList()
        {
            try
            {
                var _mobjReportList = GetAllLicenseeList().Distinct();

                string SearchParameter = string.Empty;


                StringBuilder mstr_searchparameter = new StringBuilder();
                mstr_searchparameter.Append("<table width='100%'>");
                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'>" + "<b>Licensee List</b>" + "</td>");
                mstr_searchparameter.Append("</tr>");

                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td  style='width: 50%;' valign='top' align=left >" + "<b>Number of Records:</b> " + _mobjReportList.Count() + "</td>");
                mstr_searchparameter.Append("<td  style='width: 50%;' valign='top' align=right >" + "<b>List Created Date:</b> " + String.Format("{0:dd MMM yy HH:mm:ss}", DateTime.Now) + "</td>");
                mstr_searchparameter.Append("</tr>");


                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'></td>");
                mstr_searchparameter.Append("</tr>");

                mstr_searchparameter.Append("</table>");
                mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td colspan='2'>");
                mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td><b>SNo.</b></td>");
                mstr_searchparameter.Append("<td ><b>Organiasation Name</b></td>");
                mstr_searchparameter.Append("<td ><b>Contact Person</b></td>");
                mstr_searchparameter.Append("<td ><b>Address</b></td>");
                mstr_searchparameter.Append("<td ><b>Country</b></td>");
                mstr_searchparameter.Append("<td ><b>State</b></td>");
                mstr_searchparameter.Append("<td ><b>City</b></td>");
                mstr_searchparameter.Append("<td ><b>Mobile</b></td>");
                mstr_searchparameter.Append("<td ><b>Email</b></td>");
                mstr_searchparameter.Append("<td ><b>URL</b></td>");

                mstr_searchparameter.Append("</tr>");
                mstr_searchparameter.Append("</td>");
                int mint_Counter = 1;
                foreach (var data in _mobjReportList)
                {
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td align='right'>" + mint_Counter + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.OrganizationName + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.ContactPerson + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.Address + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.CountryName + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.StateName + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.CityName + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.Mobile + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.Email + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.URL + "</td>");

                    mstr_searchparameter.Append("</tr>");
                    mint_Counter++;
                }

                mstr_searchparameter.Append("</table></td></tr></table>");

                string sFileName = "LicenseeList_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls";

                HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
                this.Response.ContentType = "application/excel";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(mstr_searchparameter.ToString());
                return File(buffer, "application/vnd.ms-excel");
            }
            catch (Exception ex)
            {

                return null;
                throw ex;
            }
        }

        #endregion Licensee List


        //Added by Ankush on 19/07/2016 Copyright Holder List
        #region Copyright Holder List

        public ActionResult CopyrightHolderList()
        {
            if (_workContext.CurrentUser == null || Session["UserId"]==null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                return View();
            }
        }

        public IList<SLV.Model.Common.CopyRightHolderModel> GetAllCopyrightHolderList()
        {

            var mvarCopyrightHolderList = (from CR in _mobjCopyRightHolderMasterRepository.Table.Where(a => a.Deactivate == "N")

                                           join Country in _mobjGeographicalMasterRepository.Table.Where(a => a.Deactivate == "N")
                                              on CR.CountryId equals Country.Id into countryGroup
                                           from c in countryGroup.DefaultIfEmpty()

                                           join State in _mobjGeographicalMasterRepository.Table.Where(a => a.Deactivate == "N")
                                              on CR.Stateid equals State.Id into stateGroup
                                           from s in stateGroup.DefaultIfEmpty()

                                           join City in _mobjGeographicalMasterRepository.Table.Where(a => a.Deactivate == "N")
                                              on CR.Cityid equals City.Id into cityGroup
                                           from t in cityGroup.DefaultIfEmpty()

                                           select new SLV.Model.Common.CopyRightHolderModel
                                           {
                                               CopyRightHolderName = CR.CopyRightHolderName,
                                               ContactPerson = CR.ContactPerson,
                                               Address = CR.Address,
                                               CountryName = c.geogName,
                                               StateName = s.geogName,
                                               CityName = t.geogName,
                                               Mobile = CR.Mobile,
                                               Email = CR.Email,
                                               URL = CR.URL,
                                               BankName = CR.BankName,
                                               AccountNo = CR.AccountNo,
                                               BankAddress = CR.BankAddress,
                                               IFSCCode = CR.IFSCCode,
                                               PANNo = CR.PANNo,
                                               VendorCOde = CR.VendorCOde,
                                           }).ToList();

            return mvarCopyrightHolderList;

            //var mvarCopyrightHolderList = _dbContext.ExecuteStoredProcedureListNewData<SLV.Model.Common.CopyRightHolderModel>("Proc_Get_CopyrightHolderList").ToList();
            //return mvarCopyrightHolderList;
        }

        public ActionResult exportToExcelCopyrightHolderList()
        {
            try
            {
                var _mobjReportList = GetAllCopyrightHolderList().Distinct();

                string SearchParameter = string.Empty;


                StringBuilder mstr_searchparameter = new StringBuilder();
                mstr_searchparameter.Append("<table width='100%'>");
                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'>" + "<b>Copyright Holder List</b>" + "</td>");
                mstr_searchparameter.Append("</tr>");

                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td  style='width: 50%;' valign='top' align=left >" + "<b>Number of Records:</b> " + _mobjReportList.Count() + "</td>");
                mstr_searchparameter.Append("<td  style='width: 50%;' valign='top' align=right >" + "<b>List Created Date:</b> " + String.Format("{0:dd MMM yy HH:mm:ss}", DateTime.Now) + "</td>");
                mstr_searchparameter.Append("</tr>");


                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'></td>");
                mstr_searchparameter.Append("</tr>");

                mstr_searchparameter.Append("</table>");
                mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td colspan='2'>");
                mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td><b>SNo.</b></td>");
                mstr_searchparameter.Append("<td ><b>Copyright Holder Name</b></td>");
                mstr_searchparameter.Append("<td ><b>Contact Person</b></td>");
                mstr_searchparameter.Append("<td ><b>Country</b></td>");
                mstr_searchparameter.Append("<td ><b>State</b></td>");
                mstr_searchparameter.Append("<td ><b>City</b></td>");
                mstr_searchparameter.Append("<td ><b>Mobile</b></td>");
                mstr_searchparameter.Append("<td ><b>Email</b></td>");
                mstr_searchparameter.Append("<td ><b>URL</b></td>");

                mstr_searchparameter.Append("<td ><b>Bank Name</b></td>");
                mstr_searchparameter.Append("<td ><b>Account No.</b></td>");
                mstr_searchparameter.Append("<td ><b>Bank Address</b></td>");
                mstr_searchparameter.Append("<td ><b>IFSC Code</b></td>");
                mstr_searchparameter.Append("<td ><b>PAN</b></td>");
                mstr_searchparameter.Append("<td ><b>Vendor Code</b></td>");

                mstr_searchparameter.Append("</tr>");
                mstr_searchparameter.Append("</td>");
                int mint_Counter = 1;
                foreach (var data in _mobjReportList)
                {
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td align='right'>" + mint_Counter + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.CopyRightHolderName + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.ContactPerson + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.CountryName + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.StateName + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.CityName + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.Mobile + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.Email + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.URL + "</td>");

                    mstr_searchparameter.Append("<td align='left'>" + data.BankName + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.AccountNo + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.BankAddress + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.IFSCCode + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.PANNo + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.VendorCOde + "</td>");

                    mstr_searchparameter.Append("</tr>");
                    mint_Counter++;
                }

                mstr_searchparameter.Append("</table></td></tr></table>");

                string sFileName = "CopyrightHolderList_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls";

                HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
                this.Response.ContentType = "application/excel";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(mstr_searchparameter.ToString());
                return File(buffer, "application/vnd.ms-excel");
            }
            catch (Exception ex)
            {

                return null;
                throw ex;
            }
        }

        #endregion Copyright Holder List

        #endregion
    }
}