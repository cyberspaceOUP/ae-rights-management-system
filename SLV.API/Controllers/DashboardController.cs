using ACS.Core;
using ACS.Services.Authentication;
using ACS.Services.Contact;
using ACS.Services.Localization;
using ACS.Services.Master;
using ACS.Services.Product;
using ACS.Services.Security;
using ACS.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using ACS.Core.Domain.Product;
using ACS.Core.Domain.OtherContract;
using ACS.Services.Other_Contract;
using ACS.Data;
using ACS.Core;
using SLV.Model.Common;
using ACS.Services.AuthorContract;

namespace SLV.API.Controllers
{
    public class DashboardController : ApiController
    {
        private readonly IProductMasterService _ProductMasterService;
        private readonly IDbContext _dbContext;


        public DashboardController(

             IProductMasterService ProductMasterService
              , IDbContext dbContext
            )
        {
            _ProductMasterService = ProductMasterService;
            this._dbContext = dbContext;
        }


        [HttpGet]
        public IHttpActionResult getNotEnteredSAPAgreementNoList()
        {
            return Json(_ProductMasterService.NotEnteredSAPAgreementNoList().ToList());
        }

        [HttpGet]
        public IHttpActionResult getNotEnteredISBNForProductList()
        {
            return Json(_ProductMasterService.NotEnteredISBNForProductList().ToList());
        }

        //-----Modified by Prakash on 1/09/2016
        [HttpGet]
        public IHttpActionResult AuthorContractProduct(int ExecutiveId)
        {
            //return Json(_ProductMasterService.AuthorProductNotSigned());

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("ExecutiveId", SqlDbType.VarChar, 200);
            parameters[0].Value = "'" + ExecutiveId + "'";

            var _GetAuthorContractNotEntered = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_AuthorContractNotEntered_get", parameters).ToList();
            return Json(_GetAuthorContractNotEntered);
        }

        //-----Modified by Prakash on 30/08/2016
        [HttpGet]
        public IHttpActionResult ProductLicenseNotEntered(int ExecutiveId)
        {
            //return Json(_ProductMasterService.LicenceProductNotSigned());

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("ExecutiveId", SqlDbType.VarChar, 200);
            parameters[0].Value = "'" + ExecutiveId + "'";

            var _GetLicenceProductNotEntered = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_ProductLicenseNotEntered_get", parameters).ToList();
            return Json(_GetLicenceProductNotEntered);
        }


        [HttpGet]
        public IHttpActionResult PendingRequestOtherContract()
        {
            var _GetOtherContractSearch = _dbContext.ExecuteStoredProcedureListNewData<Contract_Signed_By_Executive>("Proc_Pending_Request_OtherContract_get").ToList();
            return Json(_GetOtherContractSearch);
        }


        public IHttpActionResult ProductLicenses()
        {

            var _GetProductLicensesSearch = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_ProductLicenses_Desh_get").ToList();


            return Json(_GetProductLicensesSearch);

        }


        public IHttpActionResult ProductLicensesAddendums()
        {

            var _GetProductLicensesAddendumsSearch = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_ProductLicensesAddendums_Dash_get").ToList();


            return Json(_GetProductLicensesAddendumsSearch);

        }

        public IHttpActionResult Product_ISBN_entered(int ExecutiveId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("ExecutiveId", SqlDbType.VarChar, 200);
            parameters[0].Value = "'" + ExecutiveId + "'";

            var _GetProductLicensesAddendumsSearch = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_Product_ISBN_entered_Dash_get", parameters).ToList();
            return Json(_GetProductLicensesAddendumsSearch);
        }

        public IHttpActionResult Product_SAP_Agr_No_Not_Entered(int ExecutiveId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("ExecutiveId", SqlDbType.VarChar, 200);
            parameters[0].Value = "'" + ExecutiveId + "'";

            var _GetProductLicensesAddendumsSearch = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_Product_SAP_Agr_No_Not_Entered_Desh_get", parameters).ToList();
            return Json(_GetProductLicensesAddendumsSearch);
        }

        //Added by Suranjana on 21/07/2016
        public IHttpActionResult Product_ISBN_Is_Not_Null(int ExecutiveId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("ExecutiveId", SqlDbType.VarChar, 200);
            parameters[0].Value = "'" + ExecutiveId + "'";

            var _GetProductForIsbnEnteredSearch = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_Product_ISBN_Entered_Is_NOT_Null_Dash_get", parameters).ToList();
            return Json(_GetProductForIsbnEnteredSearch);
        }

