<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication.common.Default" %>

<!DOCTYPE html>

<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>Insert title here</title>
</head>
<body>
	<table border="0" width="100%" cellspacing="0" cellpadding="0" style="margin-top: 8;">
	
        <%foreach (hua_bbs.Model.Zone zone in zoneList) { %>
			<tr>
				<td>
					<table style="width: 1200px;" align="center">
						<tr height="30"><td style="text-indent:5;" background="images/index/classT.jpg"><b><font color="white">■ <%=zone.name %></font></b></td></tr>
						<tr>
							<td>
								<ul class="unstyled inline" >
									<%  
                                        hua_bbs.BLL.TopicService topicService = new hua_bbs.BLL.TopicService();
                                        hua_bbs.BLL.UserService userService = new hua_bbs.BLL.UserService();
                                        foreach (hua_bbs.Model.Section section in sectionList)
                                        {
                                            if (zone.id == section.t_z_id)
                                            {%>
										<li style="width: 394px; margin-left: 0px;padding: 0px;">
											<div align="center" style="margin-top: 20px;">
												<div><a href="/topic/TopicList.aspx?sectionId=<%=section.id %>"><img style="width: 100px;" alt="" src="<%=section.logo %>"></a></div>					
                                            <div style="margin: 10px auto;"> <a href="/topic/TopicList.aspx?sectionId=<%=section.id %>"><font style="font-size: 30px;font-weight: bold;"><%=section.name %></font></a></div>
												<font style="font-size: 12px;">帖子总数：<%=topicService.GetRecordCount("t_s_id='"+section.id+"'") %></font>
												<font style="font-size: 12px;">精华帖子：<%=topicService.GetRecordCount("t_s_id='"+section.id+"' and good='1'")%></font>
												<font style="font-size: 12px;">未回复：<%=topicService.replyTopic(section.id) %></font>
												<font style="font-size: 12px;">版主：<%= userService.GetModel(section.t_u_id).nickname %></font>
											</div>
										</li>
									<%}
                                        } %>
								</ul>
							</td>
						</tr>
                        <tr height="25"><td style="text-indent:10" background="images/index/boardE.jpg"><font color="#F9F9F9)">论坛介绍：<%=zone.description %></font></td></tr>
					</table>
				</td>
			</tr>
        <%} %>
	</table>
</body>
</html>