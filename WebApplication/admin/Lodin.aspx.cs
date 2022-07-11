using hua_bbs.BLL;
using hua_bbs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication.admin
{
    public partial class Lodin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
            {
                int username_id = int.Parse(Request.Form["login"]);
                string userpass = Request.Form["password"];

                 
                UserService userService = new UserService();

                if(userService.CheckLogin(username_id, userpass))
                {
                    Response.Redirect("/Index.aspx");
                }

            }

        }
    }
}