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
        readonly Models.TubeIoService tubeioservice = new Models.TubeIoService();
        readonly Models.MedService medservice = new Models.MedService();
        readonly Models.TPRService TPRservice = new Models.TPRService();
        String patientId = "";
        List<string> medname = new List<string>();
        List<string> medstart = new List<string>();
        List<string> mednamehm = new List<string>();
        List<string> medsourse = new List<string>();
        List<string> TPRBreath = new List<string>();
        List<string> TPRTemp = new List<string>();
        List<string> TPRPulse = new List<string>();
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


        public ActionResult Switch()
        {
            //var search_id = TempData["id"] as string;
            var search_id = "406570123";
            ViewBag.location_x = tubeservice.GetTubeLocationX(search_id);
            ViewBag.location_y = tubeservice.GetTubeLocationY(search_id);
            ViewBag.tube_caliber = tubeservice.GetTubeCaliber(search_id);
            ViewBag.tube_inbodycm = tubeservice.GetTubeInBodyCm(search_id);
            ViewBag.tube_insertname = tubeservice.GetTubeInsertName(search_id);
            ViewBag.date = tubeservice.GetTubeDate(search_id);
            ViewBag.search_id = search_id;
            return View("Switch");
        }

        public ActionResult MHistory()
        {
            var search_id = TempData["id"] as string;
            ViewBag.search_id = search_id;
            return View("MHistory");

        }

        /// <summary>
        ///  得到搜尋病人病歷號
        /// </summary>
        /// <param name="PatientId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Getifo(String PatientId)
        {
            patientId = PatientId;
            TempData["id"] = patientId;
            return this.Json(true);
        }

        /// <summary>
        /// 得到病人基本資料
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetPatientData(Models.InfoData data)
        {
            return Json(this.infoservice.GetPatientData(data));
        }




        /// <summary>
        /// GetPipeLineDropDownList
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetPipeLineDropDownList(Models.TubeData data)
        {
            return Json(this.tubeservice.GetPipeLine(data));
        }
        [HttpPost]
        public JsonResult GetPipeLineDropDownListStart()
        {
            return Json(this.tubeservice.GetPipeLineStart());
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
        /// getioinsearchgrid
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public JsonResult GetIoInGridData(Models.TubeIoData data)
        {
            return Json(this.tubeioservice.GetIoInByCondtioin(data));
        }

        /// <summary>
        /// getiooutsearchgrid
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public JsonResult GetIoOutGridData(Models.TubeIoData data)
        {
            return Json(this.tubeioservice.GetIoOutByCondtioin(data));
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
            if (loginservice.UserLogin(login) != 0)
            {
                return this.Json(true);
            }
            else
            {
                return this.Json(false);
            }

        }

        /// <summary>
        /// GetFirstIoGridData
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetFirstIoGridData(Models.TubeIoData data)
        {
            return Json(this.tubeioservice.GetFirstIo(data));
        }

        /// <summary>
        /// /GetSecMorningIoGridData
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetSecMorningIoGridData(Models.TubeIoData data)
        {
            return Json(this.tubeioservice.GetSecMorningIoGridData(data));
        }

        /// <summary>
        /// GetSecNightIoGridData
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetSecNightIoGridData(Models.TubeIoData data)
        {
            return Json(this.tubeioservice.GetSecNightIoGridData(data));
        }


        /// <summary>
        /// GetSecGraveyardIoGridData
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetSecGraveyardIoGridData(Models.TubeIoData data)
        {
            return Json(this.tubeioservice.GetSecGraveyardIoGridData(data));
        }


        /// <summary>
        /// GetSecAllDayIoGridData
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetSecAllDayIoGridData(Models.TubeIoData data)
        {
            return Json(this.tubeioservice.GetSecAllDayIoGridData(data));
        }

        [HttpGet]
        public JsonResult GetMedMedName(Models.MedData data)
        {
            foreach (string item in medservice.GetMedNameData(data))
            {
                medname.Add(item);

            }
            return Json(new { medname = medname }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult GetMedData(Models.MedData data)
        {
            var test = medservice.GetMedData(data);
            TempData["meddata"] = test;
            return this.Json(true);
        }
        [HttpPost]
        public JsonResult GetMedDayData(Models.MedData data)
        {
            return Json(this.medservice.GetMedDataByMedName(data));
        }
        public ActionResult PushDataToHM()
        {
            var test = TempData["meddata"];
            return Json(test, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// GetTPRData
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetTPRData(Models.TPRData data)
        {
            foreach (string item in TPRservice.GetTPRBreath(data))
            {
                TPRBreath.Add(item);

            }
            foreach (string item in TPRservice.GetTPRTemp(data))
            {
                TPRTemp.Add(item);

            }
            foreach (string item in TPRservice.GetTPRPluse(data))
            {
                TPRPulse.Add(item);

            }
            return Json(new { TPRBreath = TPRBreath, TPRPulse = TPRPulse , TPRTemp = TPRTemp }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// GetTPRDataWeek
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetTPRDataWeek(Models.TPRData data)
        {
            foreach (string item in TPRservice.GetTPRBreathWeek(data))
            {
                TPRBreath.Add(item);

            }
            foreach (string item in TPRservice.GetTPRTempWeek(data))
            {
                TPRTemp.Add(item);

            }
            foreach (string item in TPRservice.GetTPRPluseWeek(data))
            {
                TPRPulse.Add(item);

            }
            return Json(new { TPRBreath = TPRBreath, TPRPulse = TPRPulse, TPRTemp = TPRTemp }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetPatientNote(Models.InfoData data)
        {
            return Json(this.infoservice.GetPatientNote(data));
        }
    

    }
}


