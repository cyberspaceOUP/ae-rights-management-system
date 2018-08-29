//Added by Prakash on 20/09/2016
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ACS.Core.Domain.Master;
using ACS.Services.Logging;
using ACS.Services.Localization;
using ACS.Services.RightsSelling;
using ACS.Core;
using SLV.Model.Common;
using Autofac.Integration.WebApi;
using SLV.API.Controllers.JsonSerializer;
using ACS.Core.Infrastructure;
using System.Data;

using System.Data.SqlClient;
using ACS.Data;
using ACS.Core.Data;
using ACS.Core.Domain.Product;
using ACS.Services.Master;
using System.Web.Script.Serialization;
using ACS.Services.User;
using SLV.Model.Report;



namespace SLV.API.Controllers.Report
{
    public class ReportController : ApiController
    {
        private readonly IDbContext _dbContext;

        public ReportController(

                IDbContext dbContext
            , IRepository<LicenseeMaster> LicenseeMaster
            , IRightsSelling IRightsSelling
            , IRepository<ApplicationSetUp> ApplicationSetUp
            , IApplicationSetUpService ApplicationSetUpService

            )
        {
            this._dbContext = dbContext;
        }



        //Added by Prakash 
        //Rights - Get Author / Publisher Statement Search  on 20/09/2016
        [HttpPost]
        public IHttpActionResult GetRightsStatementList(StatementModel _AuthorPubStModel)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[11];

                //parameters[0] = new SqlParameter("Year", SqlDbType.VarChar, 4);
                //parameters[0].Value = "'" + _AuthorPubStModel.Year + "'";
                //parameters[1] = new SqlParameter("Month", SqlDbType.VarChar, 2);
                //parameters[1].Value = "'" + _AuthorPubStModel.Month + "'";

                parameters[0] = new SqlParameter("EntryFromYear", SqlDbType.VarChar, 4);
                parameters[0].Value = "'" + _AuthorPubStModel.EntryFromYear + "'";
                parameters[1] = new SqlParameter("EntryFromMonth", SqlDbType.VarChar, 2);
                parameters[1].Value = "'" + _AuthorPubStModel.EntryFromMonth + "'";
                parameters[2] = new SqlParameter("EntryToYear", SqlDbType.VarChar, 4);
                parameters[2].Value = "'" + _AuthorPubStModel.EntryToYear + "'";
                parameters[3] = new SqlParameter("EntryToMonth", SqlDbType.VarChar, 2);
                parameters[3].Value = "'" + _AuthorPubStModel.EntryToMonth + "'";

                parameters[4] = new SqlParameter("FromYear", SqlDbType.VarChar, 4);
                parameters[4].Value = "'" + _AuthorPubStModel.FromYear + "'";
                parameters[5] = new SqlParameter("FromMonth", SqlDbType.VarChar, 2);
                parameters[5].Value = "'" + _AuthorPubStModel.FromMonth + "'";
                parameters[6] = new SqlParameter("ToYear", SqlDbType.VarChar, 4);
                parameters[6].Value = "'" + _AuthorPubStModel.ToYear + "'";
                parameters[7] = new SqlParameter("ToMonth", SqlDbType.VarChar, 2);
                parameters[7].Value = "'" + _AuthorPubStModel.ToMonth + "'";


                //if ((_AuthorPubStModel.AuthorName != null && _AuthorPubStModel.AuthorName != "") ||
                //    (_AuthorPubStModel.AuthorCode != null && _AuthorPubStModel.AuthorCode != "") ||
                //    (_AuthorPubStModel.AuthorContractCode != null && _AuthorPubStModel.AuthorContractCode != ""))
                if (_AuthorPubStModel.Type != null && _AuthorPubStModel.Type != "" && _AuthorPubStModel.Type.ToLower() == "authorcontractcode")
                {
                    parameters[8] = new SqlParameter("AuthorContractCode", SqlDbType.VarChar, 200);
                    parameters[8].Value = "'" + _AuthorPubStModel.AuthorContractCode + "'";
                    parameters[9] = new SqlParameter("AuthorCode", SqlDbType.VarChar, 200);
                    parameters[9].Value = "'" + _AuthorPubStModel.AuthorCode + "'";
                    parameters[10] = new SqlParameter("AuthorName", SqlDbType.VarChar, 200);
                    parameters[10].Value = "'" + _AuthorPubStModel.AuthorName + "'";
                    var _GetAuthorStatement = _dbContext.ExecuteStoredProcedureListNewData<StatementModel>("Proc_Statement_Search_RS_A_get", parameters).ToList();

                    return Json(_GetAuthorStatement);
                }
                else
                {
                    parameters[8] = new SqlParameter("ProductLicenseCode", SqlDbType.VarChar, 200);
                    parameters[8].Value = "'" + _AuthorPubStModel.ProductLicenseCode + "'";
                    parameters[9] = new SqlParameter("PublishingCompanyCode", SqlDbType.VarChar, 200);
                    parameters[9].Value = "'" + _AuthorPubStModel.PublishingCompanyCode + "'";
                    parameters[10] = new SqlParameter("PublishingCompanyName", SqlDbType.VarChar, 200);
                    parameters[10].Value = "'" + _AuthorPubStModel.PublishingCompanyName + "'";
                    var _GetStatement = _dbContext.ExecuteStoredProcedureListNewData<StatementModel>("Proc_Statement_Search_RS_P_get", parameters).ToList();

                    return Json(_GetStatement);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //Rights - Get Author / Publisher Statement Detail on 20/09/2016
        [HttpPost]
        public IHttpActionResult GetRightsStatementDetail(StatementModel _AuthorPubStModel)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];

