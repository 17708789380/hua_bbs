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
namespace hua_bbs.Web.Section
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
		hua_bbs.BLL.SectionService bll=new hua_bbs.BLL.SectionService();
		hua_bbs.Model.Section model=bll.GetModel(id);
		this.lblid.Text=model.id.ToString();
		this.lblt_z_id.Text=model.t_z_id.ToString();
		this.lblt_u_id.Text=model.t_u_id.ToString();
		this.lblname.Text=model.name;
		this.lbllogo.Text=model.logo;

	}


    }
}
