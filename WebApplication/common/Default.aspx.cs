using hua_bbs.BLL;
using hua_bbs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication.common
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Zone> zoneList { set; get; }
        public List<Section> sectionList { set; get; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ZoneService zoneService = new ZoneService();
            zoneList = zoneService.GetZoneSectionList();
            SectionService sectionService = new SectionService();
            sectionList = sectionService.GetModelList("");

            //foreach(Zone zone in zoneList)
            //{
            //    Response.Write("<h1>"+zone.name+ "</h1><br>");

            //    foreach (Section section in sectionList)
            //    {
            //        if(zone.id == section.t_z_id)
            //        {
            //            Response.Write(section.name + "<br>");
            //        }               
            //    }
            //}


        }
    }
}