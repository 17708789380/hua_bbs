<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Show.aspx.cs" Inherits="hua_bbs.Web.Topic.Show" Title="显示页" %>
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
		t_u_id
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblt_u_id" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		t_s_id
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblt_s_id" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		content
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblcontent" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		modifytime
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblmodifytime" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		publishtime
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblpublishtime" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		title
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lbltitle" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		good
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblgood" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		top
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lbltop" runat="server"></asp:Label>
	</td></tr>
</table>

                    </td>
                </tr>
            </table>
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>--%>




