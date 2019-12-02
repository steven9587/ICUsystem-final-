using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HISMvcProject1.Models
{
    public class TubeService
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
        public List<SelectListItem> GetTubePartName()
        {
            DataTable dt = new DataTable();
            string sql = @" SELECT tube_part_id as CodeID, 
	                               tube_part as CodeName 
                            FROM tube_part;";
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

        /// <summary>
        /// PipeLine
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetPipeLine()
        {
            DataTable dt = new DataTable();
            string sql = @" SELECT Tube_Name_ID as TubeID,Tube_Name as TubeName FROM tube_info;";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapTubeName(dt);
        }
        /// <summary>
        /// PipeLineMap
        /// </summary>
        /// <returns></returns>
        private List<SelectListItem> MapTubeName(DataTable dt)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["TubeName"].ToString(),
                    Value = row["TubeID"].ToString()
                });
            }
            return result;
        }

    }
}