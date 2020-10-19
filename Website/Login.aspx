<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Website.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
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
            <div class="row" style="padding: 100px;">
                <div class="col d-flex justify-content-center">
                    <div class="card">
                        <div class="card-boddy text-centre">
                            <h5 class="card-title">Login</h5>
                            <p class="card-text">
                                <table style="width:100%;padding: 40px;border-spacing: 20px;">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblUsername" runat="server" Text="Username :"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Username"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPassword" runat="server" Text="Password :"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" type="password" placeholder="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td><asp:Button ID="Button1" runat="server" Text="Submit" CssClass="btn-block btn-primary pull-right" OnClick="Button1_Click" /></td>
                                    </tr>
                                </table>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
