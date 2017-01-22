<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BookStore.WebForm1" %>
<%@ Register TagPrefix="book" TagName="Search" Src="~/AjaxBookSearch.ascx" %>

<asp:Content ID="Bookstore" ContentPlaceHolderID="MainContent" runat="server">
    <book:Search ID="BookShelf" runat="server" />
</asp:Content>