        public IHttpActionResult AuthorContractRequestStatus()
        {
            var _GetAuthorContractRequestStatus = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_Author_Contract_Request_Status_get").ToList();

            return Json(_GetAuthorContractRequestStatus);
        }
        //Ended by Suranjana

        //-----Added by Prakash 
        //-------------Get Detail list of Pending Author Contract Request on 30/08/2016
        public IHttpActionResult PendingAuthorContractRequest(int ExecutiveId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("ExecutiveId", SqlDbType.VarChar, 200);
            parameters[0].Value = "'" + ExecutiveId + "'";

            var _GetPendingAuthorContractRequest = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_PendingAuthorContractRequest_get", parameters).ToList();
            return Json(_GetPendingAuthorContractRequest);
        }

        //-------------Get Detail list of Draft and Issued Author Contract Request Status on 10 July, 2017
        public IHttpActionResult IssuedDraftAuthorContractDetails(int ExecutiveId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("ExecutiveId", SqlDbType.VarChar, 200);
            parameters[0].Value = "'" + ExecutiveId + "'";

            var _GetPendingAuthorContractRequest = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_Issued_Draft_AuthorContract_Dashboard_get", parameters).ToList();
            return Json(_GetPendingAuthorContractRequest);
        }

        //-------------Get Detail list of Inbound Permission Not Entered By Author Contract on 02/09/2016
        public IHttpActionResult InboundPermissionNotEntered(int ExecutiveId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("ExecutiveId", SqlDbType.VarChar, 200);
            parameters[0].Value = "'" + ExecutiveId + "'";

            var _GetInboundPermissionNotEntered = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_InboundPermissionNotEntered_get", parameters).ToList();
            return Json(_GetInboundPermissionNotEntered);
        }

        //-------------Get Detail list of Inbound Permission Not Entered By ProductLicense on 13/09/2016
        public IHttpActionResult InboundPermissionNotEnteredByProductLicense()
        {
            var _GetInboundPermissionNotEnteredByProductLicense = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_InboundPermissionNotEntered_ByProductLicense_get").ToList();
            return Json(_GetInboundPermissionNotEnteredByProductLicense);
        }

        //-------------Get Detail list of Final Publishing Date Not Entered on 02/09/2016
        public IHttpActionResult FinalPublishingDateNotEntered(int ExecutiveId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("ExecutiveId", SqlDbType.VarChar, 200);
            parameters[0].Value = "'" + ExecutiveId + "'";

            var _GetFinalPublishingDateNotEntered = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_FinalPublishingDateNotEntered_get", parameters).ToList();
            return Json(_GetFinalPublishingDateNotEntered);
        }

        //-------------Get Detail list of Final Publishing Date is Entered but Impression not entered on 02/09/2016
        public IHttpActionResult ImpressionNotEntered(int ExecutiveId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("ExecutiveId", SqlDbType.VarChar, 200);
            parameters[0].Value = "'" + ExecutiveId + "'";

            var _GetImpressionNotEntered = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_FinalPublishingDateEntered_ImpressionNotEntered_get", parameters).ToList();
            return Json(_GetImpressionNotEntered);
        }

        //-------------Get Detail list of Final Publishing Date is Entered but Impression not entered based on AuthorContract on 02/09/2016 By Ankush
        public IHttpActionResult ImpressionNotEntered_AuthorContract(int ExecutiveId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("ExecutiveId", SqlDbType.VarChar, 200);
            parameters[0].Value = "'" + ExecutiveId + "'";

            var _GetImpressionNotEntered = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_FinalPublishingDateEntered_ImpressionNotEntered_AuthorContract_get", parameters).ToList();
            return Json(_GetImpressionNotEntered);
        }

        //-------------Get Detail list of Right Sale Contract Expire within 3 month on 05/09/2016 
        public IHttpActionResult RightSaleContractExpiring(int ExecutiveId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("ExecutiveId", SqlDbType.VarChar, 200);
            parameters[0].Value = "'" + ExecutiveId + "'";

            var _GetRightSaleContractExpiring = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_RightSaleContractExpiring_get", parameters).ToList();
            return Json(_GetRightSaleContractExpiring);
        }

