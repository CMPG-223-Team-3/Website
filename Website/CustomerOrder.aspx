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
<body>
    <form id="form1" runat="server">
        <div class="container">
            <!--Nav bar-->
            <nav class="navbar navbar-expand-md navbar-dark bg-dark sticky-top">
                <div class="container-fluid">
                    <a class="navbar-brand" href="#"><img src="Images/logo.jpg" style="max-height: 40px"/></a>
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

            <!--Search Nav-->
            

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

        

        <div>
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
