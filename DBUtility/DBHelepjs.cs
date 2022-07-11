using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hua_bbs.DBUtility
{
   public class DBHelepjs
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private static string connstu = "server=DESKTOP-F83QS3N\\YXN;database=bbs;Integrated Security=true";
        /// <summary>
        /// 数据库连接
        /// </summary>
        public static SqlConnection conn = null;
        /// <summary>
        /// 数据库连接初始化
        /// </summary>
        public static void zong()
        {
            if (conn == null)
            {
                conn = new SqlConnection(connstu);
            }
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }
        /// <summary>
        /// 根据sql语句查询数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string sql)
        {
            zong();
            SqlDataAdapter dap = new SqlDataAdapter(sql, conn);
            DataTable table = new DataTable();
            dap.Fill(table);
            return table;
        }
        /// <summary>
        /// 用于数据表的新增、修改、删除的数据操作
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static bool ExcuteSql(string sql)
        {
            zong();
            SqlCommand comd = new SqlCommand(sql, conn);
            int r = comd.ExecuteNonQuery();
            conn.Close();
            return r > 0 ? true : false;
        }
    }
}
