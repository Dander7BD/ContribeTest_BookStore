<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="BookStore.Index" %>
<%@ Register TagPrefix="book" TagName="Search" Src="~/AjaxBookSearch.ascx" %>
<%@ Register TagPrefix="book" TagName="Cart" Src="~/AjaxShoppingCart.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Contribe test submission</title>
</head>
<body runat="server">
    <form id="MainContent" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <book:Search ID="BookShelf" runat="server" />
        <book:Cart ID="ShoppingCart" runat="server" />
    </form>
    <footer>
        <p>Created by Dan Andersson 2017</p>
    </footer>
</body>
</html>
