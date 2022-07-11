using hua_bbs.BLL;
using hua_bbs.DAL;
using hua_bbs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.topic
{
    /// <summary>
    /// TopicModify 的摘要说明
    /// </summary>
    public class TopicModify : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //topicId:topicId,topicTop:topicTop,topicGood:topicGood
            int topicId =  int.Parse(context.Request["topicId"]);
            string topicTop = context.Request["topicTop"];
            string topicGood = context.Request["topicGood"];

            TopicService topicService = new TopicService();
            Topic topic = topicService.GetModel(topicId);
            //Topic topic = topicService.GetModel(topicId);
            topic.top = topicTop;
            topic.good = topicGood;

            TopicDao topciDao = new TopicDao();
            bool success = topciDao.Update(topic);

            context.Response.Write(success);
            context.Response.End();

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}