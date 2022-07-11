using hua_bbs.BLL;
using hua_bbs.DAL;
using hua_bbs.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication.admin
{
    public partial class Login : System.Web.UI.Page
    {
        public string error { set; get; }
        public UserDao userDao { set; get; }

        public string nickName;
        public string passWord;

        public string nickName1;
        public string passWord1;


        protected void Page_Load(object sender, EventArgs e)
        {
            //HttpCookie cookie1 = null;
            //HttpCookie cookie2 = null;

            //if (this.IsPostBack)
            //{ 
            //        #region 登陆的代码
            //        nickName = Request.Form["user_nickName"];
            //        passWord = Request.Form["user_password"];
            //        string imageCode = Request.Form["imageCode"];

            //        Session["Username_session"] = nickName;  //(给其他地方使用， 相当于全局变量)


            //    //如果账号框有字符串，则清空

            //        if (Session["code"].ToString().Equals(imageCode))  //什么意思？   Session["code"]是在admin/ValidateCode.ashx.cs 里面定义的数据
            //        {
            //            UserService userService = new UserService();
            //            List<User> user = userService.GetModelList("nickname='" + nickName + "' and password='" + passWord + "'");
            //            if (user.Count > 0)
            //            {
            //                User u = user[0];
            //                Session["user"] = u;

            //            //*************************************************************************************\
            //            //使用cookie
            //            //判断用户是否选择了记住我
            //            if (!string.IsNullOrEmpty(Request.Form["CheckMe"]))   //判断是否打钩记住了我？
            //            {
            //                //避免中文乱码，需要进行转码
            //                cookie1 = new HttpCookie("cp1", Server.UrlEncode(nickName));  //创建cookie
            //                cookie2 = new HttpCookie("cp2", Server.UrlEncode(passWord));
            //                Response.Cookies.Add(cookie1);   //将cookie存入到浏览器
            //                Response.Cookies.Add(cookie2);
            //                cookie1.Expires = DateTime.Now.AddDays(7);  //设置过期时间
            //                cookie2.Expires = DateTime.Now.AddDays(7);
            //            }
            //            //*************************************************************************************

            //            Response.Redirect("/Index.aspx");
            //            }
            //            else
            //            {
            //                error = "您的用户名或者密码错误请重新输入!";
            //            }
            //        }
            //        else
            //        {
            //            error = "您的验证码错误!";
            //        }
            //#endregion
            //}
            //else
            //{
            //    CheckCookieInfo();
            //}

            if (!IsPostBack)
            {
                Initinfo();
            }
        }



        protected void CheckCookieInfo()
        {
            nickName = Request.Form["user_nickName"];
            passWord = Request.Form["user_password"];
            string imageCode = Request.Form["imageCode"];

            //先判断验证码
            if (Session["code"].ToString().Equals(imageCode))
            {
                //判断Cookie中是否有值
                //if (Request.Cookies["cp1"] != null && Request.Cookies["cp2"] != null)
                //{
                //    nickName1 = Request.Cookies["cp1"].Value;
                //    passWord1 = Request.Cookies["cp2"].Value;


                Session["Username_session"] = nickName;  //(给其他地方使用， 相当于全局变量)

            UserService userService = new UserService();
            List<User> user = userService.GetModelList("nickname='" + nickName + "' and password='" + passWord + "'");
            if (user.Count > 0)
            {
                User u = user[0];
                Session["user"] = u;

                if (u.nickname == nickName && u.password == passWord)
                {
                    Response.Redirect("/Index.aspx");
                }
                else
                {
                    error = "您的用户名或者密码错误请重新输入!";
                    //Response.Cookies["cp1"].Expires = DateTime.Now.AddDays(-1);
                    //Response.Cookies["cp2"].Expires = DateTime.Now.AddDays(-1);
                }
            }
            else
            {
                error = "您的用户名或者密码错误请重新输入!";
                //nickName1 = Request.Cookies["cp1"].Value;
                //passWord1 = Request.Cookies["cp2"].Value;
            }
            }
            else
                {
                error = "您的验证码错误!";
            }


        }


        public void Initinfo()
        {
            if (Request.Cookies["cp1"] != null && Request.Cookies["cp2"] != null)
            {
                //判断浏览器里面是否有上一次保存的账号、密码字符串，如果有则解码
                //解码
                nickName1 =Server.UrlDecode(Request.Cookies["cp1"].Value);
                passWord1 =Server.UrlDecode(Request.Cookies["cp2"].Value); 
            }
            else
            {
                //没有的话则为空字符串
                nickName1 ="";
                passWord1 = "";
            }
        }
        protected void btn_submit_Click(object sender, EventArgs e)
        {
            CheckCookieInfo();
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            HttpCookie cookie1 = null;
            HttpCookie cookie2 = null;

            //解码？


           
            if (CheckBox1.Checked == true)
            {
                if (!string.IsNullOrEmpty(Request.Form["CheckMe"]))   //判断是否打钩记住了我？
                {
                    //避免中文乱码，需要进行转码
                    cookie1 = new HttpCookie("cp1", Server.UrlEncode(nickName));  //创建cookie
                    cookie2 = new HttpCookie("cp2", Server.UrlEncode(passWord));
                    Response.Cookies.Add(cookie1);   //将cookie存入到浏览器
                    Response.Cookies.Add(cookie2);
                    cookie1.Expires = DateTime.Now.AddDays(7);  //设置过期时间
                    cookie2.Expires = DateTime.Now.AddDays(7);
                }
            }
        }
    }
}