<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TopicList.aspx.cs" Inherits="WebApplication.topic.TopicList" %>

<!DOCTYPE html>

<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title><%=section.name %>板块详情</title>

<link rel="stylesheet" href="/admin/css/bootstrap.css" />
<link rel="stylesheet" href="/admin/css/bootstrap-responsive.min.css" />
<link rel="stylesheet" href="/admin/css/uniform.css" />


<script type="text/javascript" src="/js/jquery-1.11.1.js"></script>
<script src="/admin/js/jquery.min.js"></script>
<script src="/admin/js/jquery.ui.custom.js"></script>
<script src="/admin/js/bootstrap.min.js"></script>

<script src="/admin/js/jquery.dataTables.min.js"></script>
    <script src="/js/ckeditor/ckeditor.js"></script>
<script src="/admin/js/unicorn.tables.js"></script>
<script type="text/javascript">

    //******************************************************
    //另一种方法
    var HtmlUtil = {
        /*1.用浏览器内部转换器实现html转码*/
        htmlEncode: function (html) {
            //1.首先动态创建一个容器标签元素，如DIV
            var temp = document.createElement("div");
            //2.然后将要转换的字符串设置为这个元素的innerText(ie支持)或者textContent(火狐，google支持)
            (temp.textContent != undefined) ? (temp.textContent = html) : (temp.innerText = html);
            //3.最后返回这个元素的innerHTML，即得到经过HTML编码转换的字符串了
            var output = temp.innerHTML;
            temp = null;
            return output;
        },
        /*2.用浏览器内部转换器实现html解码*/
        htmlDecode: function (text) {
            //1.首先动态创建一个容器标签元素，如DIV
            var temp = document.createElement("div");
            //2.然后将要转换的字符串设置为这个元素的innerHTML(ie，火狐，google都支持)
            temp.innerHTML = text;
            //3.最后返回这个元素的innerText(ie支持)或者textContent(火狐，google支持)，即得到经过HTML解码的字符串了。
            var output = temp.innerText || temp.textContent;
            temp = null;
            return output;
        }
    };

    //******************************************************

