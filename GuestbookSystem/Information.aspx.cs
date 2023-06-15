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
    public partial class Information : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["rzid"] != null)//判断用户是查看信息还是发布信息
                {
                    chakan();//
                    commit.CommandName = "update";
                    commit.Text = "确认修改>";
                }
            }
        }
        /// <summary>
        /// 查看信息
        /// 给页面绑定用户要看的信息
        /// </summary>
        public void chakan()
        {
            DataProcessing DP = new DataProcessing();
            DataSet ds = DP.Sda("select * from Information where IId='" + Request["rzid"].ToString() + "'");
            RXbt.Text = ds.Tables[0].Rows[0]["Title"].ToString();
            RZwb.InnerText = ds.Tables[0].Rows[0]["Contents"].ToString();
        }

        /// <summary>
        /// 判断用户点击的事件.是修改还是发布
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void commit_Click(object sender, EventArgs e)
        {
            if (commit.CommandName == "update")
            {
                update();//进入修改事件
            }
            else
            {
                insert();//进入添加发布事件
            }
        }

        /// <summary>
        /// 添加发布事件
        /// </summary>
        public void insert()
        {
            string rzbt = RXbt.Text.ToString();
            string rzwb = RZwb.InnerText.ToString();
            DataProcessing DP = new DataProcessing();
            int hs = DP.comm("insert into Information values ('" + rzbt + "','" + rzwb + "','" + Session["Uid"].ToString() + "','" + System.DateTime.Now.ToString() + "',1,0)");
            if (hs > 0)
            {
                Response.Write("<script>alert('添加成功');location.href='Index.aspx';</script>");
            }
            else
            {
                Response.Write("<script>alert('添加失败,请重试!')</script>");
            }
        }
        /// <summary>
        /// 修改信息事件
        /// </summary>
        public void update()
        {
            string rzbt = RXbt.Text.ToString();
            string rzwb = RZwb.InnerText.ToString();
            DataProcessing DP = new DataProcessing();
            int hs = DP.comm("update Information set Title='" + rzbt + "',Contents='" + rzwb + "'where IId='" + Request["rzid"].ToString() + "'");
            if (hs > 0)
            {
                Response.Write("<script>alert('修改成功');location.href='Index.aspx';</script> ");
            }
            else
            {
                Response.Write("<script>alert('修改失败,请重试!')</script>");
            }
        }

       

       
    }
}