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
    public partial class UserIM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HPageData();
            }
        }
        /// <summary>
        /// 加载用户信息列表
        /// </summary>
        public void HPageData()
        {
            DataProcessing DP = new DataProcessing();
            DataSet ds = DP.Sda("select * from Users where isdelete = 0");
            Repeater1.DataSource = ds;
            Repeater1.DataBind();
            string type = DP.sdauser(Convert.ToInt32( Session["Uid"]))["Type"].ToString();
            if (type != "1")
            {
                xuanze.Visible = false;//隐藏方法
               // delete.Visible = false;
            }
        }
        /// <summary>
        /// 点击查看用户资料
        /// 当用户为管理员或者用户本人时,可以查看修改用户资料
        /// 否则弹出用户没有权限
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            DataProcessing DP = new DataProcessing();
            int UUid = Convert.ToInt32(Session["Uid"]);
            int Uid = Convert.ToInt32(e.CommandArgument);
            int type = Convert.ToInt32( DP.sdauser(UUid)["Type"].ToString());
            
            if (type!=0|UUid==Uid)
            {
                Response.Redirect("personaldata.aspx?UId=" + e.CommandArgument.ToString());
            }
            else
            {
                Response.Write("<script>alert('抱歉.您没有权限')</script>");
            }
        }

        /// <summary>
        /// 删除用户信息时间
        /// 仅管理员可以操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void delete_Click(object sender, EventArgs e)
        {
            if (Repeater1.Items.Count > 0)
            {
                int num = 0;
                for (int i = 0; i < Repeater1.Items.Count; i++)
                {
                    CheckBox check = Repeater1.Items[i].FindControl("CheckBox") as CheckBox;
                    if (check.Checked == true)
                    {
                        int Uid = Convert.ToInt32((Repeater1.Items[i].FindControl("HF1") as HiddenField).Value);
                        DataProcessing DP = new DataProcessing();
                        DP.comm("update Users set Isdelete=1 where Uid=" + Uid + "");
                        num++;
                    }
                }
                Response.Write("<script>alert('成功删除" + num + "项')</script>");
                HPageData();
            }
            else
            {
                Response.Write("<script>alert('请选择删除项')</script>");
            }
        }
    }
}