<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WaiterLogin.aspx.cs" Inherits="Website.WaiterLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WaiterLogin</title>

    <link rel="stylesheet" href="dist/style.css"/>
    <script src="dist/general.js"></script>
    <script src="dist/jquery.js"></script>
    <script src="dist/popper.js"></script>
    <script src="dist/fontawesome.js"></script>
    <script src="dist/bootstrap.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar navbar-expand-md navbar-dark sticky-top mb-sm-1 mb-md-2 mb-lg-3">
            <div class="container-fluid">
                <a class="navbar-brand thumbnail navPic" href="#"><img src="images/logo.jpg" style="max-height: 40px"/></a>
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarResponsive">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarResponsive">
                    <ul class="navbar-nav ml-auto">
                        <li class="nav-item">
                            <a class="nav-link" href="Default.aspx">Home</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link active" href="#">Order</a>
                        </li>
                        <li class="nav-item dropdown">
                            <a class="nav-link dropdown-toggle" href="#" id="navbarDropDown" data-toggle="dropdown"><asp:Label ID="lblLogin" runat="server" Text="Login" href="#"></asp:Label></a>
                            <div class="dropdown-menu">
                                <a class="dropdown-item" href="OrderStatus.aspx">My Order</a>
                                <a class="dropdown-item" href="#">Item2</a>
                                <a class="dropdown-item" href="#">Item3</a>
                                <div class="dropdown-divider"></div>
                                <a class="dropdown-item" href="#">Item4</a>
                            </div>
                        </li>
                        <li class="nav-item">
                                
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="#">Contact</a>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>


        <div class="container mt-3">
            <div class="row">
                <div class="col-sm-12 col-md-6"><!--left side-->
                    <div>
                        <div class="Logo row"><asp:Image runat="server" ImageUrl="~/images/logo.jpg"/></div>
                        <h3 class="row">Welcome back !</h3>
                        <p class="row">You feel lost?</p>
                        <p class="row">Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ullam non suscipit quibusdam nobis saepe. Architecto sunt sit odio magni facere.</p>
                        <ul>
                            <li>
                                point 1
                            </li>
                            <li>
                                point 2
                            </li>
                            <li>
                                point 3
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="col-sm-12 col-md-6 FormBox"><!--right side-->
                    <fieldset class="field form-group">
                        <div class="m-b-5 row">
                            <h2><a href="" class="linkInActive">Login</a> / <a href="WaiterLogin.aspx" class="active linkActive"> Waiter</a></h2>
                        </div>
                        <div class="row">
                            <asp:Label ID="lblStatus" runat="server" Text="" CssClass="text"></asp:Label>
                        </div>
                        <div class="row">
                            <!--<asp:Label CssClass="col-3" ID="Label1" runat="server" Text="Username: "></asp:Label>-->
                            <label class="control-label mt-2">Username:</label>
                            <asp:TextBox AutoCompleteType="DisplayName" ID="txtName" runat="server" placeholder="UserName" CssClass="form-control col-9" required="true"></asp:TextBox>
                        </div>
                        <div class="row">
                            <!--<asp:Label CssClass="col-3" ID="Label2" runat="server" Text="Password: "></asp:Label>-->
                            <label class="control-label mt-2">Password:</label>
                            <asp:TextBox ID="txtPassword" runat="server" placeholder="Password" CssClass="form-control col-9" TextMode="Password" required="true"></asp:TextBox>
                            <div><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPassword" ErrorMessage="* This is a required field" CssClass="text"></asp:RequiredFieldValidator></div>
                        </div>
                        <div class="row">
                            <asp:Button ID="Button1" runat="server" Text="Sign In" CssClass="btn btn-dark" OnClick="Button1_Click"/>
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
