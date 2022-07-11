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
    public partial class Add : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                       
        }

        		protected void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(!PageValidate.IsNumber(txtt_z_id.Text))
			{
				strErr+="t_z_id格式错误！\\n";	
			}
			if(!PageValidate.IsNumber(txtt_u_id.Text))
			{
				strErr+="t_u_id格式错误！\\n";	
			}
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
			int t_z_id=int.Parse(this.txtt_z_id.Text);
			int t_u_id=int.Parse(this.txtt_u_id.Text);
			string name=this.txtname.Text;
			string logo=this.txtlogo.Text;

			hua_bbs.Model.Section model=new hua_bbs.Model.Section();
			model.t_z_id=t_z_id;
			model.t_u_id=t_u_id;
			model.name=name;
			model.logo=logo;

			hua_bbs.BLL.SectionService bll=new hua_bbs.BLL.SectionService();
			bll.Add(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"保存成功！","add.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
