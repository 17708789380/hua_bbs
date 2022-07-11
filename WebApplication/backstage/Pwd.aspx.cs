
using hua_bbs.BLL;
using hua_bbs.DBUtility;
using hua_bbs.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication.backstage
{
    public partial class Pwd : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }
     

        protected void Button1_Click1(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string usename = Session["usename"].ToString();
            string yhpwd = this.yhm.Text;
            string mima = this.mima.Text;
            //int r = nameuser(usename, yhpwd);
            UserService userService = new UserService();
            List<User> userList = userService.GetModelList("type='3' and nickname='" + usename + "' and password='" + yhpwd + "'");
            List<User> users = new List<User>();
            if (userList.Count >= 0)
            {
                //bool s = publctnameuser(usename, mima);
                foreach (var item in userList)
                {
                    item.password = mima;
                    users.Add(item);
                }
                try
                {
                    bool s = userService.Update(users[0]);
                    if (s)
                    {
                        Response.Write("<script>alert('密码修改成功！')</script>");
                    }
                }
                catch (Exception)
                {
                    Response.Write("<script>alert('原密码不正确！')</script>");
                }
                
            }
            else
            {
                Response.Write("<script>alert('原密码不正确！')</script>");
            }
        }

        //public int nameuser(string usename, string ymma)
        //{
        //    string sql = string.Format("select * from Staff where nickname='{0}' and password='{1}'", usename, ymma);
        //    //定义SQL语句
        //    DataTable dataTable = DBHelepjs.GetDataTable(sql);
        //    return dataTable.Rows.Count;
        //}
        //public bool publctnameuser(string usename, string xingmm)
        //{
        //    string sql = string.Format("update t_user set password='{0}' where nickname='{1}'", xingmm, usename);
        //    return DBHelepjs.ExcuteSql(sql);
        //}
    }

}