                if ((_AuthorPubStModel.ContractId != null && _AuthorPubStModel.ContractId != "") || 
                    (_AuthorPubStModel.AuthorId != null && _AuthorPubStModel.AuthorId != 0))
                {
                    parameters[0] = new SqlParameter("ContractId", SqlDbType.VarChar, 50);
                    parameters[0].Value = "'" + _AuthorPubStModel.ContractId + "'";
                    parameters[1] = new SqlParameter("AuthorId", SqlDbType.VarChar, 50);
                    parameters[1].Value = "'" + _AuthorPubStModel.AuthorId + "'";
                    var _GetAuthorStatement_Detail = _dbContext.ExecuteStoredProcedureListNewData<StatementModel>("Proc_Statement_DetaiList_RS_A_get", parameters).ToList();

                    return Json(_GetAuthorStatement_Detail);
                }
                else
                {
                    parameters[0] = new SqlParameter("LicenseId", SqlDbType.VarChar, 50);
                    parameters[0].Value = "'" + _AuthorPubStModel.LicenseId + "'";
                    parameters[1] = new SqlParameter("PublishingCompanyId", SqlDbType.VarChar, 50);
                    parameters[1].Value = "'" + _AuthorPubStModel.PublishingCompanyId + "'";
                    var _GetStatement_Detail = _dbContext.ExecuteStoredProcedureListNewData<StatementModel>("Proc_Statement_DetaiList_RS_P_get", parameters).ToList();

                    return Json(_GetStatement_Detail);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        //PermissionsOutbound - Get Author / Publisher Statement Search  on 22/09/2016
        [HttpPost]
        public IHttpActionResult GetPermissionsOutboundStatementList(StatementModel _AuthorPubStModel)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[11];

                //parameters[0] = new SqlParameter("Year", SqlDbType.VarChar, 4);
                //parameters[0].Value = "'" + _AuthorPubStModel.Year + "'";
                //parameters[1] = new SqlParameter("Month", SqlDbType.VarChar, 2);
                //parameters[1].Value = "'" + _AuthorPubStModel.Month + "'";

                parameters[0] = new SqlParameter("EntryFromYear", SqlDbType.VarChar, 4);
                parameters[0].Value = "'" + _AuthorPubStModel.EntryFromYear + "'";
                parameters[1] = new SqlParameter("EntryFromMonth", SqlDbType.VarChar, 2);
                parameters[1].Value = "'" + _AuthorPubStModel.EntryFromMonth + "'";
                parameters[2] = new SqlParameter("EntryToYear", SqlDbType.VarChar, 4);
                parameters[2].Value = "'" + _AuthorPubStModel.EntryToYear + "'";
                parameters[3] = new SqlParameter("EntryToMonth", SqlDbType.VarChar, 2);
                parameters[3].Value = "'" + _AuthorPubStModel.EntryToMonth + "'";

                parameters[4] = new SqlParameter("FromYear", SqlDbType.VarChar, 4);
                parameters[4].Value = "'" + _AuthorPubStModel.FromYear + "'";
                parameters[5] = new SqlParameter("FromMonth", SqlDbType.VarChar, 2);
                parameters[5].Value = "'" + _AuthorPubStModel.FromMonth + "'";
                parameters[6] = new SqlParameter("ToYear", SqlDbType.VarChar, 4);
                parameters[6].Value = "'" + _AuthorPubStModel.ToYear + "'";
                parameters[7] = new SqlParameter("ToMonth", SqlDbType.VarChar, 2);
                parameters[7].Value = "'" + _AuthorPubStModel.ToMonth + "'";


                //if ((_AuthorPubStModel.AuthorName != null && _AuthorPubStModel.AuthorName != "") ||
                //    (_AuthorPubStModel.AuthorCode != null && _AuthorPubStModel.AuthorCode != "") ||
                //    (_AuthorPubStModel.AuthorContractCode != null && _AuthorPubStModel.AuthorContractCode != ""))
                if (_AuthorPubStModel.Type != null && _AuthorPubStModel.Type != "" && _AuthorPubStModel.Type.ToLower() == "authorcontractcode")
                {
                    parameters[8] = new SqlParameter("AuthorContractCode", SqlDbType.VarChar, 200);
                    parameters[8].Value = "'" + _AuthorPubStModel.AuthorContractCode + "'";
                    parameters[9] = new SqlParameter("AuthorCode", SqlDbType.VarChar, 200);
                    parameters[9].Value = "'" + _AuthorPubStModel.AuthorCode + "'";
                    parameters[10] = new SqlParameter("AuthorName", SqlDbType.VarChar, 200);
                    parameters[10].Value = "'" + _AuthorPubStModel.AuthorName + "'";
                    var _GetAuthorStatement = _dbContext.ExecuteStoredProcedureListNewData<StatementModel>("Proc_Statement_Search_PO_A_get", parameters).ToList();

                    return Json(_GetAuthorStatement);
                }
                else
                {
                    parameters[8] = new SqlParameter("ProductLicenseCode", SqlDbType.VarChar, 200);
                    parameters[8].Value = "'" + _AuthorPubStModel.ProductLicenseCode + "'";
                    parameters[9] = new SqlParameter("PublishingCompanyCode", SqlDbType.VarChar, 200);
                    parameters[9].Value = "'" + _AuthorPubStModel.PublishingCompanyCode + "'";
                    parameters[10] = new SqlParameter("PublishingCompanyName", SqlDbType.VarChar, 200);
                    parameters[10].Value = "'" + _AuthorPubStModel.PublishingCompanyName + "'";
                    var _GetStatement = _dbContext.ExecuteStoredProcedureListNewData<StatementModel>("Proc_Statement_Search_PO_P_get", parameters).ToList();

                    return Json(_GetStatement);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //PermissionsOutbound - Get Author / Publisher Statement Detail on 23/09/2016
        [HttpPost]
        public IHttpActionResult GetPermissionsOutboundStatementDetail(StatementModel _AuthorPubStModel)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];

                if ((_AuthorPubStModel.ContractId != null && _AuthorPubStModel.ContractId != "") || (_AuthorPubStModel.AuthorId != null && _AuthorPubStModel.AuthorId != 0))
                {
                    parameters[0] = new SqlParameter("ContractId", SqlDbType.VarChar, 50);
                    parameters[0].Value = "'" + _AuthorPubStModel.ContractId + "'";
                    parameters[1] = new SqlParameter("AuthorId", SqlDbType.VarChar, 50);
                    parameters[1].Value = "'" + _AuthorPubStModel.AuthorId + "'";
                    var _GetAuthorStatement_Detail = _dbContext.ExecuteStoredProcedureListNewData<StatementModel>("Proc_Statement_DetaiList_PO_A_get", parameters).ToList();

                    return Json(_GetAuthorStatement_Detail);
                }
                else
                {
                    parameters[0] = new SqlParameter("LicenseId", SqlDbType.VarChar, 50);
                    parameters[0].Value = "'" + _AuthorPubStModel.LicenseId + "'";
                    parameters[1] = new SqlParameter("PublishingCompanyId", SqlDbType.VarChar, 50);
                    parameters[1].Value = "'" + _AuthorPubStModel.PublishingCompanyId + "'";
                    var _GetStatement_Detail = _dbContext.ExecuteStoredProcedureListNewData<StatementModel>("Proc_Statement_DetaiList_PO_P_get", parameters).ToList();

                    return Json(_GetStatement_Detail);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //Get InvoiceReport Search / List on 23/09/2016
        [HttpPost]
        public IHttpActionResult GetInvoiceReportList(InvoiceReportModel _InvoiceRModel)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[9];

                parameters[0] = new SqlParameter("InvoiceFromDate", SqlDbType.VarChar, 200);
                parameters[0].Value = "'" + _InvoiceRModel.InvoiceFromDate + "'";
                parameters[1] = new SqlParameter("InvoiceToDate", SqlDbType.VarChar, 200);
                parameters[1].Value = "'" + _InvoiceRModel.InvoiceToDate + "'";
                parameters[2] = new SqlParameter("InvoiceNo", SqlDbType.VarChar, 200);
                parameters[2].Value = "'" + _InvoiceRModel.InvoiceNo + "'";
                parameters[3] = new SqlParameter("InvoiceValue", SqlDbType.VarChar, 200);
                parameters[3].Value = "'" + _InvoiceRModel.InvoiceValue + "'";
                parameters[4] = new SqlParameter("InvoiceStatus", SqlDbType.VarChar, 200);
                parameters[4].Value = "'" + _InvoiceRModel.InvoiceStatus + "'";
                parameters[5] = new SqlParameter("LicenseeName", SqlDbType.VarChar, 200);
                parameters[5].Value = "'" + _InvoiceRModel.LicenseeName + "'";
                parameters[6] = new SqlParameter("Country", SqlDbType.VarChar, 200);
                parameters[6].Value = "'" + _InvoiceRModel.Country + "'";
                parameters[7] = new SqlParameter("State", SqlDbType.VarChar, 200);
                parameters[7].Value = "'" + _InvoiceRModel.State + "'";
                parameters[8] = new SqlParameter("City", SqlDbType.VarChar, 200);
                parameters[8].Value = "'" + _InvoiceRModel.City + "'";
                var _GetInvoiceReport_Detail = _dbContext.ExecuteStoredProcedureListNewData<InvoiceReportModel>("Proc_InvoiceReport_get", parameters).ToList();

                return Json(_GetInvoiceReport_Detail);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //Get LicenseeName For InvoiceReport List on 26/09/2016
        [HttpPost]
        public IHttpActionResult GetLicenseeNameList(InvoiceReportModel _InvoiceRModel)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];

                parameters[0] = new SqlParameter("LicenseeName", SqlDbType.VarChar, 200);
                parameters[0].Value = "'" + _InvoiceRModel.LicenseeName + "'";
                var _GetLicenseeName = _dbContext.ExecuteStoredProcedureListNewData<InvoiceReportModel>("Proc_LicenseeName_ForInvoiceReport_get", parameters).ToList();

                return Json(_GetLicenseeName);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        //-------------Get Detail list of Product licenses which will be expiring within 3 months or for which balance quantity is less than 25%  on 25/10/2016
        [HttpPost]
        public IHttpActionResult GetProductLicenseExpired(DashBoardModel Flag)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("Flag", SqlDbType.VarChar, 200);
                parameters[0].Value = "'" + Flag.Flag + "'";
                parameters[1] = new SqlParameter("ExecutiveId", SqlDbType.VarChar, 200);
                parameters[1].Value = "'" + Flag.ExecutiveId + "'"; ;

                var _GetProductLicenseExpired = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_ProductLicenseExpired_get", parameters).ToList();
                return Json(_GetProductLicenseExpired);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //-------------Get Detail list of Product License Addendums which will be expiring within 3 months or for which balance quantity is less than 25% on 25/10/2016
        [HttpPost]
        public IHttpActionResult GetProductLicenseAddendumExpired(DashBoardModel Flag)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("Flag", SqlDbType.VarChar, 200);
                parameters[0].Value = "'" + Flag.Flag + "'";

                var _GetProductLicenseExpired = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_ProductLicenseAddendumExpired_get", parameters).ToList();
                return Json(_GetProductLicenseExpired);
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        //Get Author Contract Expiry Report / Dated on 25/10/2016 Added By Ankush 
        [HttpPost]
        public IHttpActionResult AuthorContractExpiryReportList(AuthorContractExpiryReportModel _AuthorContractExpiryReportModel)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[6];

                parameters[0] = new SqlParameter("ProductCode", SqlDbType.VarChar, 200);
                parameters[0].Value = "'" + _AuthorContractExpiryReportModel.ProductCode + "'";
                parameters[1] = new SqlParameter("ProductName", SqlDbType.VarChar, 200);
                parameters[1].Value = "'" + _AuthorContractExpiryReportModel.ProductName + "'";
                parameters[2] = new SqlParameter("ISBN", SqlDbType.VarChar, 200);
                parameters[2].Value = "'" + _AuthorContractExpiryReportModel.ISBN + "'";
                parameters[3] = new SqlParameter("Authors", SqlDbType.VarChar, 200);
                parameters[3].Value = "'" + _AuthorContractExpiryReportModel.Authors + "'";
                parameters[4] = new SqlParameter("ExpiryFromDate", SqlDbType.VarChar, 200);
                parameters[4].Value = "'" + _AuthorContractExpiryReportModel.ExpiryFromDate + "'";
                parameters[5] = new SqlParameter("ExpiryToDate", SqlDbType.VarChar, 200);
                parameters[5].Value = "'" + _AuthorContractExpiryReportModel.ExpiryToDate + "'";
                var _GetInvoiceReport_Detail = _dbContext.ExecuteStoredProcedureListNewData<AuthorContractExpiryReportModel>("Proc_AuthorContractExpiryReport_get", parameters).ToList();

                return Json(_GetInvoiceReport_Detail);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}