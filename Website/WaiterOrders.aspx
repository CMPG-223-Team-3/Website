﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WaiterOrders.aspx.cs" Inherits="Website.WaiterOrders" %>

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
                            <a class="nav-link" href="Default.aspx"><asp:Label  ID="Label1" runat="server" Text="Home"></asp:Label></a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="CustomerLogin.aspx"><asp:Label ID="lblLogin" runat="server" Text="Login"></asp:Label></a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="Checkout.aspx"><asp:Label ID="Label3" runat="server" Text="My Order"></asp:Label></a>
                        </li> 
                    </ul>
                </div>
            </div>
        </nav>

         <div class="container">
             <div class="row">
                 <div class="col-md-12">
                    <div class="FormBox">
                        <div class="card-boddy text-centre">
                            <h5 class="card-title">Orders that have not been delivered</h5>
                            <div class="card-text">
                                <asp:GridView ID="GridView1" runat="server"></asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                 <div class="col-sm-12 col-md-12">
                      <div class="card-dark mb-4"></div>
                 </div>
                 <div class="col-sm-12 col-md-6">
                    <div class="FormBox mb-4">
                        <div class="card-boddy text-centre">
                            <h5 class="card-title">Orders delivered</h5>
                            <div class="card-text">
                                <label>Order ID:</label>
                            </div>
                            <div>
                                <asp:DropDownList ID="DropDownListOrderIds" runat="server"></asp:DropDownList>
                            </div>
                            <div>
                                <asp:RadioButtonList ID="RadioButtonListDeliveryStatus" runat="server">
                                    <asp:ListItem Selected="True" Text="Delivered"></asp:ListItem>
                                    <asp:ListItem Text="Not Delivered"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div>
                                <asp:Button ID="BtnDelivered" runat="server" Text="Button" class="btn btn-secondary myButtonRight"/>
                            </div>
                        </div>
                    </div>
                </div>
                 <div class="col-sm-12 col-md-6">
                    <div class="FormBox mb-4">
                        <div class="card-boddy text-centre">
                            <h5 class="card-title">Orders Paid</h5>
                            <div class="card-text">
                                <div>
                                    <label>Table Number:</label>
                                    <asp:TextBox AutoCompleteType="None" ID="TextBoxTableNumber" runat="server" CssClass="form-control" required="true" TextMode="Number"></asp:TextBox>
                                </div>
                                <div>
                                    <asp:Button ID="ButtonPay" runat="server" Text="Button" class="btn btn-secondary myButtonRight"/>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
             </div>
         </div>
    </form>
</body>
</html>
