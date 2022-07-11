using hua_bbs.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication.topic
{
    public partial class TopicDelete1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //先删除主帖的回复帖，
            //再删除主帖（因为级连关系）
            int topicId = int.Parse(Request["topicId"]);
            ReplyService replyService = new ReplyService();
            replyService.DeleteByTopicId(topicId);

            TopicService topicService = new TopicService();
            bool b = topicService.Delete(topicId);

            Response.Write(b);

        }
    }
}