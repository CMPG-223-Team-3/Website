<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerOrder.aspx.cs" Inherits="Website.CustomerOrder" ErrorPage="Error.aspx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Order</title>

    <link rel="stylesheet" href="dist/style.css" />
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
                            <a class="nav-link" href="Default.aspx">Home</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link active" href="CustomerOrder.aspx">Order</a>
                        </li>
                        <li class="nav-item">
                            <a><asp:Label ID="lblLogin" runat="server" Text="Login" href="CustomerLogin.aspx"></asp:Label></a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="Checkout.aspx">My Order</a>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>

        <div class="container">


            <!--Search Nav-->
            <div class="row mt-md- mb-md-4 mt-sm-2 mb-sm-2">
                <asp:Button ID="btnSearch" class="btn btn-dark col-2" runat="server" Text="Search" OnClick="btnSearch_Click" />
                <asp:TextBox ID="txtSearch" class="form-control col-10" placeholder="Search term, keyword" runat="server"></asp:TextBox>
            </div>


            <!--
            panel for deals of the day and such - maybe a carousel? how to: https://www.w3schools.com/bootstrap/bootstrap_carousel.asp
            <div>
                <asp:Panel class="carousel slide" data-ride="carousel" ID="pnlDotd" runat="server"></asp:Panel>
            </div>
            -->

            <div class="container">
                <div class="row">
                    <div class="col-lg-6 FormBox">
                        <div>
                            <label class="control-label mb-3" for="pnlOrder">
                                <h2>Our Menu:</h2>
                            </label>
                        </div>
                        <asp:Panel ID="pnlMaster" runat="server"></asp:Panel>
                    </div>

                    <div class="col-lg-6 FormBox">
                        <div>
                            <label class="control-label mb-3" for="pnlOrder">
                                <h2>Order Items:</h2>
                            </label>
                        </div>
                        <div>
                            <!--<asp:Label ID="orderlbl" runat="server" Text="Click a `add to cart` button" Visible="false" class="control-label mb-2" for="pnlOrder"></asp:Label>-->
                            <asp:ScriptManager ID="ScriptManager1" runat="server" />
                            <asp:UpdateProgress runat="server" ID="pageupdateprogress">
                                <ProgressTemplate>
                                    Loading...
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <asp:UpdatePanel ID="updatePanel" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlOrder" runat="server"></asp:Panel>
                                    <!--<asp:Button CssClass="btn btn-dark btn-lg" ID="checkoutBtn" runat="server" Text="Checkout" Visible="false" CausesValidation="false"/>-->
                                </ContentTemplate>

                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div>
            <asp:Button ID="forcePostbackBtn" runat="server" Text="Button" Visible="false" OnClick="forcePostbackBtn_Click" />
        </div>

        <!--<footer class="page-footer font-small pt-md-4 pb-4 mt-4">
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
     </footer>-->

        <div class="footer-copyright text-center py-3">
            Copyright text
        <a href="#">2020</a>
        </div>


    </form>
</body>
</html>
