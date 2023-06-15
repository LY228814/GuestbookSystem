using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace GuestbookSystem
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)//页面首次加载
            {

            }
        }

        /// <summary>
        /// 登陆事件
        /// 查询用户密码并判断和输入的密码是否一致.并登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            DataProcessing DP = new DataProcessing();
            DataSet ds = DP.Sda("select * from Users where Uname='" + Textname.Text.ToString().Trim() + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                string pwd = ds.Tables[0].Rows[0]["Upwd"].ToString();
                if (Textpwd.Text.ToString() == pwd)
                {
                    Session["judge"] = "yidenglu";
                    Session["Uid"] = ds.Tables[0].Rows[0]["Uid"].ToString();
                    Response.Redirect("Index.aspx");
                }
                else
                {
                    Response.Write("<script> alert('账号或密码不正确,请重试') </script>");
                }
            }
            else
            {
                Response.Write("<script> alert('账号不存在,请核对后重试') </script>");
            }
           
        }
    }
}