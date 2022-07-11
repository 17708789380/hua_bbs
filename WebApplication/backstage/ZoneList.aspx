<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZoneList.aspx.cs" Inherits="WebApplication.backstage.ZoneList1" %>

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


    function openAddDlg() {
        $("#myModalLabel").html("增加大板块");
    }

    function saveZone() {
        //alert($("#zoneName").val());

        if ($("#zoneName").val() == null || $("#zoneName").val() == '') {
            $("#error").html("请输入大板块名称！");
            return false;
        }
       
        $.post("/backstage/ZoneSave.ashx", $("#fm").serialize(), function (data) {
            if (data) {
                alert("保存成功！");
                location.reload(true);
            }else{
                alert("添加大块板失败");
            }
        },"text");
       
       
       
    }

    function modifyZone(id, name, description) {
        $("#myModalLabel").html("修改大板块");
        $("#id").val(id);
        $("#zoneName").val(name);
        $("#description").val(description);
    }


    function zoneDelete(zoneId) {
        if (confirm("确定要删除这条数据吗?")) {
            $.post("/backstage/ZoneDelete.ashx", { zoneId: zoneId },
                    function (result) {
                        if (result) {
                            alert("删除成功！");
                            window.location.reload(true);
                           
                        } else {
                            alert("删除失败");
                        }
                    },"text");
        }
    }
    function resetValue() {
       
        $("#id").val("");
        $("#zoneName").val("");
        $("#description").val("");
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
			<a href="#"><font color="#cccccc">Java1234论坛</font></a>
		</h2>
		<h3 style="margin: 0px 0px 0px 40px;">
			<a href="#"><font color="#cccccc">后台管理</font></a>
		</h3> -->
	</div>

	<div id="sidebar">
		<ul>
			<li id="zoneLi"><a href="/backstage/ZoneList.aspx"><i class="icon icon-home"></i> <span>大板块管理</span></a></li>
			<li id="sectionLi"><a href="/backstage/SectionList.aspx"><i class="icon icon-home"></i> <span>小板块管理</span></a></li>
			<%--<li id="topicLi"><a href="/backstage/TopicListAdmin.aspx"><i class="icon icon-home"></i> <span>帖子管理</span></a></li>--%>
			<!-- <li><a href="#"><i class="icon icon-home"></i> <span>回复管理</span></a></li> -->
			<%--<li id="userLi"><a href="/backstage/UserList.aspx"><i class="icon icon-home"></i> <span>用户管理</span></a></li>--%>
			<li class="submenu"><a href="#"><i class="icon icon-th-list"></i>
					<span>系统管理</span> <span class="label">3</span></a>
				<ul>
					li><a href="/backstage/Pwd.aspx">修改密码</a></li>
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
			<i class="icon-home"></i> 首页</a> <a href="#" class="current">大板块</a>
		</div>
		
        <!-- 主题 crud 操作-->
            
        <div class="container-fluid">
		<div id="tooBar" style="padding: 10px 0px 0px 10px;">
			<button class="btn btn-primary" type="button" data-backdrop="static" data-toggle="modal" data-target="#dlg" onclick="return openAddDlg()">添加大板块</button>&nbsp;&nbsp;&nbsp;&nbsp;
		</div>
		<div class="row-fluid">
			<div class="span12">
				<div class="widget-box">
					<div class="widget-title">
						<!-- <span class="icon"> <input type="checkbox"
							id="title-checkbox" name="title-checkbox" />
						</span> -->
						<h5>大板块列表</h5>
					</div>
					<div class="widget-content nopadding">
						<table class="table table-bordered table-striped with-check">
							<thead>
								<tr>
									<th><i class=""></i></th>
									<th>编号</th>
									<th>大板块名称</th>
									<th>大板块备注</th>
									<th>操作</th>
								</tr>
							</thead>
							<tbody>

                                <%
                                    foreach(hua_bbs.Model.Zone zone in zoneList){ 
                                     %>
                                <tr>
										<td><input type="checkbox" /></td>
										<td style="text-align: center;"><%=zone.id %> </td>
										<td style="text-align: center;"><%=zone.name %> </td>
										<td style="text-align: center;"><%=zone.description %> </td>
										<td style="text-align: center;">
											<button class="btn btn-info" type="button" data-backdrop="static" data-toggle="modal" data-target="#dlg" onclick="return modifyZone(<%=zone.id %>,'<%=zone.name %>','<%=zone.description %>')">修改
											</button>&nbsp;&nbsp;<button class="btn btn-danger" type="button" onclick="javascript:zoneDelete(<%=zone.id %>)">删除</button>
										</td>
									</tr>

                                <%} %>
							</tbody>
						</table>
					</div>
				</div>
				<div class="pagination alternate">
					<ul class="clearfix">
                       <%=pageCode %>
					</ul>
				</div>


			</div>
		</div>
		<div id="dlg" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
			<div class="modal-header">
				<button type="button" class="close" data-dismiss="modal"
					aria-hidden="true" onclick="return resetValue()">×</button>
				<h3 id="myModalLabel">增加大板块</h3>
			</div>
			<div class="modal-body">
				<form id="fm" action="">
					<table>
						<tr>
							<td>
								<label class="control-label" for="zoneName">请输入大板块名称：</label>
							</td>
							<td>
								 <input id="zoneName" type="text" name="name" placeholder="请输入…">
							</td>
						</tr>
						<tr>
							<td>
								<label class="control-label" for="description">请输入备注：</label>
							</td>
							<td>
								<textarea rows="5" cols="50" style="width: 405px;" id="description" name="description"></textarea>
							</td>
						</tr>
					</table>
					<input id="id" type="hidden" name="id">
				</form>
			</div>
			<div class="modal-footer">
				<font id="error" style="color: red;"></font>
				<button class="btn" data-dismiss="modal" aria-hidden="true"
					onclick="return resetValue()">关闭</button>
				<button class="btn btn-primary" onclick="javascript:saveZone()">保存</button>
			</div>
		</div>

	</div>


        <!-- 主题 crud 操作-->

		<div class="row-fluid">
			<div id="footer" class="span12">
				2022 &copy; 计应二 作者：计应二&nbsp;&nbsp;&nbsp;&nbsp; <a href="#">盼盼论坛</a>
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