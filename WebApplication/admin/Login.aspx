<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication.admin.Login" %>

<!DOCTYPE html>

<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>登录</title>
<style type="text/css">
html{overflow-y:scroll;vertical-align:baseline;}
body{font-family:Microsoft YaHei,Segoe UI,Tahoma,Arial,Verdana,sans-serif;font-size:12px;color:#fff;height:100%;line-height:1;background:#E0E0E0}
*{padding:0;
        margin-left: 0;
        margin-right: 0;
        margin-bottom: 0;
    }
ul,li{list-style:none}
h1{font-size:30px;font-weight:700;text-shadow:0 1px 4px rgba(0,0,0,.2);color:black}
.login-box{width:410px;margin:120px auto 0 auto;text-align:center;padding:30px;background-color:white;border-radius:10px;}
.login-box .name,.login-box .password,.login-box .code,.login-box .remember{font-size:16px;text-shadow:0 1px 2px rgba(0,0,0,.1)}
.login-box .remember input{box-shadow:none;width:15px;height:15px;margin-top:25px}
.login-box .remember{padding-left:40px}
.login-box .remember label{display:inline-block;height:42px;width:70px;line-height:34px;text-align:left}
.login-box label{display:inline-block;width:100px;text-align:right;vertical-align:middle}
.login-box #imageCode{width:120px}
.login-box .codeImg{float:right;margin-top:26px;}
.login-box img{width:140px;height:42px;border:none}
table{color:black}
input[type=text],input[type=password]{width:270px;height:42px;margin:12.5px auto;padding:0px 15px;border:1px solid rgba(0,0,0,0);border-radius:6px;color:black;letter-spacing:2px;font-size:16px;background:transparent;}
#btn_submit{cursor:pointer;width:100%;height:44px;padding:0;background:#ef4300;border:1px solid #ff730e;border-radius:6px;font-weight:700;color:#fff;font-size:24px;letter-spacing:15px;margin-top:10px; text-shadow:0 1px 2px rgba(0,0,0,.1)}
input:focus{outline:none;box-shadow:0 2px 3px 0 rgba(0,0,0,.1) inset,0 2px 7px 0 rgba(0,0,0,.2)}
#btn_submit:hover{box-shadow:0 15px 30px 0 rgba(255,255,255,.15) inset,0 2px 7px 0 rgba(0,0,0,.2)}
/*.screenbg{position:fixed;bottom:0;left:0;z-index:-999;overflow:hidden;width:100%;height:100%;min-height:100%;}
.screenbg ul li{display:block;list-style:none;position:fixed;overflow:hidden;top:0;left:0;width:100%;height:100%;z-index:-1000;float:right;}
.screenbg ul a{left:0;top:0;width:100%;height:100%;display:inline-block;margin:0;padding:0;cursor:default;}
.screenbg a img{vertical-align:middle;display:inline;border:none;display:block;list-style:none;position:fixed;overflow:hidden;top:0;left:0;width:100%;height:100%;z-index:-1000;float:right;}*/
/*.bottom{margin:8px auto 0 auto;width:100%;position:fixed;text-align:center;bottom:0;left:0;overflow:hidden;padding-bottom:8px;color:#ccc;word-spacing:3px;zoom:1;}
.bottom a{color:#FFF;text-decoration:none;}
.bottom a:hover{text-decoration:underline;}*/
#foot{width:100%;height:50px;background-color:lightgray;margin-top:125px;text-align:center;line-height:50px}
#foot p a{text-decoration:none}

</style>

<script type="text/javascript" src="/js/jquery-1.11.1.js"></script>
<script type="text/javascript">
$(function(){
	$(".screenbg ul li").each(function(){
		$(this).css("opacity","0");
	});
	$(".screenbg ul li:first").css("opacity","1");
	var index = 0;
	var t;
	var li = $(".screenbg ul li");	
	var number = li.size();
	function change(index){
		li.css("visibility","visible");
		li.eq(index).siblings().animate({opacity:0},3000);
		li.eq(index).animate({opacity:1},3000);
	}
	function show(){
		index = index + 1;
		if(index<=number-1){
			change(index);
		}else{
			index = 0;
			change(index);
		}
	}
	t = setInterval(show,8000);
	//根据窗口宽度生成图片宽度
	var width = $(window).width();
	$(".screenbg ul img").css("width",width+"px");
});
function loadimage(){
    document.getElementById("randImage").src = "/admin/ValidateCode.ashx?d=" + Math.random();
}

function checkForm(){
	 var nickName=$("#nickName").val();
	 var password=$("#password").val();
	 var imageCode=$("#imageCode").val();
	 if(nickName==""){
		 $("#error").html("昵称不能为空！");
		 return false;
	 }
	 if(password==""){
		 $("#error").html("密码不能为空！");
		 return false;
	 }
	 if(imageCode==""){
		 $("#error").html("验证码不能为空！");
		 return false;
	 }
	 return true;
}
</script>
</head>
<body>
<div class="login-box">
	<h1>盼盼交流社区登录</h1>
	<form runat="server" onsubmit="return checkForm()">
		<table>
			<tr class="name">
				<td><label>用户名：</label></td>
				<td><input type="text" id="nickName" name="user_nickName" tabindex="1" autocomplete="off" value="<%=nickName1 %>"/></td>
			</tr>
			<tr class="password">
				<td><label>密  码：</label></td>
				<td><input type="password" name="user_password" maxlength="16" id="passWord" tabindex="2" value="<%=passWord1 %>"/></td>
			</tr>
			<tr class="code">   
				<td><label>验证码：</label></td>   
				<td>
					<input  class="text" style="margin-right: 10px;"
							type=text value="" name="imageCode" id="imageCode"><img
							onclick="javascript:loadimage();" title="换一张试试" name="randImage"
							id="randImage" src="/admin/ValidateCode.ashx" border="1"
							align="absmiddle">
				</td>
			</tr>
            <tr ><td >  <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" />  记住我&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>
			<tr>

				<td colspan="2">
                    
					 <asp:Button ID="btn_submit" runat="server" Text="登录" OnClick="btn_submit_Click" />	 &nbsp;&nbsp;&nbsp;&nbsp;
						<font id="error" style="font-size: 20px;" color="red"><%=error %></font>
				</td>
			</tr>
		</table>
	</form>
	
</div>

<%--<div class="bottom">©2022 愿以百年挽朝夕 <a href="https://blog.csdn.net/qq_33154343/article/list?t=1" target="_blank">关于</a> <span>asp.nte</span><img width="13" height="16" src="images/copy_rignt_24.png" /></div>--%>

<div class="screenbg">
	<%--<ul>
		<li><a href="javascript:;"><img src="/images/0.png"></a></li>
		<li><a href="javascript:;"><img src="/images/1.png"></a></li>
		<li><a href="javascript:;"><img src="/images/2.png"></a></li>
	</ul>--%>
</div>
	
	<div id="foot">
		<p >
			<a href="#" />©2022 最终解释权归本组所有&emsp;&emsp;&emsp;&emsp;联系电话：400-1235-xxxxx<a />
		</p>
	</div>
</body>
</html>