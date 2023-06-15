using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace GuestbookSystem
{
    public partial class NavMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Uid"] != null)
                {
                    HPageData();
                }
                else
                {
                    Response.Write("<script>alert('点击跳转确认跳转登录');location.href='login.aspx'</script>");
                }

            }
        }
        /// <summary>
        /// 导航用户信息加载，性别头像
        /// </summary>
        public void HPageData()
        {
            int Uid = Convert.ToInt32( Session["Uid"].ToString());
            DataProcessing DP = new DataProcessing();
            DataRow dsrow = DP.sdauser(Uid);
            name.Text = dsrow["Uname"].ToString();
            Gendar.Text = dsrow["Gendar"].ToString() == "0" ? "先生" : "女士";//性别
            string Url = dsrow["ImageUrl"].ToString();
            Imagname.ImageUrl = Url;
        }

        /// <summary>
        /// 注销登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ZXDL_Click(object sender, EventArgs e)
        {
            Session.Remove("judge");
            Session.Remove("Uid");
            Response.Redirect("Login.aspx");
        }



    }

}