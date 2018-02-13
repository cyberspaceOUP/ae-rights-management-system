using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Core;
using ACS.Services.Configuration;
using ACS.Services.Master;
//using ACS.Services.Directory;

namespace SLV.Web.Controllers
{

    //ADDED BY AMAN KUMAR ON DATE 07/03/2016
    public class NavigationController : Controller
    {
        private readonly IWorkContext _workContext;
        private readonly INavigationService _navigationService;
        private readonly ICommonListService _commonList;

        public NavigationController
            (
            IWorkContext workContext
            , INavigationService navigationService
            , ICommonListService commonList
            )
        {
            _workContext = workContext;
            _navigationService = navigationService;
            _commonList = commonList;
        }

         //GET: /Navigation/
        //Sending DepartmentId In place Of ProfileId,we have to maintain ProfileId for each Department through Database
        public ActionResult PartialMenu()
        {
            if (_workContext.CurrentUser != null)
            {
                if (Session["UserName"] == null)
                {
                    Session["UserName"] = _workContext.CurrentUser.executiveName;
                    Session["UserDepartment"] = _workContext.CurrentUser.DepartmentM.DepartmentName;
                    Session["UserId"] = _workContext.CurrentUser.Id;
                }

                Session.Timeout = 60;
                return PartialView(_navigationService.GetTopActivities(_workContext.CurrentUser.DepartmentId));
            }
            else
                return null; 
        }

        public ActionResult SubMenu(ACS.Core.Domain.Configuration.ApplicationActivities SubMenu)
        {
            if (_workContext.CurrentUser != null)
            {
                //Condition to check if resident has the righ for the menu option
                if (SubMenu.UserProfiles.Any(up => up.Id == (_workContext.CurrentUser.DepartmentId)))
                    return PartialView(SubMenu);
                else
                    return null;

            }
            else
            {
                //Condition to check if non resident user has the righ for the menu option
                    return null;
            }
        }

        public ActionResult PartialTicker()
        {
            if (_workContext.CurrentUser != null)
            {
                if (Session["UserName"] != null)
                {
                    //Start Ticker
                    var tickerList = _commonList.GetTickerList().ToList();
                    if (tickerList.Count > 0)
                    {
                        var _ticker = "";
                        foreach (var items in tickerList)
                        {
                            if (items.FromDate == null && items.ToDate == null)
                            {
                                if (_ticker != "")
                                {
                                    _ticker += "   |   ";
                                }
                                _ticker += items.Title;
                            }

                        }

                        foreach (var items in tickerList)
                        {
                            if (items.FromDate != null && items.ToDate == null)
                            {
                                if (Convert.ToDateTime(items.FromDate) <= Convert.ToDateTime(DateTime.Today))
                                {
                                    if (_ticker != "")
                                    {
                                        _ticker += "   |   ";
                                    }
                                    _ticker += items.Title;
                                }
                            }
                        }

                        foreach (var items in tickerList)
                        {
                            if (items.FromDate == null && items.ToDate != null)
                            {
                                if (Convert.ToDateTime(DateTime.Today) >= Convert.ToDateTime(items.ToDate))
                                {
                                    if (_ticker != "")
                                    {
                                        _ticker += "   |   ";
                                    }
                                    _ticker += items.Title;
                                }
                            }
                        }

                        foreach (var items in tickerList)
                        {
                            if (items.FromDate != null && items.ToDate != null)
                            {
                                if (Convert.ToDateTime(items.FromDate) <= Convert.ToDateTime(DateTime.Today) && Convert.ToDateTime(items.ToDate) >= Convert.ToDateTime(DateTime.Today))
                                {
                                    if (_ticker != "")
                                    {
                                        _ticker += "   |   ";
                                    }
                                    _ticker += items.Title;
                                }
                            }
                        }

                        if (_ticker != "")
                        {
                            Session["Ticker"] = _ticker;
                        }
                    }
                    //End Ticker
                }
                return PartialView();
            }
            else
                return null; 
        }

	}
}