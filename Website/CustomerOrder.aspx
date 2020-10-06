<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerOrder.aspx.cs" Inherits="Website.CustomerOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Order</title>
    
    <!--Question: do we do Bootstrap thru the cdn way or no?-->
    <!--
    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous"/>
    -->

    <link rel="stylesheet" href="dist/style.css"/>
    <script src="dist/general.js"></script>
    <script src="dist/jquery.js"></script>
    <script src="dist/popper.js"></script>
    <script src="dist/fontawesome.js"></script>
    <script src="dist/bootstrap.js"></script>

</head>
<body style="background-color: #40404F;">
    <form id="form1" runat="server">

        <!--Nav bar-->
            <nav style="background-color: rgba(0, 0, 0, 0.562);" class="navbar navbar-expand-md navbar-dark sticky-top mb-sm-1 mb-md-2 mb-lg-3">
                <div class="container-fluid">
                    <a class="navbar-brand thumbnail" href="#"><img src="images/logo.jpg" style="max-height: 40px"/></a>
                    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarResponsive">
                        <span class="navbar-toggler-icon"></span>
                    </button>
                    <div class="collapse navbar-collapse" id="navbarResponsive">
                        <ul class="navbar-nav ml-auto">
                            <li class="nav-item">
                                <a class="nav-link" href="#">Home</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link active" href="#">My Order</a>
                            </li>
                            <li class="nav-item dropdown">
                                <a class="nav-link dropdown-toggle" href="#" id="navbarDropDown" data-toggle="dropdown">List</a>
                                <div class="dropdown-menu">
                                    <a class="dropdown-item" href="#">Item1</a>
                                    <a class="dropdown-item" href="#">Item2</a>
                                    <a class="dropdown-item" href="#">Item3</a>
                                    <div class="dropdown-divider"></div>
                                    <a class="dropdown-item" href="#">Item4</a>
                                </div>
                            </li>
                            <li class="nav-item">
                                <asp:Label ID="lblLogin" class="nav-link" runat="server" Text="Login" href="#"></asp:Label>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="#">Contact</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>

        <div class="container text-white">
            <!--Search Nav-->
            
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label> <!--label just to check errors-->

            <div class="row mt-md- mb-md-4 mt-sm-2 mb-sm-2">
                <asp:Button ID="Button1" class="btn btn-dark col-2" runat="server" Text="Search"/>
                <asp:TextBox ID="TextBox1" class="form-control col-10" placeholder="Search term, keyword" runat="server"></asp:TextBox>
            </div>
            

            <!--
            panel for deals of the day and such - maybe a carousel? how to: https://www.w3schools.com/bootstrap/bootstrap_carousel.asp
            <div>
                <asp:Panel class="carousel slide" data-ride="carousel" ID="pnlDotd" runat="server"></asp:Panel>
            </div>
            -->

            <div>
                <asp:Panel ID="pnlMaster" runat="server"></asp:Panel>
            </div>

        </div>

        

        <footer style="background-color: rgba(0, 0, 0, 0.562);" class="text-white page-footer font-small pt-md-4 pb-4 mt-4">
            <div class="container-fluid text-center text-sm-left">
                <div class="row">
                    <div class="col">
                        <h4>This footer is a footer</h4>
                        <p>Lorem ipsum dolor sit amet consectetur.</p>
                    </div>
                    <div class="col">
                        <h5>Some links</h5>
                        <ul class="list-unstyled">
                            <li>
                                <a href="#">Footer Link 1</a>
                            </li>
                            <li>
                                <a href="#">Footer Link 2</a>
                            </li>
                        </ul>
                    </div>
                    <div class="col">
                        <h5>Some other links</h5>
                        <ul class="list-unstyled">
                            <li>
                                <a href="#">Footer Link 3</a>
                            </li>
                            <li>
                                <a href="#">Footer Link 4</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
     </footer>

    <div style="background-color: white;" class="footer-copyright text-center py-3">
        Copyright text
        <a href="#">2020</a>
    </div>


    </form>
    <!--More Bootstrap cdn javascript links-->
    <!--
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    -->
</body>
</html>
