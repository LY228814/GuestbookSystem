using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace GuestbookSystem
{
    /// <summary>
    /// 一般处理程序,通过ajax异步查询
    /// 检测注册账号是否已存在
    /// </summary>
    public class EvHa : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {

            string name = context.Request["name"].ToString();//接受前台页面传过来的用户名
            DataProcessing DZ = new DataProcessing();//连接数据库
            DataSet ds = new DataSet();
            ds = DZ.Sda("select * from Users where Uname='" + name + "'");//查询用户名是否存在
            int hs = ds.Tables[0].Rows.Count;
            if (hs > 0)//如果存在就返回true
            {
                context.Response.Write(true);
            }
            else
            {
                context.Response.Write(false);
            }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}