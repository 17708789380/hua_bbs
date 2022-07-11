using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication.admin
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();//清除所有数据
            Response.Cookies["cp1"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["cp2"].Expires = DateTime.Now.AddDays(-1);
            Response.Redirect("/Index.aspx");
        }
    }
}