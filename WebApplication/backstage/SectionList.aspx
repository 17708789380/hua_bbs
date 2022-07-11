<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SectionList.aspx.cs" Inherits="WebApplication.backstage.SectionList" %>

<!DOCTYPE html>

<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title></title>
<link rel="stylesheet" href="/admin/css/bootstrap.min.css" />
<link rel="stylesheet" href="/admin/css/bootstrap-responsive.min.css" />
<link rel="stylesheet" href="/admin/css/uniform.css" />
<link rel="stylesheet" href="/admin/css/unicorn.main.css" />
<link rel="stylesheet" href="/admin/css/unicorn.grey.css" class="skin-color" />
<script type="text/javascript" src="/js/jquery-1.11.1.js"></script>
<script type="text/javascript" src="/js/My97DatePicker/WdatePicker.js"></script>
 
<script type="text/javascript" src="/js/uploadPreview.min.js"></script>
<script type="text/javascript">
    $(function () {
        var sectionPage = "section.jsp"; var topicPage = "topic.jsp"; var userPage = "user.jsp"; var zonePage = "zone.jsp";
        var curPage = '${mainPage}';
        if (sectionPage.indexOf(curPage) >= 0 && curPage != "") {
            $("#sectionLi").addClass("active");
        } else if (topicPage.indexOf(curPage) >= 0 && curPage != "") {
            $("#topicLi").addClass("active");
        } else if (userPage.indexOf(curPage) >= 0 && curPage != "") {
            $("#userLi").addClass("active");
        } else if (zonePage.indexOf(curPage) >= 0 && curPage != "") {
            $("#zoneLi").addClass("active");
		};
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

    $(function () {
        $("#logo").uploadPreview({ Img: "ImgPr", Width: 220, Height: 220 });
    });
    function openAddDlg() {
        $("#myModalLabel").html("增加小板块");
    }
    function saveSection() {


        $("#peizid").val($("#zid").val());
        $("#peiuid").val($("#uid").val());
        $("#peisectionname").val($("#peisnname").val());
        $("#peipage").val($("li.active").text());



        if ($("#sectionName").val() == null || $("#sectionName").val() == '') {
            $("#error").html("请输入小板块名称！");
            return false;
        }
        if ($("#zone").val() == null || $("#zone").val() == '') {
            $("#error").html("请选择所属大板块！");
            return false;
        }
        if ($("#masterNickName").val() == null || $("#masterNickName").val() == '') {
            $("#error").html("请选择版主！");
            return false;
        }
        /* $.post("Section_save.action", $("#fm").serialize()); */
        $("#fm").submit();//表单提交 
      
        resetValue();
       
    }

 

    function sectionDelete(sectionId) {
        if (confirm("确定要删除这条数据吗?")) {
            $.post("Section_delete.action", { sectionId: sectionId },
                    function (result) {
                        var result = eval('(' + result + ')');
                        if (result.error) {
                            alert(result.error);
                        } else {
                            alert("删除成功！");
                            window.location.reload(true);
                        }
                    }
                );
        }
    }
    function deleteSections() {
        var selectedSpan = $(".checked").parent().parent().next("td");
        if (selectedSpan.length == 0) {
            alert("请选择要删除的数据！");
            return;
        }


        var strIds = [];
        for (var i = 0; i < selectedSpan.length; i++) {
            strIds.push(selectedSpan[i].innerHTML);
        }
       
        var ids = strIds.join(",");
  
       

        if (confirm("您确定要删除这" + selectedSpan.length + "条数据吗？")) {
            $.post("/backstage/SectionList.aspx?flag=delete", { ids: ids }, function (result) {
                if (result) {
                    alert("数据已成功删除！");
                    location.reload(true);
                } else {
                    alert("数据删除失败！");
                }
            }, "text");
        } else {
            return;
        }
    }

    function modifySection(id, name, zone, masterNickName, logo) {
        $("#myModalLabel").html("修改小板块");
        $("#id").val(id);
        $("#sectionName").val(name);
        $("#ImgPr").attr("src", logo);
        $("#zone").val(zone);
        $("#masterNickName").val(masterNickName);

    }
    function resetValue() {
        $("#id").val("");
        $("#sectionName").val("");
        $("#ImgPr").attr("src", "");
        $("#zone").val("");
        $("#masterNickName").val("");
    }
    function searchUserByNickName(userNickName) {
        $.post("Section_getUserByNickName.action", { nickName: userNickName }, function (result) {
            var result = eval('(' + result + ')');
            $("#info").html(result.info);
            $("#masterId").val(result.masterId);
        });
    }
</script>
</head>
<%--
if(session.getAttribute("currentUser")==null){
	response.sendRedirect("login.jsp");
	return;
}
--%>
<body>
	<div id="header">
		<h1 style="margin-left: 0px;padding-left: 0px;"><a href="#">盼盼论坛</a></h1>	
		<!-- <h2 style="padding: 0px; margin-top: 10px; margin-bottom: 0px;">
			<a href="#"><font color="#cccccc">C#1234论坛</font></a>
		</h2>
		<h3 style="margin: 0px 0px 0px 40px;">
			<a href="#"><font color="#cccccc">后台管理</font></a>
		</h3> -->
	</div>

	<div id="sidebar">
		<ul>
			<li id="zoneLi"><a href="ZoneList.aspx"><i class="icon icon-home"></i> <span>大板块管理</span></a></li>
			<li id="sectionLi"><a href="/backstage/SectionList.aspx"><i class="icon icon-home"></i> <span>小板块管理</span></a></li>
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
		<i class="icon-arrow-left icon-white"></i> <span>颜色:</span> 
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
		<div id="breadcrumb">
			<a href="/Index.aspx" title="首页" class="tip-bottom">
			<i class="icon-home"></i> 首页</a> <a href="#" class="current">板块管理</a>
		</div>
<!-- ------------------------------------------------------------------------------------------------------------------------------ -->    
        <div class="container-fluid">
		<div id="tooBar" style="padding: 10px 0px 0px 10px;">
			<button class="btn btn-primary" type="button" data-backdrop="static" data-toggle="modal" data-target="#dlg" onclick="return openAddDlg()">添加小板块</button>&nbsp;&nbsp;&nbsp;&nbsp;
			<a href="#" role="button" class="btn btn-danger" onclick="javascrip:deleteSections()">批量删除</a>
			<form method="post" action="/backstage/SectionList.aspx" class="form-search" style="display: inline;">
	          &nbsp;小板块名称：
			  <input name="sectionname" id="peisnname" value="<%=sectionname%>" type="text" class="input-medium search-query" placeholder="输入小板块名称..."/>
			  &nbsp;所属大板块：
			 <%-- 
                 <select name="s_section.zone.id"><option value="">请选择...</option>
				<c:forEach var="zone" items="${zoneList }">
					<option value="${zone.id }" ${s_section.zone.id==zone.id?'selected':'' }>${zone.name }</option>
				</c:forEach>
                   </select>--%>

                  <select id="zid" name="zid"><option value="">请选择...</option>
                  <%foreach( hua_bbs.Model.Zone zone in zoneList) {%>
                    <option value="<%=zone.id%>"  <%= zid == zone.id.ToString() ? "selected": ""  %> ><%=zone.name%></option>
                  <%} %>
                  </select>
			 
			  &nbsp;版主：
                <%-- 
			  <select name="s_section.master.id"><option value="">请选择...</option>
				<c:forEach var="master" items="${masterList }">
					<option value="${master.id }" ${s_section.master.id==master.id?'selected':'' }>${master.nickName }</option>
				</c:forEach>
                </select>--%>

                <select id="uid" name="uid"><option value="">请选择...</option>
				<% foreach( hua_bbs.Model.User user in  userList) {%>
					<option value="<%=user.id%>" <%= uid == user.id.ToString() ? "selected": ""%> ><%=user.nickname %></option>
				<%} %>
                </select>
			  
			  &nbsp;
			  <button type="submit" class="btn btn-primary" title="Search">查询&nbsp;<i class="icon  icon-search"></i></button>
			</form>
		</div>
		<div class="row-fluid">
			<div class="span12">
				<div class="widget-box">
					<div class="widget-title">
						<!-- <span class="icon"> <input type="checkbox"
							id="title-checkbox" name="title-checkbox" />
						</span> -->
						<h5>小板块列表</h5>
					</div>
					<div class="widget-content nopadding">
						<table class="table table-bordered table-striped with-check">
							<thead>
								<tr>
									<th><i class=""></i></th>
									<th>编号</th>
									<th>小板块名称</th>
									<th>小板块logo</th>
									<th class="th">所属大板块</th>
									<th>版主</th>
									<th>操作</th>
								</tr>
							</thead>
							<tbody>
								
                                <%foreach(hua_bbs.Model.Section section in sectionList) {%>
									<tr>
										<td><input type="checkbox" /></td>
										<td style="text-align: center;vertical-align: middle;"><%=section.id %></td>
										<td style="text-align: center;vertical-align: middle;"><%=section.name%></td>
										<td style="text-align: center;vertical-align: middle;width: 110px;vertical-align: middle;">
											<img style="width: 100px;" src='<%=section.logo%>' />
										</td>
										<td style="text-align: center;vertical-align: middle;"><%=section._zone.name %></td>
										<td style="text-align: center;vertical-align: middle;"><%=section._user.nickname%></td>
										<td style="text-align: center;vertical-align: middle;">
											<button class="btn btn-info" type="button" data-backdrop="static" data-toggle="modal" data-target="#dlg" onclick="return modifySection(<%=section.id %>,'<%=section.name%>',<%=section.t_z_id%>,'<%=section.t_u_id%>','<%=section.logo%>')">修改
											</button>&nbsp;&nbsp;<button class="btn btn-danger" type="button" onclick="javascript:sectionDelete(${section.id})">删除</button>
										</td>
									</tr>
                                <%} %>
								
							</tbody>
						</table>
					</div>
				</div>
				<div class="pagination alternate">
					<ul class="clearfix"><%=pageCode %>
					</ul>
				</div>


			</div>
		</div>
		<div id="dlg" class="modal hide fade"  tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
			<div class="modal-header">
				<button type="button" class="close" data-dismiss="modal"
					aria-hidden="true" onclick="return resetValue()">×</button>
				<h3 id="myModalLabel">增加小板块</h3>
			</div>
			<div class="modal-body">
				<form id="fm" action="SectionList.aspx" method="post" enctype="multipart/form-data">
                        <input type="hidden" value="saveOrUpdate" name="flag"/>
					<table>
						<tr>
							<td>
								<label class="control-label" for="sectionName">请输入小板块名称：</label>
							</td>
							<td>
								<input id="sectionName" type="text" name="ssname" placeholder="请输入…">
							</td>
						</tr>
						<tr>
							<td>
								<img id="ImgPr" class="pull-left" style="width: 120px; height: 120px;" />
							</td>
							<td>
							</td>
						</tr>
						<tr>
							<td>
								<label class="control-label" for="logo">上传logo：</label>
							</td>
							<td>
								<input type="file" id="logo" name="logo">
							</td>
						</tr>
						<tr>
							<td>
								<label class="control-label" for="zone">请选择所属大板块：</label>
							</td>
							<td>
                                <%-- 
								<select id="zone" name="section.zone.id"><option value="">请选择...</option>
									<c:forEach var="zone" items="${zoneList }">
										<option value="${zone.id }">${zone.name }</option>
									</c:forEach>
								</select>--%>

                                   <select id="zone" name="zzid"><option value="">请选择...</option>
                                      <%foreach( hua_bbs.Model.Zone zone in zoneList) {%>
                                        <option value="<%=zone.id%>"><%=zone.name%></option>
                                      <%} %>
                                   </select>

							</td>
						</tr>
						<tr>
							<td>
								<label class="control-label" for="masterNickName">版主：</label>
							</td>
							<td>
                                <%--
								<input id="masterNickName" type="text" name="section.master.nickName" onkeydown="javascript:searchUserByNickName(this.value)" placeholder="请输入昵称回车">
								 --%>

                                 <select id="masterNickName" name="uuid"><option value="">请选择...</option>
				                    <% foreach( hua_bbs.Model.User user in  userList) {%>
					                    <option value="<%=user.id%>"><%=user.nickname %></option>
				                    <%} %>
                                </select>

                                <font id="info" style="color: red;"></font>
							</td>
						</tr>
					</table>
					<input id="id" type="hidden" name="ssid">

                     <input type="hidden" id="peipage" name="page"/>
                     <input type="hidden" id="peizid" name="zid"/>
                     <input type="hidden" id="peiuid" name="uid"/>
                     <input type="hidden" id="peisectionname" name="sectionname"/>
					
				</form>
			</div>
			<div class="modal-footer">
				<font id="error" style="color: red;"></font>
				<button class="btn" data-dismiss="modal" aria-hidden="true"
					onclick="return resetValue()">关闭</button>
				<button class="btn btn-primary" onclick="javascript:saveSection()">保存</button>
				<!-- <button class="btn btn-primary" type="submit">保存</button> -->
			</div>
		</div>
	</div>
<!------------------------------------------------------------------------------------------------------------------------------>
        

            
		<div class="row-fluid">
			<div id="footer" class="span12">
				2022 &copy; 计应二 作者：计应二&nbsp;&nbsp;&nbsp;&nbsp; <a href="http://www.hbpu.edu.cn/">盼盼</a>
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
<%--<script src="/admin/js/unicorn.tables.js"></script>--%>
<script type="text/javascript" src="/js/uploadPreview.min.js"></script>
</body>
</html>
