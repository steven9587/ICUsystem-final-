﻿using System;
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
        /// INSERT
        /// </summary>
        /// <returns></returns>
        public int InsertTube(Models.TubeData tube)
        {
            string sql = @"INSERT INTO TUBE_INSERT(Patient_ID,Tube_Name_ID,Tube_Part_ID,In_Body_Cm,Caliber,Sys_Date,Exp_Date,Tube_Note,Location_X,Location_Y) 
                           VALUES(@PatientID,@TubeNameID,@TubePartID,@InBodyCm,@Caliber,@SysDate,@ExpDate,@TubeNote,@LocationX,@LocationY); 
                           ";

            int TubeID;
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@PatientID", tube.PatientID));
                cmd.Parameters.Add(new SqlParameter("@TubeNameID", tube.TubeNameID));
                cmd.Parameters.Add(new SqlParameter("@TubePartID", tube.TubePartID));
                cmd.Parameters.Add(new SqlParameter("@InBodyCm", tube.InBodyCm));
                cmd.Parameters.Add(new SqlParameter("@Caliber", tube.Caliber));
                cmd.Parameters.Add(new SqlParameter("@SysDate", tube.SysDate));
                cmd.Parameters.Add(new SqlParameter("@ExpDate", tube.ExpDate));
                cmd.Parameters.Add(new SqlParameter("@TubeNote", tube.TubeNote));
                cmd.Parameters.Add(new SqlParameter("@LocationX", tube.LocationX));
                cmd.Parameters.Add(new SqlParameter("@LocationY", tube.LocationY));
                SqlTransaction Tran = conn.BeginTransaction();
                cmd.Transaction = Tran;
                try
                {
                    TubeID = Convert.ToInt32(cmd.ExecuteScalar());
                    Tran.Commit();
                }
                catch (Exception)
                {
                    Tran.Rollback();
                    throw;
                }
                finally
                {
                    conn.Close();
                }

            }
            return TubeID;
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
            string sql = @" SELECT Tube_Name_ID as TubeID,Tube_Name as TubeName 
                            FROM tube_info;";
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