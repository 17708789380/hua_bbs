using hua_bbs.BLL;
using hua_bbs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.topic
{
    /// <summary>
    /// ModifyTopicContent 的摘要说明
    /// </summary>
    public class ModifyTopicContent : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            int topicId = Int32.Parse(context.Request["topicId"]);
            string title = context.Request["title"];
            string content = context.Request["content"];

            //先load在进行修改
            TopicService topicService = new TopicService();
            Topic topic = topicService.GetModel(topicId);
            //查询出来后对 topic 对象中的数据进行修改
            topic.title = title;
            topic.content = content;
            topic.modifytime = DateTime.Now;//设置修改时间

            bool b = topicService.Update(topic);
            context.Response.Write(b);
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