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
using Maticsoft.Common;
using LTP.Accounts.Bus;
namespace hua_bbs.Web.Section
{
    public partial class Modify : Page
    {       

        		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					int id=(Convert.ToInt32(Request.Params["id"]));
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
		this.txtname.Text=model.name;
		this.txtlogo.Text=model.logo;

	}

		public void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtname.Text.Trim().Length==0)
			{
				strErr+="name不能为空！\\n";	
			}
			if(this.txtlogo.Text.Trim().Length==0)
			{
				strErr+="logo不能为空！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			int id=int.Parse(this.lblid.Text);
			int t_z_id=int.Parse(this.lblt_z_id.Text);
			int t_u_id=int.Parse(this.lblt_u_id.Text);
			string name=this.txtname.Text;
			string logo=this.txtlogo.Text;


			hua_bbs.Model.Section model=new hua_bbs.Model.Section();
			model.id=id;
			model.t_z_id=t_z_id;
			model.t_u_id=t_u_id;
			model.name=name;
			model.logo=logo;

			hua_bbs.BLL.SectionService bll=new hua_bbs.BLL.SectionService();
			bll.Update(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"保存成功！","list.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
