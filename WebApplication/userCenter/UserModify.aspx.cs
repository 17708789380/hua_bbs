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

namespace WebApplication.userCenter
{
    public partial class UserModify : System.Web.UI.Page
    {
        public User user { set; get; }
        public UserDao userDao { set; get; }
        public UserService userService { set; get; }
        protected void Page_Load(object sender, EventArgs e)
        {
            user = new User();
            userDao = new UserDao();
            userService = new UserService();

            if (!this.IsPostBack)
            {
                string temp_user = Session["Username_session"].ToString(); ;
                DataSet dataSet = userService.GetList("nickname ='" + temp_user + "'");
                user = userDao.DataRowToModel(dataSet.Tables[0].Rows[0]);
            }
            else
            {
                user.id = int.Parse(Request.Form["user.id"]);
                user.regtime = DateTime.Parse(Request.Form["user.regTime"]);
                user.type =  Request.Form["user.type"];
                user.nickname = Request.Form["user.nickName"];
                user.truename = Request.Form["user.trueName"];
                user.sex = Request.Form["user.sex"];
                user.face = Request.Form["user.face"];
                user.password = Request.Form["user.password"];
                string rePassWord = Request.Form["rePassWord"];
                user.mobile = Request.Form["user.mobile"];
                user.email = Request.Form["user.email"];

                if(rePassWord == user.password)
                {
                    userService.Update(user);
                }
            }
        }
    }
}