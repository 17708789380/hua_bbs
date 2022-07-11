<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TopicDetails.aspx.cs" Inherits="WebApplication.topic.TopicDetails" %>

<!DOCTYPE html>

<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>帖子详情</title>
<link href="/css/emoticon.css" rel="stylesheet" type="text/css"  />
<link href="/bootstrap/css/bootstrap.css" rel="stylesheet" />
<link href="/bootstrap/css/bootstrap-responsive.css" rel="stylesheet" />
<link href="/css/style.css" rel="stylesheet" />

<script src="/js/jquery-1.11.1.js" type="text/javascript"></script>
<script src="/js/jquery.emoticons.js" type="text/javascript"></script>
<script src="/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
<script src="/bootstrap/js/bootstrap.js" type="text/javascript"></script>

<script type="text/javascript">
$(function(){
	//放新浪微博表情
    $("#message_face").jqfaceedit({txtAreaObj:$("#Content"),containerObj:$('#container'),top:25,left:-27});
	 //显示表情
	$(".show_e").emotionsToHtml();
});
function saveReply(){    
	if('${currentUser.nickName}'==''){
		alert("请先登陆，再回帖！");
		/* var url="Report_preSave.action?role=0&reportType=1";
		window.open("login.jsp?url="+url); */
		return false;
	}
	if ($("#Content").val().length<10) {
		alert("最少输入10个字符！");
		return false;
	}
	if ($("#Content").val().length>1000) {
		alert("最多输入1000个字符！");
		return false;
	}
	$.post("/topic/TopicReplySave.ashx", $("#replyForm").serialize(), function (result) {
		if(result){
			alert("回复成功！");
			location.reload(true);
		}else{
			alert("回复失败！");
		}
	},"text");
}

function deleteReply(replyId) {
    //var pageNumber = $(".active").children().html();
	if (confirm("您确定要删除这条回复吗？")) {
	    $.post("/topic/TopicReplyDelete.aspx", { replyId: replyId}, function (result) {
		    if (result) {
				alert("数据已成功删除！");
				location.reload(true);
			}else{
				alert("数据删除失败！");
			}
		},"text");
	}else{
		return;
	}
}
</script>
</head>
<body>
<div id="header" class="wrap" style="width: 1200px; margin: 0 auto;">
	 <% Server.Execute("/common/Top.aspx"); %>
