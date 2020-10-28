<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Website.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Landing Page</title>
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



            <div class="container text-white">
                <div class="display-4">TORO COFFEE BAR</div>
                <h3>Where coffee meets expectations</h3>
                <p class="lead">Lorem ipsum dolor.</p>
                <p>Toro is a coffee bar - We love people, and we love coffee! That's why relationships and quality products are close to our hearts.</p>
                <asp:Button class="btn btn-dark" ID="orderbtnlink" runat="server" Text="Your Order" OnClick="orderbtnlink_Click" />
            </div>


            <div class="display-4 text-center text-white mt-3">Coffee!</div>

            <div class="container">
                <div class="jumbotron text-center text-sm-left row mt-sm-1 mt-md-3">
                    <div class="col-sm-12 col-md-3 col-lg-4">
                        <h3 class="text-uppercase">A heading</h3>
                        <p>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Voluptate, soluta?</p>
                    </div>
                    <div class="col-sm-12 col-md-3 col-lg-4">
                        <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Facere, veritatis. Reiciendis nihil maxime enim nisi quam, asperiores illo aperiam quod!</p>
                    </div>
                    <div class="col-sm-12 col-md-6 col-lg-4 container-fluid">
                        <img class="figure-img w-300 thumbnail" src="images/coffee.jpg" style="max-width: 300px;">
                    </div>
                    <div class="lead">
                        <asp:Button class="btn btn-dark" ID="loginbtnlink" runat="server" Text="Log In" OnClick="loginbtnlink_Click" />
                    </div>
                </div>

            </div>



        </div>
    </form>
    </body>
</html>
