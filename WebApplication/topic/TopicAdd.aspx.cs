using hua_bbs.BLL;
using hua_bbs.DAL;
using hua_bbs.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication.topic
{
    public partial class TopicAdd : System.Web.UI.Page
    {
        public SectionService sectionService { set; get; }
        public TopicService topicService { set; get; }
        public Section section { set; get; }
        public List<Section> sectionList { set; get; }
        public Topic topic { set; get; }
        protected void Page_Load(object sender, EventArgs e)
        {

            topic = new Topic();
            section = new Section();
            sectionList = new List<Section>();

            if (!this.IsPostBack)
            {
                //if (Session["Username_session"] == null)
                //{
                //    Response.Redirect("/admin/Login.aspx");
                //}
                
                    section.id = int.Parse(Request.QueryString["sectionId"]);
                    SectionDao sectionDao = new SectionDao();

                    section = sectionDao.GetModel(section.id);

                    //1.显示模块提供选择，显示
                    sectionService = new SectionService();
                    DataSet dataSet = sectionService.GetList("");
                    sectionList = sectionService.DataTableToList(dataSet.Tables[0]);
              
            }
            else
            {
                //2.获得 发帖子的信息
                //3.保存 到数据库
                //4. (就相当于已经可以显示了该帖子存在)
                topic.title = Request.Form["topic.title"];
                topic.content = Request.Form["topic.content"];  
                topic.modifytime = DateTime.Now;
                topic.publishtime = DateTime.Now;
                //topic.t_s_id = 7;   //***********************************这个位置没有修改*****8
                topic.t_s_id = int.Parse(Request["topic.section.id"]);
                topic.good = "0";
                topic.top = "0";

                UserService userService = new UserService();
                UserDao userDao = new UserDao();                                 
                string temp_user = Session["Username_session"].ToString();
                DataSet dataSet2 = userService.GetList("nickname ='" + temp_user + "'");
                User user = userDao.DataRowToModel(dataSet2.Tables[0].Rows[0]);
                topic.t_u_id = user.id;

                topicService = new TopicService();
                topicService.Add(topic);

                Response.Redirect("/topic/TopicList.aspx?sectionId=" + topic.t_s_id + "");



            }

        }
    }
}