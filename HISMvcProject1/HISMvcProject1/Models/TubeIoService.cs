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
                          where location_x = @LocationX and location_y = @LocationY
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

        /// <summary>
        /// 第二io小夜班
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<Models.TubeIoData> GetSecNightIoGridData(Models.TubeIoData data)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT SUM(tio.In_amount) as IoIntake ,
                                  SUM(tio.Out_amount) as IoOutput ,
                                  SUM(tio.In_amount-Out_amount) as Difference 
                          FROM TUBEIO_INFO tio
						     left join TUBE_INSERT t on tio.Tube_Info_ID = t.Tube_Info_ID
                          where location_x = @LocationX and location_y = @LocationY
                          AND tio.patient_id = @PatientId 
                          AND tio.In_date = @IoDate
                          AND tio.In_Time BETWEEN '15:01:00' AND '23:00:00'
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
            return this.MapSecIoNightgData(dt);
        }

        private List<Models.TubeIoData> MapSecIoNightgData(DataTable dt)
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


        /// <summary>
        /// 第二io大夜班
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<Models.TubeIoData> GetSecGraveyardIoGridData(Models.TubeIoData data)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT  SUM(Io_Intake) as IoIntake ,SUM(Io_Output) as IoOutput,SUM(Difference_table) as Difference
                                   FROM( 
                          (SELECT SUM(tio.In_amount) as Io_Intake ,
                                   SUM(tio.Out_amount) as Io_Output ,
                                   SUM(tio.In_amount-Out_amount) as Difference_table 
                                   FROM TUBEIO_INFO tio
                                   left join TUBE_INSERT t on tio.Tube_Info_ID = t.Tube_Info_ID
                                   where location_x = @LocationX and location_y = @LocationY
                                   AND tio.patient_id = @PatientId 
                                   AND tio.In_date = @IoDate
                                   AND tio.In_Time BETWEEN '23:01:00' AND '23:59:59'
                                   GROUP BY tio.In_date)
                                   union all
                           (SELECT SUM(tio.In_amount) as Io_Intake ,
                                   SUM(tio.Out_amount) as Io_Output ,
                                   SUM(tio.In_amount-Out_amount) as Difference_table 
                                   FROM TUBEIO_INFO tio
                                   left join TUBE_INSERT t on tio.Tube_Info_ID = t.Tube_Info_ID
                                  where location_x = @LocationX and location_y = @LocationY
                                  AND tio.patient_id = @PatientId 
                                  AND tio.In_date = @IoDateAddOne
                                  AND tio.In_Time BETWEEN '00:00:00' AND '07:00:00'
                                  GROUP BY tio.In_date)
                                  )
                          as subquery
                           ;";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@PatientId", data.PatientID));
                cmd.Parameters.Add(new SqlParameter("@LocationX", data.LocationX));
                cmd.Parameters.Add(new SqlParameter("@LocationY", data.LocationY));
                cmd.Parameters.Add(new SqlParameter("@IoDate", data.IoDate));
                cmd.Parameters.Add(new SqlParameter("@IoDateAddOne", data.IoDateAddOne));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapSecIoNightData(dt);
        }

        private List<Models.TubeIoData> MapSecIoNightData(DataTable dt)
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

         /// <summary>
        /// 第二io全天
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<Models.TubeIoData> GetSecAllDayIoGridData(Models.TubeIoData data)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT  SUM(Io_Intake) as IoIntake ,SUM(Io_Output) as IoOutput,SUM(Difference_table) as Difference
                                   FROM( 
                          (SELECT SUM(tio.In_amount) as Io_Intake ,
                                   SUM(tio.Out_amount) as Io_Output ,
                                   SUM(tio.In_amount-Out_amount) as Difference_table 
                                   FROM TUBEIO_INFO tio
                                   left join TUBE_INSERT t on tio.Tube_Info_ID = t.Tube_Info_ID
                                   where location_x = @LocationX and location_y = @LocationY
                                   AND tio.patient_id = @PatientId 
                                   AND tio.In_date = @IoDate
                                   AND tio.In_Time BETWEEN '07:01:00' AND '23:59:59'
                                   GROUP BY tio.In_date)
                                   union all
                           (SELECT SUM(tio.In_amount) as Io_Intake ,
                                   SUM(tio.Out_amount) as Io_Output ,
                                   SUM(tio.In_amount-Out_amount) as Difference_table 
                                   FROM TUBEIO_INFO tio
                                   left join TUBE_INSERT t on tio.Tube_Info_ID = t.Tube_Info_ID
                                  where location_x = @LocationX and @LocationY = @LocationY
                                  AND tio.patient_id = @PatientId 
                                  AND tio.In_date = @IoDateAddOne
                                  AND tio.In_Time BETWEEN '00:00:00' AND '07:00:00'
                                  GROUP BY tio.In_date)
                                  )
                          as subquery
                           ;";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@PatientId", data.PatientID));
                cmd.Parameters.Add(new SqlParameter("@LocationX", data.LocationX));
                cmd.Parameters.Add(new SqlParameter("@LocationY", data.LocationY));
                cmd.Parameters.Add(new SqlParameter("@IoDate", data.IoDate));
                cmd.Parameters.Add(new SqlParameter("@IoDateAddOne", data.IoDateAddOne));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapSecIoAllDayData(dt);
        }

        private List<Models.TubeIoData> MapSecIoAllDayData(DataTable dt)
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

        /// <summary>
        /// 第三ioin
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<Models.TubeIoData> GetIoInByCondtioin(Models.TubeIoData data)
        {
            DataTable dt = new DataTable();
            string sql = @"select In_date,In_time,Note,In_amount,In_user 
                            from TUBEIO_INFO 
                            where In_date in(@IoStartDate,@IoEndDate) and patient_id = @PatientId;";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@PatientId", data.PatientID));
                cmd.Parameters.Add(new SqlParameter("@IoStartDate", data.IoStartDate));
                cmd.Parameters.Add(new SqlParameter("@IoEndDate", data.IoEndDate));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapIoInData(dt);
        }

        private List<Models.TubeIoData> MapIoInData(DataTable dt)
        {
            List<Models.TubeIoData> resultModify = new List<TubeIoData>();
            foreach (DataRow row in dt.Rows)
            {
                resultModify.Add(new TubeIoData()
                {
                    FullIoDate = row["In_date"].ToString(),
                    FullIoTime = row["In_time"].ToString(),
                    FullIoName = row["Note"].ToString(),
                    FullIoIn = row["In_amount"].ToString(),
                    FullIoUser = row["In_user"].ToString(),
                });
            }
            return resultModify;
        }

        /// <summary>
        /// 第三ioout
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<Models.TubeIoData> GetIoOutByCondtioin(Models.TubeIoData data)
        {
            DataTable dt = new DataTable();
            string sql = @"select In_date, In_time, Note, Out_amount, In_user
                            from TUBEIO_INFO
                            where In_date in(@IoStartDate, @IoEndDate) and patient_id = @PatientId; ";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@PatientId", data.PatientID));
                cmd.Parameters.Add(new SqlParameter("@IoStartDate", data.IoStartDate));
                cmd.Parameters.Add(new SqlParameter("@IoEndDate", data.IoEndDate));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapIoOutData(dt);
        }

        private List<Models.TubeIoData> MapIoOutData(DataTable dt)
        {
            List<Models.TubeIoData> resultModify = new List<TubeIoData>();
            foreach (DataRow row in dt.Rows)
            {
                resultModify.Add(new TubeIoData()
                {
                    FullIoDate = row["In_date"].ToString(),
                    FullIoTime = row["In_time"].ToString(),
                    FullIoName = row["Note"].ToString(),
                    FullIoOut = row["Out_amount"].ToString(),
                    FullIoUser = row["In_user"].ToString(),
                });
            }
            return resultModify;
        }
    }
}