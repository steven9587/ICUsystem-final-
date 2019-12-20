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
        readonly Models.LoginService loginservice = new Models.LoginService();
        readonly Models.InfoService infoservice = new Models.InfoService();
        String patientId = "";

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
            //ViewBag.location_x = tubeservice.GetTubeLocationX();
            //ViewBag.location_y = tubeservice.GetTubeLocationY();
            //return xy to view from db
            return View();
        }

        public ActionResult Search()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        
        public ActionResult Switch(string id)
        {
            ViewBag.test = "test";
            var search_id = TempData["id"] as string;
            //HomeController.Initialize(patientId);
            ViewBag.location_x = tubeservice.GetTubeLocationX(search_id);
            ViewBag.location_y = tubeservice.GetTubeLocationY(search_id);
            ViewBag.date = tubeservice.GetTubeDate(search_id);

            return View("Switch");
        }

        [HttpPost]
        public ActionResult Getifo(String PatientId)
        {
            patientId = PatientId;
            TempData["id"] = patientId;
            return this.Json(true);
        }
            



       

        /// <summary>
        /// GetPipeLineDropDownList
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetPipeLineDropDownList()
        {
            return Json(this.tubeservice.GetPipeLine());
        }
        public JsonResult GetDivisionDropDownList()
        {
            return Json(this.infoservice.GetDivision());
        }
        public JsonResult GetDoctorNameDropDownList()
        {
            return Json(this.infoservice.GetDoctorName());
        }

        /// <summary>
        /// getsearchgrid
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public JsonResult GetGridData(Models.InfoData data)
        {
            return Json(this.infoservice.GetInfoByCondtioin(data));
        }
        /// <summary>
        /// GetTubePartNameDropDownList
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetTubePartNameDropDownList()
        {
            return Json(this.tubeservice.GetTubePartName());
        }

        /// <summary>
        /// Get xy from view return detail to view from db
        /// </summary>
        /// <param name="tubexy"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetTubeData(Models.TubeData tubexy)
        {
            return Json(this.tubeservice.GetTubeData(tubexy));
        }

        /// <summary>
        /// insertdata
        /// </summary>
        /// <param name="tube"></param>
        /// <returns></returns>
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

      

        /// <summary>
        /// editdata
        /// </summary>
        /// <param name="tube"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult EditData(Models.TubeData tube)
        {
            try
            {
                tubeservice.EditTube(tube);
                return this.Json(true);
            }
            catch (Exception ex)
            {

                return this.Json(false);


            }
        }





        /// <summary>
        /// DeleteTube
        /// </summary>
        /// <param name="tube"></param>
        /// <returns></returns>
        public JsonResult DeleteTube(Models.TubeData tube)
        {
            try
            {
                tubeservice.DeleteTube(tube);
                return this.Json(true);
            }
            catch (Exception ex)
            {
                return this.Json(false);
            }

        }

        /// <summary>
        /// userlogin
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UserLogin(Models.LoginData login)
        {

            loginservice.UserLogin(login);
            /* return this.Json(true);*/

            if (loginservice.UserLogin(login) != 0)
            {
                return this.Json(true);
            }
            else
            {
                return this.Json(false);
            }

        }
    }
}
