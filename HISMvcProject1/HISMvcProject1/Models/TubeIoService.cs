using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HISMvcProject1.Models
{
    public class TubeIoService
    {
        private string GetDBConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }

        /// <summary>
        /// 第一io
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<Models.TubeIoData> GetFirstIo(Models.TubeIoData data)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT In_date,
                                  SUM(In_amount) as IoIntake ,
                                  SUM(Out_amount) as IoOutput ,
                                  SUM(In_amount-Out_amount) as Difference 
                           FROM TUBEIO_INFO 
                           WHERE Patient_ID = @PatientId
                           GROUP BY In_date;";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@PatientId", data.PatientID));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapIoData(dt);
        }

        private List<Models.TubeIoData> MapIoData(DataTable dt)
        {
            List<Models.TubeIoData> resultModify = new List<TubeIoData>();
            foreach (DataRow row in dt.Rows)
            {
                resultModify.Add(new TubeIoData()
                {
                    IoDate = row["In_date"].ToString(),
                    IoIntake = (int)row["IoIntake"],
                    IoOutput = (int)row["IoOutput"],
                    Difference = (int)row["Difference"],

                });
            }
            return resultModify;

        }

        /// <summary>
        /// 第二io早班
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>

        public List<Models.TubeIoData> GetSecMorningIoGridData(Models.TubeIoData data)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT SUM(tio.In_amount) as IoIntake ,
                                  SUM(tio.Out_amount) as IoOutput ,
                                  SUM(tio.In_amount-Out_amount) as Difference 
                          FROM TUBEIO_INFO tio
						     left join TUBE_INSERT t on tio.Tube_Info_ID = t.Tube_Info_ID
                          where location_x = @LocationX and @LocationY = @LocationY
                          AND tio.patient_id = @PatientId 
                          AND tio.In_date = @IoDate
                          AND tio.In_Time BETWEEN '07:01:00' AND '15:00:00'
                          GROUP BY tio.In_date
                           ;";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@PatientId", data.PatientID));
                cmd.Parameters.Add(new SqlParameter("@LocationX", data.LocationX));
                cmd.Parameters.Add(new SqlParameter("@LocationY", data.LocationY));
                cmd.Parameters.Add(new SqlParameter("@IoDate", data.IoDate));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapSecIoMorningData(dt);
        }

        private List<Models.TubeIoData> MapSecIoMorningData(DataTable dt)
        {
            List<Models.TubeIoData> resultModify = new List<TubeIoData>();
            foreach (DataRow row in dt.Rows)
            {
                resultModify.Add(new TubeIoData()
                {
                    IoIntake = (int)row["IoIntake"],
                    IoOutput = (int)row["IoOutput"],
                    Difference = (int)row["Difference"],

                });
            }
            return resultModify;

        }


    }
}