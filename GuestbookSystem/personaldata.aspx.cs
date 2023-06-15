using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace GuestbookSystem
{
    /// <summary>
    /// 用户个人资料查看和修改
    /// </summary>
    public partial class personaldata : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["judge"] != null)
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
        /// 接受用户的id
        /// </summary>
        /// <returns></returns>
        private int Uid() {
            if (Request["Uid"] != null)
            {
                return  Convert.ToInt32(Request["Uid"]);
            }
            else
            {
                return  Convert.ToInt32(Session["Uid"]);
            }
        }
        /// <summary>
        /// 给页面绑定用户的个人资料
        /// </summary>
        public void HPageData()
        {
            DataProcessing DP = new DataProcessing();             
            DataRow dsrow = DP.sdauser(Uid());
            Uname.Text = dsrow["Uname"].ToString();
            Upwd.Text = dsrow["Upwd"].ToString();
            Usex.SelectedValue = dsrow["Gendar"].ToString();
            Uphone.Text = dsrow["Phone"].ToString();
           // UImage.ImageUrl = dsrow["ImageUrl"].ToString();
        }
        /// <summary>
        /// 获取表单的输入值
        /// </summary>
        public string fromnum()
        {

            string name = Uname.Text.ToString();
            string pwd = Upwd.Text.ToString();
            string sex = Usex.SelectedValue;
            string phone = Uphone.Text.ToString();
            string Uxinxi = "Uname='" + name + "',Upwd='" + pwd + "',Gendar='" + sex + "', Phone='" + phone + "' where Uid='" +Uid() + "'";
            return Uxinxi;
        }

        /// <summary>
        /// 头像上传图片事件
        /// </summary>
        public void Pictureupload()
        {
            if (picture.FileName != "")
            {
                DataProcessing DP = new DataProcessing();
                DataSet ds = DP.Sda("select ImageUrl from Users where Uid='" + Uid() + "'");
                string Imgurl = ds.Tables[0].Rows[0]["ImageUrl"].ToString();
                int shu = Imgurl.LastIndexOf('/');
                string url = Imgurl.Substring(shu).ToString();
                picture.SaveAs(Server.MapPath("headimg") + "\\" + url);//把图片保存到该位置
            }
        }
        /// <summary>
        /// 用户资料修改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            DataProcessing DZ = new DataProcessing();
            Pictureupload();
            int fh = DZ.comm("update Users set " + fromnum() + ";");
            if (fh > 0)
            {
                HPageData();
                Response.Write("<script>alert('修改成功');</script>");
            }
            else
            {
                Response.Write("<script>alert('修改失败,请刷新重试')</script>");
            }
        }
    }
}