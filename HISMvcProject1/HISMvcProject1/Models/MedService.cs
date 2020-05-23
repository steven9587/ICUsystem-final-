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


        private DataTable GetTVPValue<T>(params T[] args)
        {
            DataTable t = new DataTable();
            t.Columns.Add("Item", typeof(T));
            foreach (T item in args)
            {
                t.Rows.Add(item);
            }
            return t;
        }

        /// <summary>
        /// GetMedDataByClass
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<Models.MedData> GetMedDataByClass(Models.MedData data)
        {
            string[] MedclassIdString = data.MedClassId.Split(',');
            int[] MedclassIdInt = new int[MedclassIdString.Length];
            for (int i = 0; i < MedclassIdString.Length; i++)
            {
                MedclassIdInt[i] = Convert.ToInt16(MedclassIdString[i]);
            }
            DataTable dt = new DataTable();
            string sql = @"select mh.Item as Item,
                                  mh.MEDNAME_ID as MedNameId,
	                              mn.MED_NAME as MedName,
	                              mc.MEDCLASS_ID as MedClassId,
	                              mc.MED_CLASS as MedClass,
	                              Convert(varchar(10),mh.MED_STARTDATE,111)as MedStart,
	                              Convert(varchar(10),mh.MED_OVERDATE,111)as MedEnd,
	                              mh.MED_SOURCE as MedSource
                          from MEDNAME_INFO mn 
                          inner join MEDHISTORY_INFO mh on mn.MEDNAME_ID = mh.MEDNAME_ID
                          inner join MEDCLASS_INFO mc on mn.MEDCLASS_ID = mc.MEDCLASS_ID
                          where mc.MEDCLASS_ID  IN (SELECT Item FROM @MedClassId) and patient_hisid = @PatientId order by mh.ITEM";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                var medclassid = cmd.Parameters.Add(new SqlParameter("@MedClassId", SqlDbType.Structured));
                cmd.Parameters.Add(new SqlParameter("@PatientId", data.PatientId));
                medclassid.TypeName = "IntArray";
                medclassid.Value = GetTVPValue<int>(MedclassIdInt);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return MapData(dt);
        }
        private List<Models.MedData> MapData(DataTable InfoData)
        {
            List<Models.MedData> resultModify = new List<MedData>();
            foreach (DataRow row in InfoData.Rows)
            {
                resultModify.Add(new MedData()
                {
                    Item = (int)row["Item"],
                    MedNameId = (int)row["MedNameId"],
                    MedName = row["MedName"].ToString(),
                    MedClassId = row["MedClassId"].ToString(),
                    MedClass = row["MedClass"].ToString(),
                    MedStart = row["MedStart"].ToString(),
                    MedEnd = row["MedEnd"].ToString(),
                    MedSource = row["MedSource"].ToString()
                });
            }
            return resultModify;
        }


        /// <summary>
        /// GetMedNameData
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<string> GetMedNameData(Models.MedData data)
        {
            string[] MedclassIdString = data.MedClassId.Split(',');
            int[] MedclassIdInt = new int[MedclassIdString.Length];
            for (int i = 0; i < MedclassIdString.Length; i++)
            {
                MedclassIdInt[i] = Convert.ToInt16(MedclassIdString[i]);
            }
            DataTable dt = new DataTable();
            string sql = @"select mh.MEDNAME_ID as MedNameId,
	                              mn.MED_NAME as MedName
                          from MEDNAME_INFO mn 
                          inner join MEDHISTORY_INFO mh on mn.MEDNAME_ID = mh.MEDNAME_ID
                          inner join MEDCLASS_INFO mc on mn.MEDCLASS_ID = mc.MEDCLASS_ID
                          where mc.MEDCLASS_ID  IN (SELECT Item FROM @MedClassId) and patient_hisid = @PatientId order by mh.ITEM";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                var medclassid = cmd.Parameters.Add(new SqlParameter("@MedClassId", SqlDbType.Structured));
                cmd.Parameters.Add(new SqlParameter("@PatientId", data.PatientId));
                medclassid.TypeName = "IntArray";
                medclassid.Value = GetTVPValue<int>(MedclassIdInt);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return mapdata(dt);
        }
        private List<string> mapdata(DataTable InfoData)
        {
            List<string> resultModify = new List<string>();
            foreach (DataRow row in InfoData.Rows)
            {
                resultModify.Add(
                    row["MedName"].ToString()
                );
            }
            return resultModify;
        }

        /// <summary>
        /// GetMedNameData
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<Models.MedData> GetMedData(Models.MedData data)
        {
            string[] MedclassIdString = data.MedClassId.Split(',');
            int[] MedclassIdInt = new int[MedclassIdString.Length];
            for (int i = 0; i < MedclassIdString.Length; i++)
            {
                MedclassIdInt[i] = Convert.ToInt16(MedclassIdString[i]);
            }
            DataTable dt = new DataTable();
            string sql = @"select mn.MED_NAME as MedName,
						          Convert(varchar(10),mh.MED_STARTDATE,111)as MedStart,
						          mh.MED_SOURCE as MedSource
                          from MEDNAME_INFO mn 
                          inner join MEDHISTORY_INFO mh on mn.MEDNAME_ID = mh.MEDNAME_ID
                          inner join MEDCLASS_INFO mc on mn.MEDCLASS_ID = mc.MEDCLASS_ID
                          where mc.MEDCLASS_ID  IN (SELECT Item FROM @MedClassId) and patient_hisid = @PatientId order by mh.ITEM";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                var medclassid = cmd.Parameters.Add(new SqlParameter("@MedClassId", SqlDbType.Structured));
                cmd.Parameters.Add(new SqlParameter("@PatientId", data.PatientId));
                medclassid.TypeName = "IntArray";
                medclassid.Value = GetTVPValue<int>(MedclassIdInt);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return map(dt);
        }

        private List<Models.MedData> map(DataTable InfoData)
        {
            List<Models.MedData> resultModify = new List<MedData>();
            foreach (DataRow row in InfoData.Rows)
            {
                resultModify.Add(new MedData()
                {
                    MedName = row["MedName"].ToString(),
                    MedStart = row["MedStart"].ToString(),
                    MedSource = row["MedSource"].ToString()
                });
            }
            return resultModify;
        }

    }
}