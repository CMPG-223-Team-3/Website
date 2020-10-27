<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IsThisYourOrder.aspx.cs" Inherits="Website.IsThisYourOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Might This Be Your Order</title>

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
                <h1 style="margin: auto;">Pending order found with your credentials</h1>
                <h3 style="margin: auto;">Is this your order?</h3>
            </div>
            <div>
                <asp:Panel style="margin: auto;" ID="pnl1" runat="server"></asp:Panel>
                <asp:Panel style="margin: auto;" ID="pnl2" runat="server"></asp:Panel>
            </div>
            <div>
                <asp:Panel style="margin: auto;" class="container m-4" ID="pnlCheckout" runat="server"></asp:Panel>
            </div>
        </div>
    </form>
</body>
</html>
