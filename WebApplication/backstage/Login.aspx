<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication.backstage.Login" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>后台管理登陆</title>
<link rel="stylesheet" href="/admin/css/bootstrap.min.css" />
<link rel="stylesheet" href="/admin/css/bootstrap-responsive.min.css" />
<link rel="stylesheet" href="/admin/css/unicorn.login.css" />

<script src="/admin/js/jquery.min.js"></script>  
<script src="/admin/js/unicorn.login.js"></script> 
	<style type="text/css">
		h1 b{font-family:'Arial Nova';font-size:40px;color:white;}
		#logo {		
			overflow: hidden !important;
			text-align: center;
			position: relative;
		} 
	</style>
</head>
	
<body >
	<div style="background-color:darkgray; width:100%;height:100%">
	<div id="logo">
		<h1><b>盼盼后台管理</b></h1>
		<%--<img src="panpan_backend.png" alt="" />--%>
        <!--<img src="/admin/images/logo.png" alt="" />-->
    </div>
    <div id="loginbox" >
		<form id="loginform" class="form-vertical" runat="server">
			<p>输入用户昵称和密码进入后台.</p>
			<div class="control-group">
				<div class="controls">
					<div class="input-prepend">
						<span class="add-on"><i class="icon-user"></i></span>
						<input type="text" name="nickName" value="<%=nickname %>" placeholder="昵称" />
					</div>
				</div>
			</div>
			<div class="control-group">
				<div class="controls">
					<div class="input-prepend">
						<span class="add-on"><i class="icon-lock"></i></span><input
							type="password" name="password" value="<%=password %>" placeholder="密码" />
					</div>
				</div>
			</div>
			<div class="form-actions">
				 <span class="pull-right">
				 	<font id="error" style="font-size: 20px;" color="red"><%=msg %></font>
				 	<input type="submit" class="btn btn-inverse" value="进入后台" />
				 </span>
			</div>
		</form>
	</div>
		</div>
</body>
</html>
