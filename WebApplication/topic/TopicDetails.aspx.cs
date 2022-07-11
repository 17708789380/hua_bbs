using hua_bbs.BLL;
using hua_bbs.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication.topic
{
   
    
    public partial class TopicDetails : System.Web.UI.Page
    {
        public List<Reply> replyList { set; get; }
        public Topic topic { set; get; }
        public string pageCode { set; get; }
        public int pageNumber { set; get; }
        public int pageCount { set; get; }  //一页显示的条数

        public User nowUser { set; get; }
        public Section section { set; get; }
        protected void Page_Load(object sender, EventArgs e)
        {
            pageCount = 4;
            nowUser = new User();
            if (Session["user"] == null)
            {
                nowUser.id = 0;
            }
            else
            {
                nowUser = (User)Session["user"];
            }
            
            
            int topicId = int.Parse(Request["topicId"]);
            pageNumber = int.Parse(Request["Page"]);
            //int pageNumber = 1;
            ReplyService replyService = new ReplyService();
            ArrayList myList = replyService.FindReplyInfoByTopicId(topicId, pageNumber);
            topic = (Topic)myList[0];
            replyList = (List<Reply>)myList[1];
            pageCode = (string)myList[2];
            section = (Section)myList[3];


        }
    }
}