<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebApplication.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>论坛首页</title>

    <link href="/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="/bootstrap/css/bootstrap-responsive.css" rel="stylesheet" />
    <script src="/bootstrap/js/jquery.js"></script>
    <script src="/bootstrap/js/bootstrap.min.js"></script>
    <script src="/bootstrap/js/bootstrap.js"></script>
</head>
<body>
    <div id="header" class="wrap" style="width: 1200px; margin: 0 auto;">

        <% Server.Execute("/common/Top.aspx"); %>
    </div>
    <div id="content" style="width: 1200px; margin: 0 auto;">


        <% Server.Execute("/common/Default.aspx"); %>
    </div>
    <div id="footer" style="width: 1200px; margin: 0 auto;">

        <% Server.Execute("/common/Footer.aspx"); %>
    </div>
</body>
</html>
