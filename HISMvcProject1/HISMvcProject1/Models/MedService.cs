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
        public List<Models.MedData> GetMedDataByMedName(Models.MedData data)
        {
           
            DataTable dt = new DataTable();
            string sql = @"select mh.Item as Item,
                                  mh.MEDNAME_ID as MedNameId,
	                              mn.MED_NAME as MedName,
	                              mc.MEDCLASS_ID as MedClassId,
	                              mc.MED_CLASS as MedClass,
	                              Convert(varchar(10),mh.MED_STARTDATE,20)as MedStart,
	                              Convert(varchar(10),mh.MED_OVERDATE,20)as MedEnd,
								  mh.MED_DAY as MedDay,
	                              mh.MED_SOURCE as MedSource,
								  mh.MED_DOSAGE as MAmount,
								  mh.MED_UNIT as MUnit,
                                  mh.MED_WAY as MedWay,
								  mh.MED_FREQUENCY as MFrequency,
								  mh.MED_TOTAL as MTotal,
								  mh.DIVISION_CNAME as MBranch,
								  mh.DOCTOR_NAME as MDoctor
                          from MEDNAME_INFO mn 
                          inner join MEDHISTORY_INFO mh on mn.MEDNAME_ID = mh.MEDNAME_ID
                          inner join MEDCLASS_INFO mc on mn.MEDCLASS_ID = mc.MEDCLASS_ID
                          where mn.MED_NAME = @MedName
						  and  Convert(varchar(10),mh.MED_STARTDATE,20) = @MedStart
                          order by mh.ITEM";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@MedName", data.MedName));
                cmd.Parameters.Add(new SqlParameter("@MedStart", data.MedStart));
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
                    MedName = row["MedName"].ToString(),
                    MedStart = row["MedStart"].ToString(),
                    MedSource = row["MedSource"].ToString(),
                    MedClass = row["MedClass"].ToString(),
                    MedEnd = row["MedEnd"].ToString(),
                    MedDay = row["MedDay"].ToString(),
                    MUnit = row["MUnit"].ToString(),
                    MAmount = row["MAmount"].ToString(),
                    MedWay = row["MedWay"].ToString(),
                    MFrequency = row["MFrequency"].ToString(),
                    MTotal = row["MTotal"].ToString(),
                    MedDivision = row["MBranch"].ToString(),
                    MDoctor = row["MDoctor"].ToString(),
                    Item = (int)row["Item"]
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
                          where mc.MEDCLASS_ID  IN (SELECT Item FROM @MedClassId) and patient_hisid = @PatientId order by mh.MEDNAME_ID ";
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
            string sql = @"select mh.Item as Item,
                                  mh.MEDNAME_ID as MedNameId,
	                              mn.MED_NAME as MedName,
	                              mc.MEDCLASS_ID as MedClassId,
	                              mc.MED_CLASS as MedClass,
	                              Convert(varchar(10),mh.MED_STARTDATE,20)as MedStart,
	                              Convert(varchar(10),mh.MED_OVERDATE,20)as MedEnd,
								  mh.MED_DAY as MedDay,
	                              mh.MED_SOURCE as MedSource,
								  mh.MED_DOSAGE as MAmount,
								  mh.MED_UNIT as MUnit,
								  mh.MED_FREQUENCY as MFrequency,
								  mh.MED_TOTAL as MTotal,
								  mh.DIVISION_CNAME as MBranch,
								  mh.DOCTOR_NAME as MDoctor
                          from MEDNAME_INFO mn 
                          inner join MEDHISTORY_INFO mh on mn.MEDNAME_ID = mh.MEDNAME_ID
                          inner join MEDCLASS_INFO mc on mn.MEDCLASS_ID = mc.MEDCLASS_ID
                          where mc.MEDCLASS_ID  IN (SELECT Item FROM @MedClassId) 
						  and mh.PATIENT_HISID = @PatientId
						  and mh.MED_STARTDATE between @MedStart and @MedEnd order by mh.MED_STARTDATE desc";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                var medclassid = cmd.Parameters.Add(new SqlParameter("@MedClassId", SqlDbType.Structured));
                cmd.Parameters.Add(new SqlParameter("@PatientId", data.PatientId));
                cmd.Parameters.Add(new SqlParameter("@MedStart", data.MedStart));
                cmd.Parameters.Add(new SqlParameter("@MedEnd", data.MedEnd));
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
                var source = "";
                if (row["MedSource"].ToString() == "門診")
                {
                    source = "1";
                }
                if (row["MedSource"].ToString() == "急診")
                {
                    source = "3";
                }
                if (row["MedSource"].ToString() == "住院")
                {
                    source = "5";
                }
                resultModify.Add(new MedData()
                {
                    MedName = row["MedName"].ToString(),
                    MedStart = row["MedStart"].ToString(),
                    MedSource = source,
                    MedClass = row["MedClass"].ToString(),
                    MedEnd = row["MedEnd"].ToString(),
                    MedDay = row["MedDay"].ToString(),
                    MUnit = row["MUnit"].ToString(),
                    MAmount = row["MAmount"].ToString(),
                    MFrequency = row["MFrequency"].ToString(),
                    MTotal = row["MTotal"].ToString(),
                    MedDivision = row["MBranch"].ToString(),
                    MDoctor = row["MDoctor"].ToString(),
                    Item = (int)row["Item"]
                });
            }
            return resultModify;
        }
      
    }
}