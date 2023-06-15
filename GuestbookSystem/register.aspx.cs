using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GuestbookSystem
{
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        /// <summary>
        /// 获取页面用户输入的数据
        /// </summary>
        /// <returns></returns>
        public string fromnum()
        {
            string name = Uname.Text.ToString();
            string pwd = Upwd.Text.ToString();
            string sex = Usex.SelectedValue;
            //头像地址
            string hpdz = Pictureupload();
            string phone = Uphone.Text.ToString();
            string Uxinxi = "'" + name + "','" + pwd + "','" + sex + "','" + phone + "','" + hpdz + "','" + 0 + "','" + System.DateTime.Now.ToString() + "',0 ";
            return Uxinxi;
        }
        /// <summary>
        /// 用户头像上传事件
        /// </summary>
        /// <returns></returns>
        public string Pictureupload()
        {
            string time = System.DateTime.Now.ToString("yyyMMdd");//获取当前时间
            string name = picture.FileName;//获取上传文件名称
            string exname = name.Substring(name.LastIndexOf('.'));//截取文件后缀
            string path = Server.MapPath("headimg") + "\\" + Uname.Text.ToString() + time + exname;//图片保存地址.填绝对路径
            picture.SaveAs(path);//把图片保存到该位置
            string URL = "headimg/" + Uname.Text.ToString() + time + exname;//在数据库中保存相对路径
            return URL;
        }

        /// <summary>
        /// 用户注册,成功进入登录页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void zhuce_Click(object sender, EventArgs e)
        {
            DataProcessing DP = new DataProcessing();//实例化数据处理类
            int fh = DP.comm("insert into Users values (" + fromnum() + ");");
            if (fh > 0)
            {
                Response.Write("<script> alert('注册成功,点击确认前往登录!');window.location.href='Login.aspx'</script>");
            }
            Response.Write("<script>alert('出现问题了,请刷新重试注册')</script>");
        }
    }
}