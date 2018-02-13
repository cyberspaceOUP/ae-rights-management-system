using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Core;

namespace SLV.Web.Areas.Alert.Controllers
{
    public class AlertController : Controller
    {
        private readonly IWorkContext _workContext;

        public AlertController(IWorkContext workContext)
        {
            _workContext = workContext;
        }

        public ActionResult PendingRequestContract()
        {
            //if (_workContext.CurrentUser == null || Session["UserId"] == null)
            //{
            //    TempData["From"] = "S";
            //    return RedirectToAction("Login", "Login", new { area = "" });
            //}
            //else
            //{
              
            //    return View();
            //}

            return View();
          
        }

        public ActionResult PendingRequestLicense()
        {
            //if (_workContext.CurrentUser == null || Session["UserId"] == null)
            //{
            //    TempData["From"] = "S";
            //    return RedirectToAction("Login", "Login", new { area = "" });
            //}
            //else
            //{

            //    return View();
            //}

            return View();

        }

        public ActionResult ISBNNotEntered()
        {
            //if (_workContext.CurrentUser == null || Session["UserId"] == null)
            //{
            //    TempData["From"] = "S";
            //    return RedirectToAction("Login", "Login", new { area = "" });
            //}
            //else
            //{

            //    return View();
            //}

            return View();

        }

        public ActionResult SAPAgreementNumberNotEntered()
        {
            //if (_workContext.CurrentUser == null || Session["UserId"] == null)
            //{
            //    TempData["From"] = "S";
            //    return RedirectToAction("Login", "Login", new { area = "" });
            //}
            //else
            //{

            //    return View();
            //}

            return View();

        }

        public ActionResult RightsSellingContractExpiryDate()
        {
            //if (_workContext.CurrentUser == null || Session["UserId"] == null)
            //{
            //    TempData["From"] = "S";
            //    return RedirectToAction("Login", "Login", new { area = "" });
            //}
            //else
            //{

            //    return View();
            //}

            return View();

        }

        public ActionResult AuthorContractExpiryDate()
        {
            //if (_workContext.CurrentUser == null || Session["UserId"] == null)
            //{
            //    TempData["From"] = "S";
            //    return RedirectToAction("Login", "Login", new { area = "" });
            //}
            //else
            //{

            //    return View();
            //}

            return View();

        }

        public ActionResult ProductlicenseExpiryDate()
        {
            //if (_workContext.CurrentUser == null || Session["UserId"] == null)
            //{
            //    TempData["From"] = "S";
            //    return RedirectToAction("Login", "Login", new { area = "" });
            //}
            //else
            //{

            //    return View();
            //}

            return View();

        }

        public ActionResult ContractAddendumExpiryDate()
        {
            //if (_workContext.CurrentUser == null || Session["UserId"] == null)
            //{
            //    TempData["From"] = "S";
            //    return RedirectToAction("Login", "Login", new { area = "" });
            //}
            //else
            //{

            //    return View();
            //}

            return View();

        }

        public ActionResult ProductLicenseAddendumExpiryDate()
        {
            //if (_workContext.CurrentUser == null || Session["UserId"] == null)
            //{
            //    TempData["From"] = "S";
            //    return RedirectToAction("Login", "Login", new { area = "" });
            //}
            //else
            //{

            //    return View();
            //}

            return View();

        }

        public ActionResult ISBNleft()
        {
            //if (_workContext.CurrentUser == null || Session["UserId"] == null)
            //{
            //    TempData["From"] = "S";
            //    return RedirectToAction("Login", "Login", new { area = "" });
            //}
            //else
            //{

            //    return View();
            //}

            return View();

        }

        public ActionResult ContributorEntered()
        {
            //if (_workContext.CurrentUser == null || Session["UserId"] == null)
            //{
            //    TempData["From"] = "S";
            //    return RedirectToAction("Login", "Login", new { area = "" });
            //}
            //else
            //{

            //    return View();
            //}

            return View();

        }

        public ActionResult ProductLicenseEntered()
        {
            //if (_workContext.CurrentUser == null || Session["UserId"] == null)
            //{
            //    TempData["From"] = "S";
            //    return RedirectToAction("Login", "Login", new { area = "" });
            //}
            //else
            //{

            //    return View();
            //}

            return View();

        }

