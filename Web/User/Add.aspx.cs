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
    public partial class Add : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                       
        }

        		protected void btnSave_Click(object sender, EventArgs e)
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
			if(this.txtnickname.Text.Trim().Length==0)
			{
				strErr+="nickname不能为空！\\n";	
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
			string email=this.txtemail.Text;
			string face=this.txtface.Text;
			string mobile=this.txtmobile.Text;
			string nickname=this.txtnickname.Text;
			string password=this.txtpassword.Text;
			DateTime regtime=DateTime.Parse(this.txtregtime.Text);
			string sex=this.txtsex.Text;
			string truename=this.txttruename.Text;
			string type=this.txttype.Text;

			hua_bbs.Model.User model=new hua_bbs.Model.User();
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
			bll.Add(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"保存成功！","add.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