        //-------------Get Detail list of Right Sale License Expire within 3 month on 26/09/2016  Added by Ankush
        public IHttpActionResult RightSaleContractExpiring_License()
        {
            var _GetRightSaleContractExpiring = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_RightSaleContractExpiring_License_get").ToList();
            return Json(_GetRightSaleContractExpiring);
        }

        //-------------Get Detail list of Right Sale Payment Not Receive By ContractId on 06/09/2016
        public IHttpActionResult RightSalePaymentNotReceiveByContractId(int ExecutiveId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("ExecutiveId", SqlDbType.VarChar, 200);
            parameters[0].Value = "'" + ExecutiveId + "'";

            var _GetRightSalePaymentNotReceive = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_RightSalePaymentNotReceive_ByContractId_get", parameters).ToList();
            return Json(_GetRightSalePaymentNotReceive);
        }

        //-------------Get Detail list of Right Sale Payment Not Receive By LicenseId on 06/09/2016
        public IHttpActionResult RightSalePaymentNotReceiveByLicenseId(int ExecutiveId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("ExecutiveId", SqlDbType.VarChar, 200);
            parameters[0].Value = "'" + ExecutiveId + "'";

            var _GetRightSalePaymentNotReceive = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_RightSalePaymentNotReceive_ByLicenseId_get", parameters).ToList();
            return Json(_GetRightSalePaymentNotReceive);
        }

        //-------------Get Detail list of Permission Outbound Payment Not Receive By ContractId on 06/09/2016
        public IHttpActionResult PermissionOutboundPaymentNotReceiveByContractId(int ExecutiveId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("ExecutiveId", SqlDbType.VarChar, 200);
            parameters[0].Value = "'" + ExecutiveId + "'";

            var _GetPermissionOutboundPaymentNotReceive = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_PermissionOutboundPaymentNotReceive_ByContractId_get", parameters).ToList();
            return Json(_GetPermissionOutboundPaymentNotReceive);
        }

        //-------------Get Detail list of Permission Outbound Payment Not Receive By LicenseId on 06/09/2016
        public IHttpActionResult PermissionOutboundPaymentNotReceiveByLicenseId(int ExecutiveId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("ExecutiveId", SqlDbType.VarChar, 200);
            parameters[0].Value = "'" + ExecutiveId + "'";

            var _GetPermissionOutboundPaymentNotReceive = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_PermissionOutboundPaymentNotReceive_ByLicenseId_get", parameters).ToList();
            return Json(_GetPermissionOutboundPaymentNotReceive);
        }

        //-----End by Prakash 

        //Added by Ankush on 28/09/2016

        //-------------Get Detail list of Product licenses which will be expiring within 3 months or for which balance quantity is less than 25%  on 28/09/2016
        public IHttpActionResult ProductLicenseExpired(int ExecutiveId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("ExecutiveId", SqlDbType.VarChar, 200);
            parameters[0].Value = "'" + ExecutiveId + "'";

            var _GetProductLicenseExpired = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_ProductLicenseExpired_get", parameters).ToList();
            return Json(_GetProductLicenseExpired);
        }

        //-------------Get Detail list of Product  License Addendums which will be expiring within 3 months or for which balance quantity is less than 25% on 28/09/2016
        public IHttpActionResult ProductLicenseAddendumExpired()
        {
            var _GetProductLicenseAddendumExpired = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_ProductLicenseAddendumExpired_get").ToList();
            return Json(_GetProductLicenseAddendumExpired);
        }

        public IHttpActionResult InboundPermissionQuantityLess25(int ExecutiveId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("ExecutiveId", SqlDbType.VarChar, 200);
            parameters[0].Value = "'" + ExecutiveId + "'";

            var _GetInboundPermissionQuantityLess25 = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_InboundPermissionQuantityLess25_get", parameters).ToList();
            return Json(_GetInboundPermissionQuantityLess25);
        }

        //End by Ankush



        public IHttpActionResult PermissionOutboundExpiryDate_List(int ExecutiveId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("ExecutiveId", SqlDbType.VarChar, 200);
            parameters[0].Value = "'" + ExecutiveId + "'";

            var _GetInboundPermissionQuantityLess25 = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_PermissionInboundExpiryDate_Dashboard_get", parameters).ToList();
            return Json(_GetInboundPermissionQuantityLess25);
        }
      
      


    }
}