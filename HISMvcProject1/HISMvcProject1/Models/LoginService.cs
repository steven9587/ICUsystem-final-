using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HISMvcProject1.Models
{
    public class LoginService
    {
        string result;
        /// <summary>
        /// 取得DB連線字串
        /// </summary>
        /// <returns></returns>
        private string GetDBConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }
        
        /// <summary>
        /// UserLogin
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public String UserLogin(Models.LoginData data)
        {
            
            DataTable dt = new DataTable();
            string sql = @" SELECT USER_ID, USER_ACCOUNT, USER_PASSWORD,USER_NAME as UserName
                           FROM USER_LOGIN
                           WHERE USER_ACCOUNT = @UserAccount AND
                                   User_Password = @Password;";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@UserAccount", data.UserAccount));
                cmd.Parameters.Add(new SqlParameter("@Password", data.Password));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }

            
            foreach (DataRow row in dt.Rows)
            {
                result = (row["UserName"].ToString());

            }
            return result;
        }
    }
}