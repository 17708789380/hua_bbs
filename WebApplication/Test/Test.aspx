<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="WebApplication.Test.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <table border="1">
        <%
            foreach (hua_bbs.Model.Zone zone in zoneList)
            {
        %>


             <tr>
                 <td><%=zone.id %></td>
                 <td><%=zone.name %></td>
                 <td><%=zone.description%></td>
             </tr>
        

        <%
            }
        %>
      </table>
</body>
</html>
