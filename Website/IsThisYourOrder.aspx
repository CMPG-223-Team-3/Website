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
        <div>
            <asp:Panel ID="pnl1" runat="server"></asp:Panel>
            <asp:Panel ID="pnl2" runat="server"></asp:Panel>
        </div>
    </form>
</body>
</html>
