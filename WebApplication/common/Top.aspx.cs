using hua_bbs.DAL;
using hua_bbs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication.common
{
    
    public partial class Top : System.Web.UI.Page
    {
        //public string temp2 { set; get; }
        //public Section section

        protected void Page_Load(object sender, EventArgs e)
        {
            //总是先执行Top代码，再执行Defult中间部分代码（top需要的代码在Defult这里获得）， 
            //SectionDao sectionDao = new SectionDao();
            //int t_id = ((User)Session["user"]).id;
            //temp2 = sectionDao.GetModel(t_id).name;

            //temp2 = ((Section)Session["section_name_Session"]).name;
        }
    }
}