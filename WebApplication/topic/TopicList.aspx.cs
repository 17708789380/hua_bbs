using hua_bbs.BLL;
using hua_bbs.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


//***************************************
//某一个具体的模块帖子模块
//***************************************
namespace WebApplication.topic
{
    public partial class TopicList : System.Web.UI.Page
    {
        public Dictionary<int, Reply> topicLastReply { set; get; }
        public Dictionary<int, int> topicReplyCount { set; get; }
        public List<Topic> ptTopicList { set; get; }
        public string pageCode { set; get; }
        public Section section { set; get; }

        public Dictionary<int, Reply> zdtopicLastReply { set; get; }
        public Dictionary<int, int> zdtopicReplyCount { set; get; }
        public List<Topic> zdTopicList { set; get; }


       

        protected void Page_Load(object sender, EventArgs e)
        {
            



            if (!this.IsPostBack)
            {
                int page = 0;
                if (!Int32.TryParse(Request["page"], out page))
                {
                    page = 1;
                }

                // section = new Section();
                                                   
                int sectionId = int.Parse(Request["sectionId"]);//获取板块主键id

                TopicService topicService = new TopicService();

                //得到普通的主贴信息                                         
                ArrayList mylist = topicService.FindTopic(sectionId, page);
                topicReplyCount = (Dictionary<int, int>)mylist[0];
                topicLastReply = (Dictionary<int, Reply>)mylist[1];
                ptTopicList = (List<Topic>)mylist[2];
                pageCode = mylist[3].ToString();
                section = (Section)mylist[4];

                Session["section_name_Session"] = section;


                //得到置顶的主贴信息
                ArrayList myzdlist = topicService.FindStickTopic(sectionId);
                zdtopicReplyCount = (Dictionary<int, int>)myzdlist[0];
                zdtopicLastReply = (Dictionary<int, Reply>)myzdlist[1];
                zdTopicList = (List<Topic>)myzdlist[2];

                #region  测试代码
                //测试代码
                //pageCode = mylist[3].ToString();
                //section = (Section)mylist[4];
                /* foreach(Topic topic in ptTopicList){
                    Response.Write("贴子标题:" + topic.title+"--");
                    Response.Write("贴子作者:" + topic.topicuser.nickname + "--");
                    if (topicReplyCount.ContainsKey(topic.id)) {
                        Response.Write("此贴回复数:" + topicReplyCount[topic.id] + "--");
                    }
                    if (topicLastReply.ContainsKey(topic.id))
                    {
                        Response.Write("最后回复时间:" + topicLastReply[topic.id].publishtime + "--");
                        Response.Write("最后回复人:" + topicLastReply[topic.id].replyuser.nickname + "--");
                    }
                    Response.Write("<br>");
                */
                #endregion
            }

        }
    }
}