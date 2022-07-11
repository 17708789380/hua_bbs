<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="WebApplication.userCenter.UserInfo" %>

<!DOCTYPE html>

<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>Insert title here</title>
<link rel="stylesheet" href="/admin/css/bootstrap.min.css" />
<link rel="stylesheet" href="/admin/css/bootstrap-responsive.min.css" />
<script type="text/javascript" src="/js/jquery-1.11.1.js"></script>
<style type="text/css">
</style>
<script type="text/javascript">
</script>
</head>
<body>
<div id="header" class="wrap" style="width: 1200px; margin: 0 auto;">
	 <% Server.Execute("/common/Top.aspx"); %>
</div>
<div style="width: 1200px; margin: 0 auto;">
	<h2>个人信息管理</h2>
	<table class="table table-bordered table-striped with-check">
		<tr>
			<th style="text-align: center;vertical-align: middle;">ID</th>
			<th style="text-align: center;vertical-align: middle;">昵称</th>
			<th style="text-align: center;vertical-align: middle;">真实姓名</th>
			<th style="text-align: center;vertical-align: middle;">密码</th>
			<th style="text-align: center;vertical-align: middle;">性别</th>
			<th style="text-align: center;vertical-align: middle;">头像</th>
			<th style="text-align: center;vertical-align: middle;">邮箱</th>
			<th style="text-align: center;vertical-align: middle;">联系电话</th>
			<th style="text-align: center;vertical-align: middle;">注册时间</th>
			<th style="text-align: center;vertical-align: middle;">用户类型</th>
			<th style="text-align: center;vertical-align: middle;">操作</th>
		</tr>
		<tr>
			<td style="text-align: center;vertical-align: middle;"><%=user.id %></td>
			<td style="text-align: center;vertical-align: middle;"><%=user.nickname %></td>
			<td style="text-align: center;vertical-align: middle;"><%=user.truename %></td>
			<td style="text-align: center;vertical-align: middle;"><%=user.password %></td>
			<td style="text-align: center;vertical-align: middle;"><%=user.sex %></td>
			<td style="text-align: center;vertical-align: middle;">

                 <%if (user.sex == "男")
                    { %>
                <img alt="" src="<%=user.face %>" style="width: 100px; height: 100px;">
                <%}
                else if (user.sex == "女")
                { %>
                <img alt="" src="<%=user.face %>" style="width: 100px; height: 100px;">
                <%}
                else
                { %>
                <img alt="" src="<%=user.face %>" style="width: 100px; height: 100px;">
                <%} %>

				</td>
			<td style="text-align: center;vertical-align: middle;"><%=user.email %></td>
			<td style="text-align: center;vertical-align: middle;"><%=user.mobile %></td>
			<td style="text-align: center;vertical-align: middle;"><%=user.regtime %></td>
			<td style="text-align: center;vertical-align: middle;">

                <%if (user.type == "1")
                    { %>
                <font style="color: black;">普通用户</font>
                <%}
                else if (user.type == "2")
                { %>
                <font style="color: blue;">版主</font>
                <%}
                else
                { %>
                <font style="color: red;">管理员</font>
                <%} %>
				</td>
				<td style="text-align:center; vertical-align: middle;">
					<a class="btn btn-info" type="button" href="/userCenter/UserModify.aspx">修改</a>&nbsp;&nbsp;
				</td>
		</tr>
	</table>
</div>
<div id="footer" style="width: 1200px; margin: 0 auto;">
	 <% Server.Execute("/common/Footer.aspx"); %>
</div>
</body>
</html>