using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace GuestbookSystem
{
    /// <summary>
    /// DataProcessing 数据库对接数据处理类
    /// </summary>
    public class DataProcessing
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString);
        /// <summary>
        /// 数据库查询
        /// </summary>
        /// <param name="sql">传入sql查询语句</param>
        /// <returns>返回数据库中所查询到的数据</returns>
        public DataSet Sda(string sql)
    {
        SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
        DataSet ds = new DataSet();
        sda.Fill(ds);
        return ds;
    }
    /// <summary>
    /// 数据库增删改
    /// </summary>
    /// <param name="sql">传入sql增删改语句</param>
    /// <returns>返回数据库返回的行数</returns>
    public int comm(string sql)
    {
        SqlCommand comm = new SqlCommand(sql, conn);
        conn.Open();
        int fh = comm.ExecuteNonQuery();
        conn.Close();
        return fh;
    }

        /// <summary>
        /// 通过Uid查询用户信息
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns>返回一个DataRow 里面存放着用户的信息</returns>
        public DataRow sdauser(int id)
    {
        string sql = "select * from Users where Uid=" + id + "";
        SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
        DataSet ds = new DataSet();
        sda.Fill(ds);
        return ds.Tables[0].Rows[0];
    }
   }
}