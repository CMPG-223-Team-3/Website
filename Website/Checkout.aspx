<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="Website.Checkout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Checkout</title>
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
            <div>
                <h1 style="margin: auto;">Checkout</h1>
                <h3 style="margin: auto;">Please confirm your order</h3>
            </div>
            <div>
                <asp:Panel class="container m-4" ID="pnlCheckout" runat="server"></asp:Panel>
            </div>
        </div>
    </form>
</body>
</html>
