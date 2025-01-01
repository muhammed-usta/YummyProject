using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YummyProject.Context;

namespace YummyProject.Controllers
{
    [Authorize]
    public class AdminLayoutController : Controller
    {
        YummyContext context = new YummyContext();
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult AdminLayoutHead()
        {
            return PartialView();
        }
        public PartialViewResult AdminLayoutSideBar()
        {

            return PartialView();
        }

        public PartialViewResult AdminLayoutNavBar()
        {
            return PartialView();
        }

        public PartialViewResult AdminLayoutScripts()
        {
            return PartialView();
        }


    }
}