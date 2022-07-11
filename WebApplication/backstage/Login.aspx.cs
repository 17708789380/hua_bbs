using hua_bbs.BLL;
using hua_bbs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication.backstage
{
    public partial class Login : System.Web.UI.Page
    {
        public string msg { set; get; }
        public string nickname { set; get; }
        public string password { set; get; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
            {


                nickname = Request["nickName"];
                password = Request["password"];
                UserService userService = new UserService();
                List<User> userList = userService.GetModelList("type='3' and nickname='" + nickname + "' and password='" + password + "'");

                if (userList.Count > 0)
                {
                    Session["adminuser"] = userList[0];
                    Session["usename"] = userList[0].nickname;
                    Response.Redirect("/backstage/Main.aspx");
                }
                else
                {
                    msg = "你的用户名或者密码输入错误";
                }


            }

        }
    }
}