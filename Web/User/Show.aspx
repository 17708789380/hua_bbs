<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Show.aspx.cs" Inherits="hua_bbs.Web.User.Show" Title="显示页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
                <tr>                   
                    <td class="tdbg">
                               
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
	<td height="25" width="30%" align="right">
		id
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblid" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		email
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblemail" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		face
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblface" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		mobile
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblmobile" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		nickname
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblnickname" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		password
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblpassword" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		regtime
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblregtime" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		sex
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblsex" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		truename
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lbltruename" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		type
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lbltype" runat="server"></asp:Label>
	</td></tr>
</table>

                    </td>
                </tr>
            </table>
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>--%>




