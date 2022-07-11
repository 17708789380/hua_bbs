using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
namespace hua_bbs.Web.User
{
    public partial class Show : Page
    {        
        		public string strid=""; 
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					strid = Request.Params["id"];
					int id=(Convert.ToInt32(strid));
					ShowInfo(id);
				}
			}
		}
		
	private void ShowInfo(int id)
	{
		hua_bbs.BLL.UserService bll=new hua_bbs.BLL.UserService();
		hua_bbs.Model.User model=bll.GetModel(id);
		this.lblid.Text=model.id.ToString();
		this.lblemail.Text=model.email;
		this.lblface.Text=model.face;
		this.lblmobile.Text=model.mobile;
		this.lblnickname.Text=model.nickname;
		this.lblpassword.Text=model.password;
		this.lblregtime.Text=model.regtime.ToString();
		this.lblsex.Text=model.sex;
		this.lbltruename.Text=model.truename;
		this.lbltype.Text=model.type;

	}


    }
}
