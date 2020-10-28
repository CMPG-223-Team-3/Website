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
                <h1 class="align-middle mt-3 mb-5">Welcome!</h1>
                <h3 class="align-middle">We found a pending order with your credentials</h3>
                <h5>Is this your order? Would you like to continue with it? Click Yes</h5>
            </div>
            <div class="bordered mt-5">
                <div>
                    <asp:Panel class="FormBox" ID="orderpanel" runat="server"></asp:Panel>
                    <asp:Panel class="mt-3" ID="yesnopanel" runat="server"></asp:Panel>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