        public ActionResult PendingRequestOtherContract()
        {
            //if (_workContext.CurrentUser == null || Session["UserId"] == null)
            //{
            //    TempData["From"] = "S";
            //    return RedirectToAction("Login", "Login", new { area = "" });
            //}
            //else
            //{

            //    return View();
            //}

            return View();

        }

        public ActionResult OtherContractExpiryDate()
        {
            //if (_workContext.CurrentUser == null || Session["UserId"] == null)
            //{
            //    TempData["From"] = "S";
            //    return RedirectToAction("Login", "Login", new { area = "" });
            //}
            //else
            //{

            //    return View();
            //}

            return View();

        }

        public ActionResult BalanceQuantityAddendum()
        {
            //if (_workContext.CurrentUser == null || Session["UserId"] == null)
            //{
            //    TempData["From"] = "S";
            //    return RedirectToAction("Login", "Login", new { area = "" });
            //}
            //else
            //{

            //    return View();
            //}

            return View();

        }

        public ActionResult InboundPermissionNotEntered()
        {
            //if (_workContext.CurrentUser == null || Session["UserId"] == null)
            //{
            //    TempData["From"] = "S";
            //    return RedirectToAction("Login", "Login", new { area = "" });
            //}
            //else
            //{

            //    return View();
            //}

            return View();

        }

        public ActionResult BalanceQuantityProductLicense()
        {
            //if (_workContext.CurrentUser == null || Session["UserId"] == null)
            //{
            //    TempData["From"] = "S";
            //    return RedirectToAction("Login", "Login", new { area = "" });
            //}
            //else
            //{

            //    return View();
            //}

            return View();

        }

        public ActionResult PermissionOutboundExpiryDate()
        {
            //if (_workContext.CurrentUser == null || Session["UserId"] == null)
            //{
            //    TempData["From"] = "S";
            //    return RedirectToAction("Login", "Login", new { area = "" });
            //}
            //else
            //{

            //    return View();
            //}

            return View();

        }

        public ActionResult PermissionInboundExpiryDate()
        {
            //if (_workContext.CurrentUser == null || Session["UserId"] == null)
            //{
            //    TempData["From"] = "S";
            //    return RedirectToAction("Login", "Login", new { area = "" });
            //}
            //else
            //{

            //    return View();
            //}

            return View();

        }

        public ActionResult RecurringExpiryDate()
        {
            //if (_workContext.CurrentUser == null || Session["UserId"] == null)
            //{
            //    TempData["From"] = "S";
            //    return RedirectToAction("Login", "Login", new { area = "" });
            //}
            //else
            //{

            //    return View();
            //}

            return View();

        }

        public ActionResult PermissionOutboundReceivedInvoiceDate()
        {
            //if (_workContext.CurrentUser == null || Session["UserId"] == null)
            //{
            //    TempData["From"] = "S";
            //    return RedirectToAction("Login", "Login", new { area = "" });
            //}
            //else
            //{

            //    return View();
            //}

            return View();

        }

        public ActionResult PermissionInboundBalanceQuantity()
        {
            //if (_workContext.CurrentUser == null || Session["UserId"] == null)
            //{
            //    TempData["From"] = "S";
            //    return RedirectToAction("Login", "Login", new { area = "" });
            //}
            //else
            //{

            //    return View();
            //}

            return View();

        }

        public ActionResult RightsSellingAdvancePayment()
        {
            //if (_workContext.CurrentUser == null || Session["UserId"] == null)
            //{
            //    TempData["From"] = "S";
            //    return RedirectToAction("Login", "Login", new { area = "" });
            //}
            //else
            //{

            //    return View();
            //}

            return View();

        }

        public ActionResult RightsSellingAlertFrequency()
        {
            //if (_workContext.CurrentUser == null || Session["UserId"] == null)
            //{
            //    TempData["From"] = "S";
            //    return RedirectToAction("Login", "Login", new { area = "" });
            //}
            //else
            //{

            //    return View();
            //}

            return View();

        }

        public ActionResult TestMail()
        {
            return View();
        }


    }
}