</div>
<div style="width: 1200px; margin: 0 auto;">
	<div class="container-fluid" style="padding-left: 0px;padding-right: 0px;">
		<div class="row-fluid">
			<div class="span2">
				<table style="width: 100%;" cellpadding="5px;">
					<tr>
						<td>
							★楼主&nbsp;<a href="#" style="font-size: 9pt;color: black;"><strong><%=topic._topicUser.nickname %></strong></a>
						</td>
					</tr>
					<tr>
						<td>
                            <img alt="" src="<%=topic._topicUser.face %>" style="width: 100px;height: 100px;">				
						</td>
					</tr>
					<tr>
						<td>
							性别：<%=topic._topicUser.sex %>
						</td>
					</tr>
					<tr>
						<td>
							邮箱：<%=topic._topicUser.email %>
						</td>
					</tr>
					<tr>
						<td>
                            <%if (topic._topicUser.type == "1"){%>
                            <font style="color: black;">普通用户</font>
                            <%} else if (topic._topicUser.type == "2") { %>
                            <font style="color: blue;">版主</font>
                            <%} else { %>
                            <font style="color: red;">管理员</font>
                            <%} %>
                           
							</td>
					</tr>
				</table>
			</div>
			<div class="span10">
				<table style="width: 100%;">
					<tr style="height: 50px;">
						<td>
							【主题】:<strong><%=topic.title %></strong>
						</td>
					</tr>
					<tr>
						<td style="text-align: right;">
							发表时间:『<%=topic.publishtime%>』
						</td>
					</tr>
					<tr>
						<td>
							【内容】:
							<div style="width: 982px;padding:6px; background-color: #F0F0F0" class="show_e">
								<%=Server.HtmlDecode(topic.content)%>
							</div>
						</td>
					</tr>
				</table>
			</div>
		</div>
        

        <%int index = 1; %>

        <%foreach (hua_bbs.Model.Reply reply in replyList) { %>
        <div class="row-fluid" style="margin-top: 20px;">
			<div class="span2">
				<table style="width: 100%;" cellpadding="5px;">
					<tr>
						<td>
							▲<%=(pageNumber-1)*pageCount+index++ %>楼
						</td>
					</tr>
					<tr>
						<td>
                            <img alt="" src="<%=reply._replyuser.face %>" style="width: 100px;height: 100px;">
							<a href="#" style="font-size: 9pt;color: black;"><strong><%=reply._replyuser.nickname %></strong></a>
						</td>
					</tr>
					<tr>
						<td>
							性别：<%=reply._replyuser.sex %>
						</td>
					</tr>
					<tr>
						<td>
							邮箱：<%=reply._replyuser.email %>
						</td>
					</tr>
					<tr>
						<td>
							<%if (topic._topicUser.type == "1"){%>
                            <font style="color: black;">普通用户</font>
                            <%} else if (topic._topicUser.type == "2") { %>
                            <font style="color: blue;">版主</font>
                            <%} else { %>
                            <font style="color: red;">管理员</font>
                            <%} %>
							</td>
					</tr>
				</table>
			</div>
			<div class="span10">
				<table style="width: 100%;">
					<tr>
						<td style="text-align: right;">
                             <%
                               hua_bbs.Model.User user = (hua_bbs.Model.User)Session["user"];
                               if(user != null){
                                   if(user.id == section.t_u_id || user.type == "3"){%>
                                        <button class="btn btn-danger" onclick="javascript:deleteReply(<%=reply.id%>)">删除</button>
                                  <% }
                               }
                               %>              
							回复时间:『<%=reply.publishtime %>』
						</td>
					</tr>
					<tr>
						<td>
							<div style="width: 982px;padding:6px; background-color: #F0F0F0" class="show_e">
								<%=reply.content %>
							</div>
						</td>
					</tr>
				</table>
			</div>
		</div>
        <%} %>

	</div>
	<div class="pagination alternate" align="center">
         <%if (pageCode == "未查询到数据"){%>
                本帖还没有人回复
           <% }else{%>
                <ul class="clearfix"><%=pageCode%></ul>    
           <% } %>
	</div>
	<div>
		<table>
			<tr>
				<td style="width: 20%;">
					回帖许可：
				</td>
				<td style="width: 80%;">
					<form id="replyForm" class="form-horizontal" style="margin-top: 10px;">
					<table style="width: 100%;" cellpadding="10px;">
						 <%--<tr>
							<td>
								【主题】:
							</td>
							<td>
								<input type="text" id="rTopic" name="reply.rTopic" value=""${reply.rTopic }" style="width: 800px;">
							</td>
						</tr> --%>
						<tr>
							<td>
								【表情】:
							</td>
							<td>
								<div id="container">
								<a href="JavaScript:void(0)" id="message_face">请选择...</a>
								</div>
							</td>
						</tr>
						<tr>
							<td style="vertical-align: top;">
								【内容】:
							</td>
							<td>
								<textarea name="reply.content" id="Content" cols="50" style="height:200px;width: 800px;" ></textarea>
							</td>
						</tr>
						<tr>
							<td>
								<input id="userId" name="reply.user.id" value="<%=nowUser.id %>" type="hidden"/>
								<input id="topicId" name="reply.topic.id" value="<%=topic.id %>" type="hidden"/>
							</td>
							<td>
								<Button class="btn btn-primary " data-dismiss="modal" aria-hidden="true" type="button" onclick="javascript:saveReply()">提交</Button>
								<font id="error"></font>
							</td>
						</tr>
					</table>
					</form>
				</td>
			</tr>
		</table>
	</div>
</div>
<div id="footer" style="width: 1200px; margin: 0 auto;">
	<% Server.Execute("/common/Footer.aspx"); %>   
</div>

<script type="text/javascript">
       
</script>
</body>
</html>