using hua_bbs.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.backstage
{
    /// <summary>
    /// ZoneDelete 的摘要说明
    /// </summary>
    public class ZoneDelete : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            int zoneId = Int32.Parse(context.Request["zoneId"]);
            ZoneService zoneService = new ZoneService();

            bool b = zoneService.mydelete(zoneId);//调用mydelete方法会将回贴,主贴,板块都删除掉

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