using hua_bbs.BLL;
using hua_bbs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication.Test
{
    public partial class Test : System.Web.UI.Page
    {
        public List<hua_bbs.Model.Zone> zoneList { set; get; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                ZoneService zoneService = new ZoneService();
                zoneList = zoneService.GetModelList("");
            }
        }
    }
}