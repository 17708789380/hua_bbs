using hua_bbs.DBUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//**************
//生成数字的验证码*
//**************

namespace WebApplication.admin
{
    /// <summary>
    /// ValidateCode 的摘要说明
    /// </summary>
    /// 

             //System.Web.SessionState.IRequiresSessionState  使用这个，为什么要加？ 忘记了？
    public class ValidateCode : IHttpHandler,System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            MyValidate validateCode = new MyValidate();

            string code = validateCode.CreateValidateCode(4);  //生成的验证码位数
            context.Session["code"] = code;   //保存到session中
            validateCode.CreateValidateGraphic(code, context);
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