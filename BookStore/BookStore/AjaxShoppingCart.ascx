<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AjaxShoppingCart.ascx.cs" Inherits="BookStore.AjaxShoppingCart" %>

<div id="<%= this.ID %>" class="shopping-cart">
    <div class="basket"></div>
    Sum: <div class="sum"></div>
    <div class="template">
        <div class="cart-item">
            <div class="title"></div>
            <div class="author"></div>
            <div class="id"></div>
            <input type="number" class="quantity" />
            <div class="price-sum"></div>
        </div>
    </div>
</div>