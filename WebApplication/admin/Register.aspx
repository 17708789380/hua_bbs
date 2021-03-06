<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebApplication.admin.Register" %>

<!DOCTYPE html>

<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>Insert title here</title>
<link href="/bootstrap/css/bootstrap.css" rel="stylesheet" />
<link href="/bootstrap/css/bootstrap-responsive.css" rel="stylesheet" />
<link href="/bootstrap/css/docs.css" rel="stylesheet" />
<script type="text/javascript" src="/js/jquery-1.11.1.js"></script>
<script type="text/javascript" src="/js/jquery.validate.js"></script>
<script type="text/javascript" src="/js/jquery.validate.messages_cn.js"></script>
<script type="text/javascript" src="/js/uploadPreview.min.js"></script>
<style type="text/css">
    body{background-color:lightgray}
button{cursor:pointer;width:100%;height:44px;padding:0;background:#ef4300;border:1px solid #ff730e;border-radius:30px;font-weight:700;color:#fff;font-size:24px;letter-spacing:15px;margin-top:10px; text-shadow:0 1px 2px rgba(0,0,0,.1)}
span{color:Red;font-size:12px}
.box{width:750px;height:720px;margin:0px auto;background-color:white;border-radius:15px}
</style>
<script type="text/javascript">
    $(function () {
        $("#face").uploadPreview({ Img: "ImgPr", Width: 220, Height: 220 });
        $("#regForm").validate(
                {
                    /*自定义验证规则*/
                    rules: {
                        "user.nickName": { required: true, minlength: 2 },
                        "user.trueName": { required: true, minlength: 2 },
                        "user.sex": { required: true },
                        "user.password": { required: true, minlength: 8 },
                        "rePassWord": { required: true, equalTo: password },
                        "user.mobile": { required: true, number: true },
                        "user.email": { required: true, email: true }
                    },
                    /*错误提示位置*/
                    errorPlacement: function (error, element) {
                        error.appendTo(element.siblings("span"));
                    }
                }
              );
    });


    //****************************
    //这里没有运行进去是怎么回事？？
    //function (result) {   这个也看不懂
    //****************************
    function checkNickName(nickName) {
         if($("#nickName").val()==""){
            $("#userErrorInfo").html("昵称不能为空！");
            $("#nickName").focus();
            return;
         }

         $.post("/admin/ExistUserWithUserName.aspx", { nickName: nickName },
                function (result) {   
                    //var result = eval('(' + result + ')');
                    //alert(result);
                    if (result == "False") {
                        $("#userErrorInfo").html("用户名已存在，请重新输入！");
                        $("#nickName").focus();
                    } else {
                        $("#userErrorInfo").html("");
                    }
                }
        );
    }
    function checkForm() {
        var nickName = $("#nickName").val();
        var sex = $("#sex").val();
        var password = $("#password").val();
        var rePassWord = $("#rePassWord").val();
        var mobile = $("#mobile").val();
        var email = $("#email").val();
        if (nickName == "") {
            $("#error").html("昵称不能为空！");
            return false;
        }
        if (sex == "") {
            $("#error").html("请选择性别！");
            return false;
        }
        if (password == "") {
            $("#error").html("密码不能为空！");
            return false;
        }
        if (rePassWord == "") {
            $("#error").html("确认密码不能为空！");
            return false;
        }
        if (password != rePassWord) {
            $("#error").html("密码和确认密码不一致，请重新输入！");
            return false;
        }
        if (mobile == "") {
            $("#error").html("联系电话不能为空！");
            return false;
        }
        if (email == "") {
            $("#error").html("邮箱不能为空！");
            return false;
        }
    }
</script>
</head>
<body>
	<div id="header" class="wrap">
		<jsp:include page="common/top.jsp"/>
	</div>
	<div class="box" align="center" >
			<h1 style="margin-bottom: 30px;">欢迎注册</h1>
		<form id="regForm" runat="server" style="width: 700px;" enctype="multipart/form-data" class="form-horizontal form-inline" method="post">
			<div class="control-group">
				<label class="control-label" for="nickName">昵  称(*)：</label>
				<div class="controls">
					<input class="input-block-level" type="text" id="nickName" name="user.nickName" onblur="checkNickName(this.value)" value="<%=user.nickname %>"/><span class="pull-left"></span>
					<font id="userErrorInfo" class="pull-right" color="red"></font>
				</div>
			</div>
			<div class="control-group">
				<label class="control-label" for="trueName">用户名(*)：</label>
				<div class="controls"> 
					<input class="input-block-level" type="text" id="trueName" name="user.trueName" onblur="checkTrueName(this.value)" value="<%=user.truename %>"/><span class="pull-left"></span>
				</div>
			</div>
			<div class="control-group">
				<label class="control-label" for="sex">性  别(*)：</label>
				<div class="controls">
					<label class="radio" style="margin-right: 50px;">
						<select id="sex" name="user.sex"><option value="">请选择...</option>
							<option value="女" <%=user.sex == "女"? "selected" : "" %> >女</option>
							<option value="男" <%=user.sex == "男"? "selected" : "" %>>男</option>
						</select> <span class="pull-left"></span>
					</label>
				</div>
			</div>  
			<div class="control-group" id="preDiv" style="width: 700px; height: 120px;margin-left: 80px;">
				<img id="ImgPr" class="pull-left" style="width: 120px; height: 120px;" src="<%=user.face %>" />
			</div>
			<div class="control-group">
				<label class="control-label" for="face">上传头像(*)：</label>
				<div class="controls">
					<input type="file" id="face" name="face">
				</div>
			</div>
			<div class="control-group">
				<label class="control-label" for="password">登录密码(*)：</label>
				<div class="controls">
					<input class="input-block-level" type="password" id="password"
						name="user.password" value=""/><span class="pull-left"></span>
				</div>
			</div>
			<div class="control-group">
				<label class="control-label" for="rePassWord">确认密码(*)：</label>
				<div class="controls">
					<input class="input-block-level" type="password" id="rePassWord" name="rePassWord"/><span class="pull-left"></span>
				</div>
			</div>
			<div class="control-group">
				<label class="control-label" for="moble">联系电话(*)：</label>
				<div class="controls">
					<input class="text input-block-level" type="text" id="mobile"
						name="user.mobile" value="<%=user.mobile %>"/><span class="pull-left"></span>
				</div>
			</div>
			<div class="control-group">
				<label class="control-label" for="email">电子邮箱(*)：</label>
				<div class="controls">
					<input class="text input-block-level" type="text" id="email"
						name="user.email" value="<%=user.email %>"/><span class="pull-left"></span>
				</div>
			</div>
			<div class="control-group" style="margin: 0px;">
				<div style="margin-left: 70px;">
					<button type="submit" tabindex="5">提交注册</button> &nbsp;&nbsp;&nbsp;&nbsp;
				</div>
			</div>
			<font id="error" color="red"><font id="error" color="red"><%=msg%></font></font>
			<input type="hidden" name="user.type" value="1">
		</form>
	</div>
</body>
</html>