using hua_bbs.BLL;
using hua_bbs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.topic
{
    /// <summary>
    /// TopicReplySave1 的摘要说明
    /// </summary>
    public class TopicReplySave1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            //获得准备回帖内容， 当前登录用户ID， 回复的帖子id
            string content = context.Request["reply.content"];
            int nowUserId = Int32.Parse(context.Request["reply.user.id"]);
            int topicId = Int32.Parse(context.Request["reply.topic.id"]);

            ReplyService replyService = new ReplyService();

            Reply reply = new Reply();
            reply.t_u_id = nowUserId;
            reply.t_t_id = topicId;
            reply.content = content;
            reply.publishtime = DateTime.Now;


            bool b;
            if (replyService.Add(reply) > 0)
            {
                b = true;
            }
            else
            {
                b = false;
            }


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