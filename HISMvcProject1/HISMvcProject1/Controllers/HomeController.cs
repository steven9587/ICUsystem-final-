using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HIS.Entity.Models;
using HIS.Entity.ViewModels;
using HIS.Mvc;
using HIS.Mvc.Controller;
using HIS.Systems;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace HISMvcProject1.Controllers
{
    public class HomeController : Controller
    {
        readonly Models.TubeService tubeservice = new Models.TubeService();
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        

  
        public ActionResult Main()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult Search()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult Switch()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        //
        [HttpPost]
        public JsonResult GetPipeLineDropDownList()
        {
            return Json(this.tubeservice.GetPipeLine());
        }
        //
        [HttpPost]
        public JsonResult GetTubePartNameDropDownList()
        {
            return Json(this.tubeservice.GetTubePartName());
        }
    }
}
