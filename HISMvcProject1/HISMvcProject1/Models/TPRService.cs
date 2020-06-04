using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HISMvcProject1.Models
{
    public class TPRService
    {
        private string GetDBConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }
        public List<String> GetTPRBreath(Models.TPRData breath)
        {
            DataTable dt = new DataTable();
            string sql = @" SELECT TPR_BREA AS TPR_BREA
	                       FROM TPR_INFO
                           Where Patient_ID = @PatientId  
                           and TPR_DATE between @TPRStart and @TPREnd;";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@PatientId", breath.PatientId));
                cmd.Parameters.Add(new SqlParameter("@TPRStart", breath.TPRStart));
                cmd.Parameters.Add(new SqlParameter("@TPREnd", breath.TPREnd));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }

            List<String> result = new List<String>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(row["TPR_BREA"].ToString());

            }
            return result;
        }
        public List<String> GetTPRTemp(Models.TPRData temp)
        {
            DataTable dt = new DataTable();
            string sql = @" SELECT TPR_TEMP AS TPR_TEMP
	                       FROM TPR_INFO
                           Where Patient_ID = @PatientId  
                           and TPR_DATE between @TPRStart and @TPREnd;";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@PatientId", temp.PatientId));
                cmd.Parameters.Add(new SqlParameter("@TPRStart", temp.TPRStart));
                cmd.Parameters.Add(new SqlParameter("@TPREnd", temp.TPREnd));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }

            List<String> result = new List<String>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(row["TPR_TEMP"].ToString());

            }
            return result;
        }
        public List<String> GetTPRPluse(Models.TPRData pluse)
        {
            DataTable dt = new DataTable();
            string sql = @" SELECT TPR_PLUSE AS TPR_PLUSE
	                       FROM TPR_INFO
                           Where Patient_ID = @PatientId  
                           and TPR_DATE between @TPRStart and @TPREnd;";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@PatientId", pluse.PatientId));
                cmd.Parameters.Add(new SqlParameter("@TPRStart", pluse.TPRStart));
                cmd.Parameters.Add(new SqlParameter("@TPREnd", pluse.TPREnd));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }

            List<String> result = new List<String>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(row["TPR_PLUSE"].ToString());

            }
            return result;
        }

        public List<String> GetTPRBreathWeek(Models.TPRData breath)
        {
            DataTable dt = new DataTable();
            string sql = @" SELECT TPR_BREA AS TPR_BREA
	                       FROM TPR_INFO
                           Where Patient_ID = @PatientId  
                           and TPR_DATE between DATEADD(day,-6, @TPRStart) and @TPRStart;";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@PatientId", breath.PatientId));
                cmd.Parameters.Add(new SqlParameter("@TPRStart", breath.TPRStart));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }

            List<String> result = new List<String>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(row["TPR_BREA"].ToString());

            }
            return result;
        }
        public List<String> GetTPRTempWeek(Models.TPRData temp)
        {
            DataTable dt = new DataTable();
            string sql = @" SELECT TPR_TEMP AS TPR_TEMP
	                       FROM TPR_INFO
                           Where Patient_ID = @PatientId  
                           and TPR_DATE between DATEADD(day,-6, @TPRStart) and @TPRStart;";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@PatientId", temp.PatientId));
                cmd.Parameters.Add(new SqlParameter("@TPRStart", temp.TPRStart));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }

            List<String> result = new List<String>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(row["TPR_TEMP"].ToString());

            }
            return result;
        }
        public List<String> GetTPRPluseWeek(Models.TPRData pluse)
        {
            DataTable dt = new DataTable();
            string sql = @" SELECT TPR_PLUSE AS TPR_PLUSE
	                       FROM TPR_INFO
                           Where Patient_ID = @PatientId  
                           and  TPR_DATE between DATEADD(day,-6, @TPRStart) and @TPRStart;";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@PatientId", pluse.PatientId));
                cmd.Parameters.Add(new SqlParameter("@TPRStart", pluse.TPRStart));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }

            List<String> result = new List<String>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(row["TPR_PLUSE"].ToString());

            }
            return result;
        }
    }
}