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
    public partial class UserInfo : System.Web.UI.Page
    {
        public User user { set; get; }
        public UserDao userDao { set; get; }
        public UserService userService { set; get; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                user = new User();
                userDao = new UserDao();
                userService = new UserService();


                string temp_user = Session["Username_session"].ToString(); ;
                DataSet dataSet = userService.GetList("nickname ='" + temp_user + "'");
                user = userDao.DataRowToModel(dataSet.Tables[0].Rows[0]);



            }

        }
    }
}