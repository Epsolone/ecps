﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="Ecode.PortalSystem.Mvc.PortalableView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= Html.Encode(ViewData["Message"]) %></h2>
    <%= Html.ActionLink("abc", "Index", "Home", "Secure") %>
    <p>
        To learn more about ASP.NET MVC visit <a href="http://asp.net/mvc" title="ASP.NET MVC Website">http://asp.net/mvc</a>.
    </p>
</asp:Content>
