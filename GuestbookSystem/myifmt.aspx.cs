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
    public partial class myifmt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["judge"] != null)//判断用户登录事件
                {
                    HPageData();
                }
                else
                {
                    Response.Write("<script>alert('您还没登录或登录过期,点击确认跳转登录页面');location.href='Login.aspx';</script>");
                }
            }
        }
        /// <summary>
        /// 加载个人的信息数据,并且渲染给页面
        /// </summary>
        public void HPageData()
        {
            int Uid = Convert.ToInt32(Session["Uid"].ToString());
            DataProcessing DP = new DataProcessing();
            DataSet ds = DP.Sda("select *  from Information where Isdelete=0 and Uid= " + Uid + "");
            Repeater1.DataSource = ds;
            Repeater1.DataBind();
        }
        /// <summary>
        /// 判断用户点击的事件.是修改还是发布
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "create")
            {
                DataProcessing DP = new DataProcessing();
                DP.comm("update  Information set SeeCount+=1  where Iid='" + e.CommandArgument + "'");
                Response.Redirect("show.aspx?rzid=" + Convert.ToInt32(e.CommandArgument) + "");
            }
            else if (e.CommandName == "update")
            {

                Response.Redirect("Information.aspx?rzid=" + Convert.ToInt32(e.CommandArgument) + "");
            }
        }

        /// <summary>
        /// 删除事件,删除用户勾选的信息
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
                        int Iid = Convert.ToInt32((Repeater1.Items[i].FindControl("HF1") as HiddenField).Value);
                        DataProcessing DP = new DataProcessing();
                        DP.comm("update  Information set Isdelete=1  where Iid='" + Iid + "';");
                        num++;
                    }
                }
                //Response.Redirect(Request.Url.ToString());
                Response.Write("<script>alert('成功删除" + num + "项')</script>");
                HPageData();
            }
            else
            {
                Response.Write("<script>alter('请选择删除项')</script>");
            }
        }

        /// <summary>
        /// 搜索事件,搜索与输入相关的标题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void search_Click(object sender, EventArgs e)
        {
            string keyw = keyword.Text.Trim();
            if (keyword.Text.Trim() != "")
            {
                DataProcessing DP = new DataProcessing();
                DataSet ds = DP.Sda("select inf.*,us.Uname from Information as inf inner join Users as us on inf.Uid=us.Uid where inf.isdelete = '0' and Title like '%" + keyword.Text.ToString() + "%'");
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
            }
        }
    }
}