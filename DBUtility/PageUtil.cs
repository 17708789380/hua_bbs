using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// //一个分页显示的基类
/// @显示的网页链接
/// @总的帖子数量
/// @当前显示页
/// @一页显示数量
/// @参数
/// @返回分页显示的网页代码 [拼接的html分页显示语句，打包传递出来]
/// </summary>
namespace hua_bbs.DBUtility
{
    public class PageUtil
    {
        public static string GenPagination(string targetUrl, long totalNum, int currentPage, int pageSize, string param)
        {
            //求出最大页
            long totalPage = totalNum % pageSize == 0 ? totalNum / pageSize : totalNum / pageSize + 1;
            StringBuilder pageCode = new StringBuilder();
            if (totalPage == 0)
            {
                return "未查询到数据";
            }
            else
            {

                if (currentPage == 1)
                {

                    pageCode.Append("<li class=disabled><a>首页</a></li>");
                }
                else
                {
                    pageCode.Append("<li><a href='" + targetUrl + "?page=1&" + param + "'>首页</a></li>");
                }

                if (currentPage == 1)
                {
                    pageCode.Append("<li class=disabled><a>上一页</a></li>");
                }
                else
                {
                    pageCode.Append("<li><a href='" + targetUrl + "?page=" + (currentPage - 1) + "&" + param + "'>上一页</a></li>");
                }

                for (int i = currentPage - 2; i <= currentPage + 2; i++)
                {
                    if (i < 1 || i > totalPage)
                    {
                        continue;
                    }
                    if (i == currentPage)
                    {
                        pageCode.Append("<li class=active><a>" + i + "</a></li>");
                    }
                    else
                    {
                        pageCode.Append("<li><a href='" + targetUrl + "?page=" + i + "&" + param + "'>" + i + "</a></li>");
                    }
                }

                if (currentPage == totalPage)
                {
                    pageCode.Append("<li class=disabled><a>下一页</a></li>");
                }
                else
                {
                    pageCode.Append("<li><a href='" + targetUrl + "?page=" + (currentPage + 1) + "&" + param + "'>下一页</a></li>");
                }

                if (currentPage == totalPage)
                {
                    pageCode.Append("<li class=disabled><a>尾页</a></li>");
                }
                else
                {
                    pageCode.Append("<li><a href='" + targetUrl + "?page=" + totalPage + "&" + param + "'>尾页</a></li>");
                }
            }
            return pageCode.ToString();
        }


    }
}
