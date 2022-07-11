using hua_bbs.BLL;
using hua_bbs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.backstage
{
    /// <summary>
    /// ZoneSave 的摘要说明
    /// </summary>
    public class ZoneSave : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";


            int id = 0;
            string description = context.Request["description"];
            string name = context.Request["name"];
            ZoneService zoneService = new ZoneService();
            Zone zone = new Zone();
            zone.name = name;
            zone.description = description;
            bool b;
            //成功得到id的话进行修改操作否则进行添加操作
            if (Int32.TryParse(context.Request["id"], out id))
            {
                zone.id = id;
                b = zoneService.Update(zone);
            }
            else
            {
                if (zoneService.Add(zone) > 0)
                {
                    b = true;
                }
                else
                {
                    b = false;
                }
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