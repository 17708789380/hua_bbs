using hua_bbs.BLL;
using hua_bbs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//检测该用户名是否已经存在
namespace WebApplication.admin
{
    public partial class ExistUserWithUserName : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                string nickName = Request["nickName"];

                UserService userService = new UserService();
                List<User> userList = userService.GetModelList("nickname='" + nickName + "'");

                if (userList.Count > 0)
                {
                    Response.Write(false);//用户存在
                Response.End();
                }
                else
                {
                    Response.Write(true);//用户可以使用
                Response.End();
            }
            
           
        }
    }
}