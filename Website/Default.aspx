﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Website.Default" %>

<!DOCTYPE html>

<!--html xmlns="http://www.w3.org/1999/xhtml">-->
<head runat="server">
    <title>Landing Page</title>
    <link rel="stylesheet" href="dist/style.css">
    <script src="dist/general.js"></script>
    <script src="dist/jquery.js"></script>
    <script src="dist/popper.js"></script>
    <script src="dist/fontawesome.js"></script>
    <script src="dist/bootstrap.js"></script>
        
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <nav class="navbar navbar-expand-lg navbar-dark bg-primary">
                <a class="navbar-brand" href="#">Company Name</a>

                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul class="navbar-nav ml-auto">
                        <li class="nav-item">
                            <a class="nav-link" href="#">Home</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="#">About</a>
                        </li>
                        <li class="nav-item dropdown">
                            <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" data-toggle="dropdown">Products</a>
                            <div class="dropdown-menu">
                                <a class="dropdown-item" href="#">Product 1</a>
                                <a class="dropdown-item" href="#">Product 2</a>
                                <a class="dropdown-item" href="#">Product 3</a>
                                <div class="dropdown-divider"></div>
                                <a class="dropdown-item" href="#">Product 4</a>
                            </div>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="#">Contact</a>
                        </li>
                    </ul>
                </div>
            </nav>

            <div class="jumbotron">
                <h1 class="display-4">Simple. Elegant. Awesome.</h1>
                <p class="lead">Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>
                <i class="fas fa-clock"></i>
                <p class="lead">
                    <asp:Button class="btn btn-primary btn lg" ID="Button1" runat="server" Text="Learn More" />
                    <!--<a class="btn btn-primary btn lg" href="#" role="button">Learn more</a>-->
                </p>
            </div>
            <div class="row">
                <div class="col-sm-12 col-md-4">
                    <div class="card mb-4">
                        <div class="card-boddy text-centre">
                            <h5 class="card-title">Card title</h5>
                            <p class="card-text">Some quick text to buil up on the card title</p>
                            <a href="#" class="card-link">Another link</a>
                        </div>
                    </div>
                </div>
                <div class="col">
                    <div class="card">
                        <div class="card-boddy text-centre">
                            <h5 class="card-title">Card title</h5>
                            <p class="card-text">Some quick text to buil up on the card title</p>
                            <a href="#" class="card-link">Another link</a>
                        </div>
                    </div>
                </div>
                <div class="col">
                    <div class="col">
                        <div class="card">
                            <div class="card-boddy text-centre">
                                <h5 class="card-title">Card title</h5>
                                <p class="card-text">Some quick text to buil up on the card title</p>
                                <a href="#" class="card-link">Another link</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row mt-sm-4 mt-md-0">
                <div class="col-sm-12 col-md-8">
                    <h3>An important heading</h3>
                    <p class="lead">A sort of important subheading can go here, which is larger and gray.</p>
            
                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.</p>
                    <p>Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.</p>
                </div>
            
                <div class="col-sm-12 col-md-4">
                    <h3 class="mb-4">Secondary Menu</h3>
            
                    <ul class="nav flex-column nav-pills">
                        <li class="nav-item">
                            <a class="nav-link active" href="#">Active</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="#">Link</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="#">Link</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link disabled" href="#">Disabled</a>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </form>
    </body>
</!--html>