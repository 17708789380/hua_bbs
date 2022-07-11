using hua_bbs.DAL;
using hua_bbs.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication.admin
{
    public partial class Register : System.Web.UI.Page
    {
        public User user { set; get; }
        public string msg { set; get; }  

        protected void Page_Load(object sender, EventArgs e)
        {
            user = new User();

            try {
                if (this.IsPostBack)
                {
                    //" nickName  user.trueName  user.sex face  user.password  user.mobile  user.email  user.type"
                    
                    user.nickname = Request.Form["user.trueName"];
                    user.truename = Request.Form["user.trueName"];
                    user.sex = Request.Form["user.sex"];
                    HttpPostedFile file = Request.Files["face"];
                    user.password = Request.Form["user.password"];
                    user.mobile = Request.Form["user.mobile"];
                    user.email = Request.Form["user.email"];
                    user.type = Request.Form["user.type"];
                    user.regtime = DateTime.Now;  //系统当前时间

                    //上传头像图片
                    if (file != null && !file.FileName.Equals(""))
                    {
                        string fileName = file.FileName; //得到上传头像图片 的文件名字
                        string ext = Path.GetExtension(fileName);

                        if (ext == ".jpg" || ext == ".gif" || ext == ".png" || ext == ".jpeg")
                        {
                            string newFileNames = Guid.NewGuid().ToString() + ext; //唯一的生成一个随机文件名                   
                            string startPath = "/userFace/";
                            Directory.CreateDirectory(Path.GetDirectoryName(Request.MapPath(startPath)));  //创建文件夹 "/userFace/"文件夹
                            string fileSavePath = startPath + newFileNames;//文件保存路径，将存入用户表【相对路径】
                            user.face = fileSavePath;
                            file.SaveAs(Request.MapPath(fileSavePath));//保存图片【绝对路径】到服务器指定的目录中去
                        }
                    }
                    else //没有上传头像，使用默认头像·
                    {
                        user.face = "/userFace/huahua.png";
                    }

                    UserDao userDao = new UserDao();
                    int temp_i = userDao.Add(user); //返回的是注册一个自生长的一个id
                    if (temp_i >= 0)
                    {
                        Response.Redirect("/Index.aspx");
                    }
                    else
                    {
                        Response.Write("注册失败");
                    }
                }
            }
            catch(Exception ee)
            {
                msg = "您的数据问题!刚才出现了一个不可知的异常"+ee;
            }

            
            
        }
    }
}