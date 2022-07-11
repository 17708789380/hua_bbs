using hua_bbs.BLL;
using hua_bbs.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication.backstage
{
    public partial class SectionList : System.Web.UI.Page
    {
        public List<Section> sectionList { set; get; }
        public string pageCode { set; get; }
        public string sectionname { set; get; }
        public string zid { set; get; }
        public string uid { set; get; }

        public List<User> userList { set; get; }
        public List<Zone> zoneList { set; get; }

        private SectionService sectionService = new SectionService();
        //入口方法
        protected void Page_Load(object sender, EventArgs e)
        {
            string flag = Request["flag"];
            if (flag == null || "".Equals(flag))
            {
                this.show(sender, e);
            }
            else if ("saveOrUpdate".Equals(flag))
            {
                this.saveOrUpdate(sender, e);
            }
            else if ("delete".Equals(flag))
            {
                this.delete(sender, e);
            }

        }


        public void delete(object sender, EventArgs e)
        {
            string ids = Request["ids"];

            string[] strid = ids.Split(',');

            for (int i = 0; i < strid.Length; i++)
            {
                sectionService.MyDelete(Int32.Parse(strid[i]));
            }

            Response.Write(true);
            Response.End();
        }


        //添加或修改板块信息
        public void saveOrUpdate(object sender, EventArgs e)
        {
            int ssid = 0;
            if (!Int32.TryParse(Request["ssid"], out ssid))
            {
                ssid = 0;

            }
            string ssname = Request["ssname"];
            int zzid = Int32.Parse(Request["zzid"]);
            int uuid = Int32.Parse(Request["uuid"]);
            HttpPostedFile file = Request.Files["logo"];//获取上传的图片
            string logo = "";
            //关于上传图片的代码
            if (file != null && !file.FileName.Equals(""))
            {

                string fileName = file.FileName;//得到上传图片的文件名字

                string ext = Path.GetExtension(fileName);//后缀名

                if (ext == ".jpg" || ext == ".gif" || ext == ".png" || ext == ".jpeg")
                {
                    string newFileNames = Guid.NewGuid().ToString() + ext;

                    logo = "/images/logo/" + newFileNames;
                    //img文件夹前面在加一个/表示为根目录(一级目录)
                    string fileSavePath = Request.MapPath(logo);

                    file.SaveAs(fileSavePath);//保存图片到服务器指定的目录中去

                }
                else
                {
                    logo = "/face/yyy123.gif";

                }
            }
            else
            {
                logo = "/face/yyy123.gif";
            }

            Section section = new Section();
            section.id = ssid;
            section.name = ssname;
            section.t_u_id = uuid;
            section.t_z_id = zzid;
            section.logo = logo;

            sectionService.saveOrUpdate(section);

            //添加或者修改完成之后
            this.show(sender, e);
        }

        //进行查询操作数据展示
        protected void show(object sender, EventArgs e)
        {
            int pageNumber = 0;
            if (!Int32.TryParse(Request["page"], out pageNumber))
            {
                pageNumber = 1;
            }

            uid = Request["uid"];
            zid = Request["zid"];
            sectionname = Request["sectionname"];



            ArrayList list = sectionService.GetSectionZoneUserList(pageNumber, sectionname, uid, zid);
            sectionList = (List<Section>)list[0];
            pageCode = list[1].ToString();

            ZoneService zoneService = new ZoneService();
            zoneList = zoneService.GetModelList("");

            UserService userService = new UserService();
            userList = userService.GetModelList("type = 2");
        }
    }
}