function deleteTopic(topicId){
    if (confirm("您确定要删除这条数据吗？")) {     
		$.post("/topic/TopicDelete.aspx",{topicId:topicId},function(result){
			if(result){
				/* var result=eval('('+result+')'); */
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
    //版主和管理员对帖子进行 修改（置顶+精华）和删除
function modifyTopic(topicId, topicTop, topicGood) {
	$("#topicId").val(topicId);
	$("#topicTop").val(topicTop);
	$("#topicGood").val(topicGood);
}

    //普通用户修改帖子内容和标题
function ModifyTopicContent(topicId, topicTop, topicGood) {
    $("#topicId").val(topicId);
    $("#topicTop").val(topicTop);
    $("#topicGood").val(topicGood);
}

function saveTopic(){
    var topicId = $("#topicId").val();   // document.getElementById("topicId").value
	var topicTop=$("#topicTop").val();
	var topicGood = $("#topicGood").val();

	$.post("/topic/TopicModify.ashx",{topicId:topicId,topicTop:topicTop,topicGood:topicGood},function(result){
		if (result) {
			alert("数据已成功修改！");
			location.reload(true);
		} else {
			alert("数据修改失败！");
		}
	},"text");
}

    //点击重写时候，获得相关参数
function mySetValue(topicId, title, content) {      //这三句话 ， 姑且认为 从右往左看
   // alert(topicId);
    //alert(title);
   // alert(content);

    //topic_title_2 topic_id2  tocip_content2
    $("#topic_title_2").val(title);                        //  "函数的形参title"      传递给    "[页面控件id 叫做 title]的 数值"   
    $("#topic_id2").val(topicId);                   //  "函数的形参topicId"    传递给    "[页面控件id 叫做 topicIds]的 数值"
    CKEDITOR.instances.tocip_content2.setData(content);   //  "函数的形参content"    传递给    "编辑器的 content" 
    
}

    //点击提交时候， 重写帖子内容， 参数从上面的 mySetValue（）函数里面获取
function updateTopic() {
    alert(111);
    var title = $("#topic_title_2").val();
    var topicId = $("#topic_id2").val();
    var content = HtmlUtil.htmlEncode(CKEDITOR.instances.tocip_content2.getData());

    $.post("/topic/ModifyTopicContent.ashx", { topicId: topicId, title: title, content: content }, function (result) {
        if (result) {
            alert("数据已成功修改！");
            location.reload(true);//网页重新加载
        } else {
            alert("数据修改失败！");
        }
    }, "text");
}

</script>
</head>
<body>
<div id="header" class="wrap" style="width: 1200px; margin: 0 auto;">
    <% Server.Execute("/common/Top.aspx"); %>
</div>
<div style="width: 1200px; margin: 0 auto;">
    <h1 align="center">欢迎进入<%=section.name %>版面！</h1>
	<h4>版主：<%= section._user.nickname%></h4>
	<h4><%=section._zone.description %></h4>

</div>
<div style="width: 1200px; margin: 0 auto;">
	<div style="margin-bottom: 10px;">
		<a class="" href="/topic/TopicAdd.aspx?sectionId=<%=section.id %>"><img alt="发帖" src="/images/post.jpg"></a>
		<div class="pagination alternate pull-right" align="center" style="margin: 0px;">
			<ul class="clearfix"><%=pageCode %>   <%--//分页的代码--%>
			</ul>
		</div>
	</div>
	<table border="0" width="100%" cellspacing="0" cellpadding="0" style="margin-top: 8;">
		<!-- 置顶帖子 -->
		<!-- <tr height="30">
			<td style="text-indent:5;" background="images/index/classT.jpg"><b><font color="white">■ 置顶帖子</font></b></td>
		</tr> -->
		<tr>
			<td>
				<table class="table table-bordered" width="100%" cellspacing="0" cellpadding="0" style="margin-top: 8;">
					<tr>
						<th style="text-align: center;vertical-align: middle; width: 150px;">
							状态
						</th>
						<th style="text-align: center;vertical-align: middle;">
							帖子标题
						</th>
						<th style="text-align: center;vertical-align: middle; width: 100px;">
							回复数 	
						</th>
						<th style="text-align: center;vertical-align: middle; width: 100px;">
							发表者
						</th>
						<th style="text-align: center;vertical-align: middle; width: 200px;">
							最后回复
						</th>
						<th style="text-align: center;vertical-align: middle; width: 150px;">
							操作
						</th>
					</tr>
         
                    <!-- ღღღღღღღღღღღღღღღღღღღღღღღღღღღღღღღღღღღღ置顶帖子ღღღღღღღღღღღღღღღღღღღღღღღღღღღღღღღღღღღღ-->           
					
                    <% foreach (hua_bbs.Model.Topic zdTopic in zdTopicList)
                        { %>
                    <tr>
							<td style="text-align: center;vertical-align:middle;">
                                <%
                                   if (zdTopic.good == "0")
                                   {
                                       Response.Write("<span style='color: blue;'>【普通】</span>");
                                   }
                                   else { 
                                         Response.Write("<span style='color: red;'>【精华】</span>");
                                   }
                                     %>
                                【置顶】
							</td>
							<td style="text-align: center;vertical-align:middle;">
								<a href="/topic/TopicDetails.aspx?topicId=<%=zdTopic.id %>&Page=1"><%=zdTopic.title%></a>
							</td>
							<td style="text-align: center;vertical-align:middle;">
                                <%=zdtopicReplyCount.ContainsKey(zdTopic.id) ? zdtopicReplyCount[zdTopic.id] : 0%>
							</td>
							<td style="text-align: center;vertical-align:middle;">
                                <%=zdTopic._topicUser.nickname %>
							</td>
							<td style="text-align: center;vertical-align:middle; width: 200px;">
								<strong>  <%=zdtopicLastReply.ContainsKey(zdTopic.id) ? zdtopicLastReply[zdTopic.id]._replyuser.nickname : ""  %></strong><br>
								
                                 <%=zdtopicLastReply.ContainsKey(zdTopic.id) ? zdtopicLastReply[zdTopic.id].publishtime : null %>
							</td>
							<td style="text-align: center;vertical-align:middle;">
								<% 
                                    hua_bbs.Model.User user = (hua_bbs.Model.User)Session["user"];//当前登录的用户对象
                                    if (user != null)
                                    {
                                        if (user.id == zdTopic._topicUser.id && user.id != section._user.id)//此主贴是自己发的,但自己不是版主
                                        {
                                 %>
                                            <button class="btn btn-info" data-backdrop="static" data-toggle="modal" data-target="#dlg2" onclick="return mySetValue(<%=zdTopic.id%>,'<%=zdTopic.title%>', '<%=(System.Web.HttpUtility.HtmlDecode(zdTopic.content)).Substring(0,(System.Web.HttpUtility.HtmlDecode(zdTopic.content)).Length-2)%>')">重写</button>
                                <%      
                                    }
                                    else if (user.id == section._user.id) //是版主
                                    {
                                        if(user.id == zdTopic._topicUser.id) //且帖子是自己发的
                                        { %>
                                            <button class="btn btn-info" data-backdrop="static" data-toggle="modal" data-target="#dlg2" onclick="return mySetValue(<%=zdTopic.id%>,'<%=zdTopic.title%>', '<%=(System.Web.HttpUtility.HtmlDecode(zdTopic.content)).Substring(0,(System.Web.HttpUtility.HtmlDecode(zdTopic.content)).Length-2)%>')">重写</button>
                                        <%} %> 

                                        <button class="btn btn-info" data-backdrop="static" data-toggle="modal" data-target="#dlg" onclick="return modifyTopic(<%=zdTopic.id %>,<%=zdTopic.top %>,<%=zdTopic.good %>)">修改</button>
										<button class="btn btn-danger" onclick="javascript:deleteTopic(<%=zdTopic.id %>)">删除</button>
                                <%  }
                                        else if (user.type == "3")
                                        {%>
                                        <button class="btn btn-info" data-backdrop="static" data-toggle="modal" data-target="#dlg" onclick="return modifyTopic(<%=zdTopic.id %>,<%=zdTopic.top %>,<%=zdTopic.good %>)">修改</button>
										<button class="btn btn-danger" onclick="javascript:deleteTopic(<%=zdTopic.id %>)">删除</button>
                                 <%}
                                        else
                                        { %>     
                                        您无权对本贴进行操作
                                <%}
                                    }
                                    else
                                    {
                                        Response.Write("您无权对本贴进行操作");
                                    }
                                  
                                   %> 

                    <%} %>
							</td>
						</tr>
				</table>
			</td>
		</tr>
		
		<!-- ღღღღღღღღღღღღღღღღღღღღღღღღღღღღღღღღღღღღ普通帖子ღღღღღღღღღღღღღღღღღღღღღღღღღღღღღღღღღღღღ-->

		<tr>
			<td>
				<table class="table table-bordered" width="100%" cellspacing="0" cellpadding="0" style="margin-top: 8;">

                    <% 
                        foreach (hua_bbs.Model.Topic topic in ptTopicList) { %>      
                    <tr>
							<td style="text-align: center;vertical-align:middle;width: 150px;">
								
                                 <%
                                    if(topic.good == "0"){
                                 %>
                                    <span style="color: blue;">【普通】</span>
								<%}else{ %>
                                    <span style="color: red;">【精华】</span>
                                <%} %>

							</td>
							<td style="text-align: center;vertical-align:middle;">
								<a href="/topic/TopicDetails.aspx?topicId=<%=topic.id %>&page=1"><%=topic.title%></a>
							</td>
							<td style="text-align: center;vertical-align:middle;width: 100px;">
							
                                <%= topicReplyCount.ContainsKey(topic.id) ? topicReplyCount[topic.id] : 0%>
							</td>
							<td style="text-align: center;vertical-align:middle;width: 100px;">
								
                                <%=topic._topicUser.nickname%>
							</td>
							<td style="text-align: center;vertical-align:middle;width: 200px;">
								<strong>
                                    <%=topicLastReply.ContainsKey(topic.id) ? topicLastReply[topic.id]._replyuser.nickname : ""  %>
								</strong><br>
								
                                    <%=topicLastReply.ContainsKey(topic.id) ? topicLastReply[topic.id].publishtime : null %>
							</td>
							<td style="text-align: center;vertical-align:middle;width: 150px;">
								 <% 
                                    hua_bbs.Model.User user = (hua_bbs.Model.User)Session["user"];//当前登录的用户对象
                                    if (user != null)
                                    {
                                        if (user.id == topic._topicUser.id && user.id != section._user.id)//此主贴是不是自己发的   
                                        { %>
                                           <button class="btn btn-info" data-backdrop="static" data-toggle="modal" data-target="#dlg2" onclick="return mySetValue(<%=topic.id%>,'<%=topic.title%>','<%=(System.Web.HttpUtility.HtmlDecode(topic.content)).Substring(0,(System.Web.HttpUtility.HtmlDecode(topic.content)).Length-2)%>')">重写</button>
                                         <%}
                                        else if (user.id == section._user.id)
                                        {//判断是不是板主
                                            if(user.id == topic._topicUser.id) //且帖子是自己发的
                                            { %>
                                                <button class="btn btn-info" data-backdrop="static" data-toggle="modal" data-target="#dlg2" onclick="return mySetValue(<%=topic.id%>,'<%=topic.title%>','<%=(System.Web.HttpUtility.HtmlDecode(topic.content)).Substring(0,(System.Web.HttpUtility.HtmlDecode(topic.content)).Length-2)%>')">重写</button>
                                            <%} %>  
                                        <button class="btn btn-info" data-backdrop="static" data-toggle="modal" data-target="#dlg" onclick="return modifyTopic(<%=topic.id %>, '<%=topic.top %>', '<%=topic.good %>')">修改</button>
										<button class="btn btn-danger" onclick="javascript:deleteTopic(<%=topic.id %>)">删除</button>
                                      <%}
                                        else if (user.type == "2")
                                        {%>
                                        <button class="btn btn-info" data-backdrop="static" data-toggle="modal" data-target="#dlg" onclick="return modifyTopic(<%=topic.id %>, '<%=topic.top %>', '<%=topic.good %>')">修改</button>
										<button class="btn btn-danger" onclick="javascript:deleteTopic(<%=topic.id %>)">删除</button>
                                      <%}
                                        else
                                        { %>     
                                        您无权对本贴进行操作
                                      <%}
                                    }
                                    else
                                    {
                                        Response.Write("您无权对本贴进行操作");
                                    }
                                  
                                   %>  
								</c:choose>
							</td>
						</tr>
                            
                     <%       
                        }
                       %>

				</table>
			</td>
		</tr>
	</table>
</div>
<div id="dlg" class="modal hide fade"  tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
			<div class="modal-header">
				<button type="button" class="close" data-dismiss="modal"
					aria-hidden="true" onclick="return resetValue()">×</button>
				<h3 id="myModalLabel">修改主题</h3>
			</div>
			<div class="modal-body">
				<form id="fm" action="#" method="post" enctype="multipart/form-data">
					<table>
						<tr>
							<td>
								<label class="control-label" for="top">置顶：</label>
							</td>
							<td>
								<select id="topicTop">
									<option value="0">非置顶</option>
									<option value="1">置顶</option>
								</select>
							</td>
						</tr>
						<tr>
							<td>
								<label class="control-label" for="good">精华：</label>
							</td>
							<td>
								<select id="topicGood">
									<option value="0">非精华</option>
									<option value="1">精华</option>
								</select>
							</td>
						</tr>						
					</table>
					<input id="topicId" type="hidden">
				</form>
			</div>
			<div class="modal-footer">
				<font id="error" style="color: red;"></font>
				<button class="btn" data-dismiss="modal" aria-hidden="true"
					onclick="return resetValue()">关闭</button>
				<button class="btn btn-primary" onclick="javascript:saveTopic()">保存</button>
			</div>
</div>

    <%--//普通用户 修改按钮 使用的模态对话框样式 --%>
    <div id="dlg2" class="modal hide fade"  tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
			<div class="modal-header">
				<button type="button" class="close" data-dismiss="modal"
					aria-hidden="true" onclick="return resetValue()">×</button>
				<h3 id="myModalLabel">修改帖子内容</h3>
			</div>
			<div class="modal-body">
				<form id="fm" action="#" method="post" enctype="multipart/form-data">
					<table>
						<tr>
							<td>
								<label class="control-label" for="top">帖子标题：</label>
							</td>
						</tr>
                        <tr>
							<td>
                                
                                <input type="text" id="topic_title_2" />
								
							</td>
						</tr>  
						<tr>
							<td>
								<label class="control-label" for="good">帖子内容：</label>
							</td>
						</tr>
                        <tr>       
							<td>   <%--topic_title_2 topic_id2  tocip_content2--%>
                                <textarea  id="tocip_content2" class="ckeditor" cols="50" style="height:200px;width: 500px;" ></textarea>
							</td>
						</tr>							
					</table>
					<input id="topic_id2" type="hidden" value="">
				</form>
			</div>
			<div class="modal-footer">
				<font id="error" style="color: red;"></font>
				<button class="btn" data-dismiss="modal" aria-hidden="true"
					onclick="return resetValue()">关闭</button>
				<button class="btn btn-primary" onclick="javascript:updateTopic()">修改完成</button>
			</div>  
</div>
<div id="footer" style="width: 1200px; margin: 0 auto;">
    <% Server.Execute("/common/Footer.aspx"); %>
</div>
</body>
</html>