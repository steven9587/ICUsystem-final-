using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HISMvcProject1.Models
{
    public class MedService
    {       /// <summary>
            /// 取得DB連線字串
            /// </summary>
            /// <returns></returns>
        private string GetDBConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }


        /// <summary>
        /// GetPatientData
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Models.MedData GetMedDataByClass(Models.MedData data)
        {
            DataTable dt = new DataTable();
            string sql = @"select mh.MEDNAME_ID as MedNameId,
	                              mn.MED_NAME as MedName,
	                              mc.MEDCLASS_ID as MedClassId,
	                              mc.MED_CLASS as MedClass,
	                              mh.MED_STARTDATE as MedStart,
	                              mh.MED_OVERDATE as MedEnd,
	                              mh.MED_SOURCE as MedSource
                          from MEDNAME_INFO mn 
                          inner join MEDHISTORY_INFO mh on mn.MEDNAME_ID = mh.MEDNAME_ID
                          inner join MEDCLASS_INFO mc on mn.MEDCLASS_ID = mc.MEDCLASS_ID
                          where mc.MEDCLASS_ID  IN ("+ string.Join(",", medclassidlist.ToArray()) + ")
                          and patient_hisid = @PatientId
                          order by mh.ITEM";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                string[] medclassid = data.MedClassId.Split(',');
                int count = 0;
                List<string> medclassidlist = new List<string>();
                foreach (string id in medclassid) {
                    string classid = "@MedClassId" + count++;
                    cmd.Parameters.Add(classid, SqlDbType.Int).Value = id;
                    medclassidlist.Add(classid);
                }
                cmd.Parameters.Add(new SqlParameter("@PatientId", data.PatientId));

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return MapData(dt);
        }

        private Models.MedData MapData(DataTable dt)
        {
            Models.MedData result = new Models.MedData();
            result.MedNameId = (int)dt.Rows[0]["MedNameId"];
            result.MedName = dt.Rows[0]["MedName"].ToString();
            result.MedClassId = dt.Rows[0]["MedClassId"].ToString();
            result.MedClass = dt.Rows[0]["MedClass"].ToString();
            result.MedStart = dt.Rows[0]["MedStart"].ToString();
            result.MedEnd = dt.Rows[0]["MedEnd"].ToString();
            result.MedSource = dt.Rows[0]["MedSource"].ToString();
            return result;
        }
    }
}