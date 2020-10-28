<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WaiterOrders.aspx.cs" Inherits="Website.WaiterOrders" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Waiter Orders</title>

    <link rel="stylesheet" href="dist/style.css"/>
    <script src="dist/general.js"></script>
    <script src="dist/jquery.js"></script>
    <script src="dist/popper.js"></script>
    <script src="dist/fontawesome.js"></script>
    <script src="dist/bootstrap.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <nav>
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
                                <a class="dropdown-item" href="Checkout.aspx">My Order</a>
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


         <div class="container">

         </div>
    </form>
</body>
</html>
