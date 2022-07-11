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
namespace hua_bbs.Web.Reply
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
		hua_bbs.BLL.ReplyService bll=new hua_bbs.BLL.ReplyService();
		hua_bbs.Model.Reply model=bll.GetModel(id);
		this.lblid.Text=model.id.ToString();
		this.lblt_t_id.Text=model.t_t_id.ToString();
		this.lblt_u_id.Text=model.t_u_id.ToString();
		this.txtmodifytime.Text=model.modifytime.ToString();
		this.txtpublishtime.Text=model.publishtime.ToString();
		this.txtcontent.Text=model.content;
		this.txttitle.Text=model.title;

	}

		public void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(!PageValidate.IsDateTime(txtmodifytime.Text))
			{
				strErr+="modifytime格式错误！\\n";	
			}
			if(!PageValidate.IsDateTime(txtpublishtime.Text))
			{
				strErr+="publishtime格式错误！\\n";	
			}
			if(this.txtcontent.Text.Trim().Length==0)
			{
				strErr+="content不能为空！\\n";	
			}
			if(this.txttitle.Text.Trim().Length==0)
			{
				strErr+="title不能为空！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			int id=int.Parse(this.lblid.Text);
			int t_t_id=int.Parse(this.lblt_t_id.Text);
			int t_u_id=int.Parse(this.lblt_u_id.Text);
			DateTime modifytime=DateTime.Parse(this.txtmodifytime.Text);
			DateTime publishtime=DateTime.Parse(this.txtpublishtime.Text);
			string content=this.txtcontent.Text;
			string title=this.txttitle.Text;


			hua_bbs.Model.Reply model=new hua_bbs.Model.Reply();
			model.id=id;
			model.t_t_id=t_t_id;
			model.t_u_id=t_u_id;
			model.modifytime=modifytime;
			model.publishtime=publishtime;
			model.content=content;
			model.title=title;

			hua_bbs.BLL.ReplyService bll=new hua_bbs.BLL.ReplyService();
			bll.Update(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"保存成功！","list.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
