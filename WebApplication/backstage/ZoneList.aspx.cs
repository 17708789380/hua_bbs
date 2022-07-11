using hua_bbs.BLL;
using hua_bbs.DBUtility;
using hua_bbs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication.backstage
{
    public partial class ZoneList1 : System.Web.UI.Page
    {
        public List<Zone> zoneList { set; get; }
        public string pageCode { set; get; }
        public string str { set; get; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                int pageNumber = 1;
                if (!Int32.TryParse(Request["page"], out pageNumber))
                {
                    pageNumber = 1;
                }
                ZoneService zoneService = new ZoneService();

                zoneList = zoneService.findAllZone(pageNumber);

                pageCode = PageUtil.GenPagination("/backstage/ZoneList.aspx", zoneService.GetRecordCount(""), pageNumber, zoneService.pageCount, null);

                str = "测试";
            }

        }
    }
}