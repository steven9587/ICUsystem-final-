using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HISMvcProject1.Models
{
    public class InfoService
    {
        /// <summary>
        /// 取得DB連線字串
        /// </summary>
        /// <returns></returns>
        private string GetDBConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }

        /// <summary>
        /// TubePartName
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetDivision()
        {
            DataTable dt = new DataTable();
            string sql = @" SELECT division_id as CodeID, 
	                               division_ename as CodeName 
                            FROM division_info;";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapCodeName(dt);
        }
        /// <summary>
        /// TubePartName
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetDoctorName()
        {
            DataTable dt = new DataTable();
            string sql = @" SELECT doctor_id as CodeID, 
	                               doctor_name as CodeName 
                            FROM doctor_info;";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapCodeName(dt);
        }
        /// <summary>
        /// getinfo
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public List<Models.InfoData> GetInfoByCondtioin(Models.InfoData data)
        {

            DataTable dt = new DataTable();
            string sql = @"SELECT pai.patient_bed as BedId,
                                  pai.patient_division as DivisionId ,
                                  di.division_ename as DivisionName,
                                  pai.patient_id as PatientID,
                                  pai.patient_name as PatientName,
                                  pai.patient_doctor_id as DoctorId,
                                  doi.doctor_name as DoctorName,
                                  ul.user_name as NurseName 
                           FROM PATIENT_INFO pai 
			               INNER JOIN DIVISION_INFO di 
                                ON pai.PATIENT_DIVISION = di.division_id
			               INNER JOIN doctor_info doi 
                                on pai.patient_doctor_id = doi.doctor_id
			               INNER JOIN user_login ul 
                                on pai.PATIENT_NURSE_ID_1 = ul.user_id
                           WHERE (pai.patient_id LIKE ('%'+@PatientId+'%') OR @PatientId='' )AND
					             (pai.patient_division LIKE ('%'+@DivisionId+'%') OR @DivisionId='')AND
								 (pai.patient_doctor_id LIKE ('%'+@DoctorId+'%') OR @DoctorId='' )
                           ORDER BY pai.patient_bed";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@DivisionId", data.DivisionId == null ? string.Empty : data.DivisionId));
                cmd.Parameters.Add(new SqlParameter("@DoctorId", data.DoctorId == null ? string.Empty : data.DoctorId));
                cmd.Parameters.Add(new SqlParameter("@PatientId", data.PatientId == null ? string.Empty : data.PatientId));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapBookDataToListForSearch(dt);
        }
        private List<Models.InfoData> MapBookDataToListForSearch(DataTable InfoData)
        {
            List<Models.InfoData> resultModify = new List<InfoData>();
            foreach (DataRow row in InfoData.Rows)
            {
                resultModify.Add(new InfoData()
                {
                    BedId = (int)row["BedId"],
                    PatientId= row["PatientID"].ToString(),
                    Division = row["DivisionName"].ToString(),
                    PatientName = row["PatientName"].ToString(),
                    DoctorName = row["DoctorName"].ToString(),
                    NurseName = row["NurseName"].ToString(),
                });
            }
            return resultModify;
        }

        /// <summary>
        /// TubePartNameMap
        /// </summary>
        /// <returns></returns>
        private List<SelectListItem> MapCodeName(DataTable dt)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["CodeName"].ToString(),
                    Value = row["CodeID"].ToString()
                });
            }
            return result;
        }
    }
}