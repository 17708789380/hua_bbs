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
namespace hua_bbs.Web.User
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
		hua_bbs.BLL.UserService bll=new hua_bbs.BLL.UserService();
		hua_bbs.Model.User model=bll.GetModel(id);
		this.lblid.Text=model.id.ToString();
		this.txtemail.Text=model.email;
		this.txtface.Text=model.face;
		this.txtmobile.Text=model.mobile;
		this.lblnickname.Text=model.nickname;
		this.txtpassword.Text=model.password;
		this.txtregtime.Text=model.regtime.ToString();
		this.txtsex.Text=model.sex;
		this.txttruename.Text=model.truename;
		this.txttype.Text=model.type;

	}

		public void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtemail.Text.Trim().Length==0)
			{
				strErr+="email不能为空！\\n";	
			}
			if(this.txtface.Text.Trim().Length==0)
			{
				strErr+="face不能为空！\\n";	
			}
			if(this.txtmobile.Text.Trim().Length==0)
			{
				strErr+="mobile不能为空！\\n";	
			}
			if(this.txtpassword.Text.Trim().Length==0)
			{
				strErr+="password不能为空！\\n";	
			}
			if(!PageValidate.IsDateTime(txtregtime.Text))
			{
				strErr+="regtime格式错误！\\n";	
			}
			if(this.txtsex.Text.Trim().Length==0)
			{
				strErr+="sex不能为空！\\n";	
			}
			if(this.txttruename.Text.Trim().Length==0)
			{
				strErr+="truename不能为空！\\n";	
			}
			if(this.txttype.Text.Trim().Length==0)
			{
				strErr+="type不能为空！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			int id=int.Parse(this.lblid.Text);
			string email=this.txtemail.Text;
			string face=this.txtface.Text;
			string mobile=this.txtmobile.Text;
			string nickname=this.lblnickname.Text;
			string password=this.txtpassword.Text;
			DateTime regtime=DateTime.Parse(this.txtregtime.Text);
			string sex=this.txtsex.Text;
			string truename=this.txttruename.Text;
			string type=this.txttype.Text;


			hua_bbs.Model.User model=new hua_bbs.Model.User();
			model.id=id;
			model.email=email;
			model.face=face;
			model.mobile=mobile;
			model.nickname=nickname;
			model.password=password;
			model.regtime=regtime;
			model.sex=sex;
			model.truename=truename;
			model.type=type;

			hua_bbs.BLL.UserService bll=new hua_bbs.BLL.UserService();
			bll.Update(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"保存成功！","list.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
