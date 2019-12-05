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
            List<float> location_x = new List<float>();
            location_x.Add(123);
            location_x.Add(456);
            List<float> location_y = new List<float>();
            location_y.Add(123);
            location_y.Add(456);

            ViewBag.location_x = location_x;
            ViewBag.location_y = location_y;

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


        [HttpPost]
        public JsonResult InsertData(Models.TubeData tube)
        {
            try
            {
                tubeservice.InsertTube(tube);
                return this.Json(true);
            }
            catch (Exception ex)
            {
                return this.Json(false);
            }

        }
    }
}
