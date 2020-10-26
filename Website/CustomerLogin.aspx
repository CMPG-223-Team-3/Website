<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerLogin.aspx.cs" Inherits="Website.CustomerLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CustomerLogin</title>

    <link rel="stylesheet" href="dist/style.css"/>
    <script src="dist/general.js"></script>
    <script src="dist/jquery.js"></script>
    <script src="dist/popper.js"></script>
    <script src="dist/fontawesome.js"></script>
    <script src="dist/bootstrap.js"></script>

</head>
<body>
    <form id="form1" runat="server">
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
                <div class="col-sm-12 col-md-6"><!--right side-->
                    <fieldset class="field" >
                        <div class="m-b-3 row">
                            <h2><a href="" class="active">Login</a> / <a href="WaiterLogin.aspx" class=""> Waiter</a></h2>
                        </div>
                        <div class="row">
                            <asp:Label ID="lblStatus" runat="server" Text="" CssClass="text"></asp:Label>
                        </div>
                        <div class="row">
                            <asp:Label ID="Label1" CssClass="col-3" runat="server" Text="Name: "></asp:Label>
                            <asp:TextBox AutoCompleteType="FirstName" ID="txtName" runat="server" placeholder="First Name" CssClass="form-control col-9"></asp:TextBox>
                            <div><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtName" ErrorMessage="* This is a required field" CssClass="text"></asp:RequiredFieldValidator></div>
                        </div>
                        <div class="row">
                            <asp:Label ID="Label2" CssClass="col-3" runat="server" Text="Table Number: "></asp:Label>
                            <asp:TextBox ID="txtTable" runat="server" placeholder="Table Number" CssClass="form-control col-9" TextMode="Number"></asp:TextBox>
                            <div><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTable" ErrorMessage="* This is a required field" CssClass="text"></asp:RequiredFieldValidator></div>
                        </div>
                            <div><asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Please input table number larger than 0 and smaller than 101" ControlToValidate="txtTable" MinimumValue="1" MaximumValue="100"></asp:RangeValidator> </div>
                        <div class="row">
                            <asp:Button ID="Button1" runat="server" Text="Sign In" CssClass="btn btn-dark"/>
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
