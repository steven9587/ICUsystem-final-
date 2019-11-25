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
        [HttpPost]
        public JsonResult GetPipeLineDropDownList()
        {
            List<string> tempData = new List<string>();
            tempData.Add("RD");
            tempData.Add("ICP");
            tempData.Add("PENrose");
            return Json(tempData);
        }
        [HttpPost]
        public JsonResult GetPipeLineLocationDropDownList()
        {
            List<string> tempLocation = new List<string>();
            tempLocation.Add("頭");
            tempLocation.Add("鼻");
            tempLocation.Add("脖子");
            tempLocation.Add("胸腔");
            tempLocation.Add("腹腔");
            tempLocation.Add("尿管");
            tempLocation.Add("屁股");
            tempLocation.Add("周邊動脈");
            return Json(tempLocation);
        }
    }
}
