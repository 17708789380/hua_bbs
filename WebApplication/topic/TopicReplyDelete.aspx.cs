using hua_bbs.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication.topic
{
    public partial class TopicReplyDelete : System.Web.UI.Page
    {
        public int replyId { set; get; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                replyId = int.Parse(Request["replyId"]);

                ReplyService replyService = new ReplyService();
                bool result = replyService.Delete(replyId);
            }
        }
    }
}