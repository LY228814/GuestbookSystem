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
    public partial class show : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)//页面首次加载事件
            {
                HPageData();
            }
        }
        /// <summary>
        /// 绑定信息内容以及评论留言信息
        /// </summary>
        public void HPageData()
        {

            DataProcessing DP = new DataProcessing();
            DataRow hang = DP.Sda("select inf.*,us.Uname from Information as inf inner join Users as us on inf.Uid=us.Uid where Iid='" + Request["rzid"].ToString() + "'").Tables[0].Rows[0];
            biaoti.Text = hang["Title"].ToString();
            time.Text = hang["CreateTime"].ToString();
            num.Text = hang["SeeCount"].ToString();
            neirong.Text = hang["Contents"].ToString();
            name.Text = hang["Uname"].ToString();
            message();//加载留言信息方法
        }
        /// <summary>
        /// 加载信息的留言回复方法
        /// </summary>
        public void message()
        {
            DataProcessing DP = new DataProcessing();
            DataSet ds = DP.Sda("select Row_number() over(order by mes.CreateTime asc ) as xh,Mid,ImageUrl,Contents,mes.CreateTime,Uname from Messages as mes inner join Users on Users.Uid=mes.UId where Iid='" + Request["rzid"].ToString() + "'and Isdelete = 0 ");
            Repeater1.DataSource = ds;
            Repeater1.DataBind();
        }

        /// <summary>
        /// 判断文章是否是登录用的发布的
        /// 为否时,隐藏回复删除按钮
        /// 方法根据用户传过来的信息id进行查询留言
        /// 根据留言id进行查询回复内容然后进行绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataProcessing DP = new DataProcessing();
            string rzuid = DP.Sda("select Uid from Information where IId ='" + Request["rzid"].ToString() + "'").Tables[0].Rows[0]["Uid"].ToString();
            string yhuid = Session["Uid"].ToString();
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                if (rzuid != yhuid)
                {
                    (e.Item.FindControl("huifu") as LinkButton).Visible = false;
                    (e.Item.FindControl("delete") as LinkButton).Visible = false;
                }
                string Mid = (e.Item.FindControl("HF1") as HiddenField).Value;
                Repeater rep = e.Item.FindControl("Repeater2") as Repeater;//找到里层的repeater对象
                DataSet ds = DP.Sda("select Rid,Contents,Replys.createtime,Uname,ImageUrl from Replys inner join Users on Users.Uid=Replys.UId where Mid= " + Mid + " order by Replys.CreateTime asc");
                rep.DataSource = ds;
                rep.DataBind();
            }
        }

        /// <summary>
        /// 回复删除事件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "huifu")
            {
                Session["Hfid"] = e.CommandArgument.ToString();
                DataProcessing DP = new DataProcessing();
                string name = DP.Sda("select Uname from Messages inner join Users on Users.Uid=Messages.UId where Mid='" + Session["Hfid"].ToString() + "'").Tables[0].Rows[0]["Uname"].ToString();
                hfren.Text = "回复用户" + name + "的评论";
            }
            else
            {
                DataProcessing DP = new DataProcessing();
                int hs1 = DP.comm("delete  from Replys where Mid='" + e.CommandArgument.ToString() + "'");
                int hs2 = DP.comm("delete  from Messages where Mid='" + e.CommandArgument.ToString() + "'");
                if (hs1 + hs2 > 0)
                {
                    Response.Redirect(Request.Url.ToString());
                   // Response.Write("<script> alert('删除成功!');window.location.reload();</script>");  
                }
                else 
                {
                    Response.Write("<script> alert('删除失败!') </script>");
                }
            }
        }

        /// <summary>
        /// 留言回复事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Session["Hfid"] == null)
            {
                DataProcessing DP = new DataProcessing();
                int hs = DP.comm("insert into Messages values ( '" + PLText.InnerText.ToString() + "', '" + System.DateTime.Now.ToString() + "','" + Session["Uid"].ToString() + "','" + Request["rzid"].ToString() + "')");
                if (hs > 0)
                {
                    Response.Write("<script> alert('留言成功.') </script>");
                    message();
                    PLText.InnerText = null;
                }
                else
                {
                    Response.Write("<script> alert('留言失败,请重试!') </script>");
                }
            }
            else
            {
                DataProcessing DP = new DataProcessing();
                int hs = DP.comm("insert into Replys values ( '" + PLText.InnerText.ToString() + "','" + Session["Hfid"].ToString() + "', '" + System.DateTime.Now.ToString() + "','" + Session["Uid"].ToString() + "')");
                if (hs > 0)
                {
                    Response.Write("<script> alert('回复成功.') </script>");
                    message();
                    PLText.InnerText = null;
                }
                else
                {
                    Response.Write("<script> alert('回复失败,请刷新页面重试!') </script>");
                }
            }


        }

        /// <summary>
        /// 留言事件
        /// 该方法点击时提醒用户输入留言内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void plwz_Click(object sender, EventArgs e)
        {
            Session["Hfid"] = null;
            hfren.Text = "请输入留言内容";
        }
    }
}