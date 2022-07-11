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
namespace hua_bbs.Web.Topic
{
    public partial class Add : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                       
        }

        		protected void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(!PageValidate.IsNumber(txtt_u_id.Text))
			{
				strErr+="t_u_id格式错误！\\n";	
			}
			if(!PageValidate.IsNumber(txtt_s_id.Text))
			{
				strErr+="t_s_id格式错误！\\n";	
			}
			if(this.txtcontent.Text.Trim().Length==0)
			{
				strErr+="content不能为空！\\n";	
			}
			if(!PageValidate.IsDateTime(txtmodifytime.Text))
			{
				strErr+="modifytime格式错误！\\n";	
			}
			if(!PageValidate.IsDateTime(txtpublishtime.Text))
			{
				strErr+="publishtime格式错误！\\n";	
			}
			if(this.txttitle.Text.Trim().Length==0)
			{
				strErr+="title不能为空！\\n";	
			}
			if(this.txtgood.Text.Trim().Length==0)
			{
				strErr+="good不能为空！\\n";	
			}
			if(this.txttop.Text.Trim().Length==0)
			{
				strErr+="top不能为空！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			int t_u_id=int.Parse(this.txtt_u_id.Text);
			int t_s_id=int.Parse(this.txtt_s_id.Text);
			string content=this.txtcontent.Text;
			DateTime modifytime=DateTime.Parse(this.txtmodifytime.Text);
			DateTime publishtime=DateTime.Parse(this.txtpublishtime.Text);
			string title=this.txttitle.Text;
			string good=this.txtgood.Text;
			string top=this.txttop.Text;

			hua_bbs.Model.Topic model=new hua_bbs.Model.Topic();
			model.t_u_id=t_u_id;
			model.t_s_id=t_s_id;
			model.content=content;
			model.modifytime=modifytime;
			model.publishtime=publishtime;
			model.title=title;
			model.good=good;
			model.top=top;

			hua_bbs.BLL.TopicService bll=new hua_bbs.BLL.TopicService();
			bll.Add(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"保存成功！","add.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
