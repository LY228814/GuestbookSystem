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
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)//页面首次加载
            {

                if (Session["judge"] != null)//判断用户是否denglu
                {
                    HPageData();//如果已经登录.加载页面数据
                }
                else//如果用户没登录,则跳转之登录页
                {
                    Response.Write("<script>alert('您还没登录或登录过期,点击确认跳转登录页面');location.href='Login.aspx';</script>");
                }
            }
        }
        /// <summary>
        /// 加载页面数据方法
        /// </summary>
        public void HPageData()
        {
            DataProcessing DP = new DataProcessing();//连接数据库
            DataSet ds = DP.Sda("select inf.*,us.Uname from Information as inf inner join Users as us on inf.Uid=us.Uid where inf.Isdelete=0");//查询数据
            reptinf(ds);//把数据交给数据处理方法
        }

        /// <summary>
        /// 处理传过来的数据,渲染给页面
        /// </summary>
        /// <param name="ds"></param>
        public void reptinf(DataSet ds)
        {
            DataProcessing DP = new DataProcessing();
            Repeater1.DataSource = ds;
            Repeater1.DataBind();
            
            string type = DP.sdauser(Convert.ToInt32(Session["Uid"]))["Type"].ToString();//查询用户的权限
            if (type != "1")
            {
                xuanze.Visible = false;//当为普通用户是,隐藏删除按钮
            }
        }
        /// <summary>
        /// Repeater1的ItemCommand时间
        /// 前天页面点击查看或者修改时进入次方法
        /// </summary>

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //判断用户点击的是查看或修改除,进入相应的事件
            if (e.CommandName == "create")//查看
            {
                DataProcessing DP = new DataProcessing();
                DP.comm("update  Information set SeeCount+=1  where Iid='" + e.CommandArgument + "'");//查看次数+1
                Response.Redirect("show.aspx?rzid=" + Convert.ToInt32(e.CommandArgument) + "");
            }
            else if (e.CommandName == "update")//修改
            {

                Response.Redirect("Information.aspx?rzid=" + Convert.ToInt32(e.CommandArgument) + "");
            }
        }
        /// <summary>
        /// Repeater1的ItemDataBound逐行判断
        /// 当信息不是自己的时候隐藏修改按钮,防止别人修改自己的信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string Uid = Session["Uid"].ToString();
            DataProcessing DP = new DataProcessing();
            string type = DP.sdauser(Convert.ToInt32(Session["Uid"]))["Type"].ToString();//查询用户的权限
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                if ( (e.Item.FindControl("HF1") as HiddenField).Value!=Uid )
                {
                    (e.Item.FindControl("update") as LinkButton).Visible = false;
                }
                 if (type == "1")
                {
                    (e.Item.FindControl("update") as LinkButton).Visible = true;
                }

            }
        }


        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void delete_Click(object sender, EventArgs e)
        {
            if (Repeater1.Items.Count > 0)
            {
                int num = 0;
                for (int i = 0; i < Repeater1.Items.Count; i++)//遍历每一行,删除勾选的信息*管理员使用*
                {
                    CheckBox check = Repeater1.Items[i].FindControl("CheckBox") as CheckBox;
                    if (check.Checked == true)
                    {
                        int Iid = Convert.ToInt32((Repeater1.Items[i].FindControl("HF1") as HiddenField).Value);
                        DataProcessing DP = new DataProcessing();
                        DP.comm("update  Information set Isdelete=1  where Iid='"+Iid+"';");
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
        /// 搜索方法
        /// 当用户点击搜索是进入该方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void search_Click(object sender, EventArgs e)
        {
            string keyw = keyword.Text.Trim();//获取输入值
            if (keyword.Text.Trim() != "")
            {
                DataProcessing DP = new DataProcessing();
                DataSet ds = DP.Sda("select inf.*,us.Uname from Information as inf inner join Users as us on inf.Uid=us.Uid where inf.isdelete = '0' and Title like '%" + keyword.Text.ToString() + "%'");
                reptinf(ds);//查询结果交给数据处理方法
            }
        }
    }
}