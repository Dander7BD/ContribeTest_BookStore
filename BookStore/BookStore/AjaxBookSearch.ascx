<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AjaxBookSearch.ascx.cs" Inherits="BookStore.Web.AjaxBookSearch" %>

<div id="<%= this.ID %>" class="book-search">
    
    <input type="search" class="filter-input" />
    <div class="response"></div>
    <div class="template">
        <div class="book-item">
            <div class="title"></div>
            <div class="author"></div>
            <div class="price"></div>
            <div class="id"></div>
        </div>
    </div>
</div>