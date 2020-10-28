<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderStatus.aspx.cs" Inherits="Website.OrderStatus" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Your Order Status</title>
    <link rel="stylesheet" href="dist/style.css"/>
    <script src="dist/general.js"></script>
    <script src="dist/jquery.js"></script>
    <script src="dist/popper.js"></script>
    <script src="dist/fontawesome.js"></script>
    <script src="dist/bootstrap.js"></script>

</head>
<body>
    <form id="form1" runat="server">

        <!--Nav bar-->
        <nav class="navbar navbar-expand-md navbar-dark sticky-top mb-sm-1 mb-md-2 mb-lg-3">
            <div class="container-fluid">
                <a class="navbar-brand thumbnail navPic" href="#">
                    <img src="images/logo.jpg" style="max-height: 40px" /></a>
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarResponsive">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarResponsive">
                    <ul class="navbar-nav ml-auto">
                        <li class="nav-item">
                            <!--<a class="nav-link" href="Default.aspx">Home</a>-->
                            <a class="nav-link" href="Default.aspx"><asp:Label  ID="Label1" runat="server" Text="Home"></asp:Label></a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="CustomerLogin.aspx"><asp:Label ID="lblLogin" runat="server" Text="Login"></asp:Label></a>
                        </li>
                        <li class="nav-item">
                            <!--<a class="nav-link" href="Checkout.aspx">My Order</a>-->
                            <a class="nav-link" href="OrderStatus.aspx"><asp:Label ID="Label3" runat="server" Text="My Order"></asp:Label></a>
                        </li> 
                    </ul>
                </div>
            </div>
        </nav>


        <div class="container">
            <div>
                <h1 class="align-middle mt-3">Welcome!</h1>
                <h3 class="align-middle">You can see your order status here! For any enquiries, please call a waiter to assist you</h3>
            </div>
            <div class="bordered mt-5">
                <div>
                    <label class="control-label" for="pnl1">Order Items:</label>
                    <asp:Panel class="container align-self-center FormBox" ID="pnl1" runat="server"></asp:Panel>
                </div>
                <div>
                    <label class="control-label mt-2" for="pnlstatus">Status:</label>
                    <asp:Panel class="container align-self-center FormBox" ID="pnlstatus" runat="server"></asp:Panel>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
