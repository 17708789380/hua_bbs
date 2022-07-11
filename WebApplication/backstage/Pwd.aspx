<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pwd.aspx.cs" Inherits="WebApplication.backstage.Pwd" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <title>大模块管理</title>
    <link rel="stylesheet" href="/admin/css/bootstrap.min.css" />
    <link rel="stylesheet" href="/admin/css/bootstrap-responsive.min.css" />
    <link rel="stylesheet" href="/admin/css/uniform.css" />
    <link rel="stylesheet" href="/admin/css/unicorn.main.css" />
    <link rel="stylesheet" href="/admin/css/unicorn.grey.css" class="skin-color" />
    <script type="text/javascript" src="/js/jquery-1.11.1.js"></script>
    <script type="text/javascript" src="/js/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#tuichu").click(function () {
                //清空浏览器储存
                var r = confirm("您确定退出吗？");
                if (r == true) {
                    location.href = "/Index.aspx";
                    alert("退出成功，cookie已清除！");
                    document.cookie;
                };
            });
            $("#qccookie").click(function () {

                var r = confirm("您确定要刷新系统缓存吗？");
                if (r == true) {
                    this.href = "/backstage/SectionList.aspx";
                    alert("刷新系统缓存成功！");
                };
            });
        })
    </script>
    <style type="text/css">
        .container {
            height: auto;
            width: 300px;
            margin: 20px auto;
            background-color: white;
            box-shadow: 0px 0px 10px 1px;
            padding: 25px;
        }

        .text {
            margin-top: 20px;
            color: #72b7e6;
            font-size: 26px;
        }
    </style>
</head>
<%--
if(session.getAttribute("currentUser")==null){
	response.sendRedirect("login.jsp");
	return;
}
--%>
<body>
    <div id="header">
        <h1 style="margin-left: 0px; padding-left: 0px;"><a href="#">盼盼论坛</a></h1>
        <!-- <h2 style="padding: 0px; margin-top: 10px; margin-bottom: 0px;">
			<a href="#"><font color="#cccccc">Java1234论坛</font></a>
		</h2>
		<h3 style="margin: 0px 0px 0px 40px;">
			<a href="#"><font color="#cccccc">后台管理</font></a>
		</h3> -->
    </div>

    <div id="sidebar">
        <ul>
            <li id="zoneLi"><a href="/backstage/ZoneList.aspx"><i class="icon icon-home"></i><span>大板块管理</span></a></li>
            <li id="sectionLi"><a href="/backstage/SectionList.aspx"><i class="icon icon-home"></i><span>小板块管理</span></a></li>
            <%--<li id="topicLi"><a href="/backstage/TopicListAdmin.aspx"><i class="icon icon-home"></i> <span>帖子管理</span></a></li>--%>
            <!-- <li><a href="#"><i class="icon icon-home"></i> <span>回复管理</span></a></li> -->
            <%--<li id="userLi"><a href="/backstage/UserList.aspx"><i class="icon icon-home"></i> <span>用户管理</span></a></li>--%>
            <li class="submenu"><a href="#"><i class="icon icon-th-list"></i>
                <span>系统管理</span> <span class="label">3</span></a>
                <ul>
                    <li><a href="/backstage/Pwd.aspx">修改密码</a></li>
					<li ><a id="tuichu" href="#">安全退出</a></li>
					<li><a id="qccookie" href="#">刷新系统缓存</a></li>
                </ul>

            </li>
        </ul>

    </div>

    <%--<div id="style-switcher">
        <i class="icon-arrow-left icon-white"></i><span>颜色:</span>
        <a href="#grey" style="background-color: #555555; border-color: #aaaaaa;"></a>
        <a href="#blue" style="background-color: #2D2F57;"></a>
        <a href="#red" style="background-color: #673232;"></a>
    </div>--%>

    <div id="content">
        <div id="content-header">
            <h1>后台管理</h1>
            <!-- <div class="btn-group">
				<a class="btn btn-large tip-bottom" title="Manage Files"><i
					class="icon-file"></i></a> <a class="btn btn-large tip-bottom"
					title="Manage Users"><i class="icon-user"></i></a> <a
					class="btn btn-large tip-bottom" title="Manage Comments"><i
					class="icon-comment"></i><span class="label label-important">5</span></a>
				<a class="btn btn-large tip-bottom" title="Manage Orders"><i
					class="icon-shopping-cart"></i></a>
			</div> -->
        </div>
        <form id="form1" runat="server">
        <div class="container">
            <asp:Image ID="Image1" runat="server" ImageUrl="~\images\bj2.jpg" Width="120px"/>
            <div class="text" style="padding-bottom: 30px">
                修改密码
            </div>
            <div class="form-horizontal">
                <div class=" input-group">
                    <span class="input-group-addon glyphicon glyphicon-user"></span>
                    <asp:TextBox ID="yhm" runat="server" class="form-control" type="text" placeholder="原密码" Style="margin-top: 2px"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ControlToValidate="yhm" ForeColor="Red" ID="RequiredFieldValidator1" runat="server" ErrorMessage="原密码不能为空"></asp:RequiredFieldValidator>
                <div class="input-group" style="">
                    <span class="input-group-addon glyphicon glyphicon-lock"></span>
                    <asp:TextBox ID="mima" runat="server" class="form-control" type="text" placeholder="新密码" Style="margin-top: 2px"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ControlToValidate="mima" ForeColor="Red" ID="RequiredFieldValidator2" runat="server" ErrorMessage="密码不能为空"></asp:RequiredFieldValidator>
                <div class="input-group" style="">
                    <span class="input-group-addon glyphicon glyphicon-lock"></span>
                    <asp:TextBox ID="newmima" runat="server" class="form-control" type="text" placeholder="确认新密码" Style="margin-top: 2px"></asp:TextBox>
                </div>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="两次密码不一致" ControlToCompare="newmima" ControlToValidate="mima" ForeColor="Red"></asp:CompareValidator>
                <div class="input-group">
                    <asp:Button ID="Button1" runat="server" Text="提交" class="btn btn-success" Style="margin-left: 160px; margin-top: 10px" OnClick="Button1_Click" />
                </div>

            </div>

        </div>
    </form>

        <!-- 主题 crud 操作-->

        <div class="row-fluid">
            <div id="footer" class="span12">
                2022 &copy; 计应二 作者：计应二&nbsp;&nbsp;&nbsp;&nbsp; <a href="http://www.hbpu.edu.cn/">盼盼论坛</a>
            </div>
        </div>
    </div>

    <script src="/admin/js/jquery.min.js"></script>
    <script src="/admin/js/jquery.ui.custom.js"></script>
    <script src="/admin/js/bootstrap.min.js"></script>
    <script src="/admin/js/jquery.uniform.js"></script>
    <!-- <script src="js/select2.min.js"></script> -->
    <script src="/admin/js/jquery.dataTables.min.js"></script>
    <script src="/admin/js/unicorn.js"></script>
    <script src="/admin/js/unicorn.tables.js"></script>
    <script type="text/javascript" src="/js/uploadPreview.min.js"></script>
</body>
</html>
