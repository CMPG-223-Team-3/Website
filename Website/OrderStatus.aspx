<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderStatus.aspx.cs" Inherits="Website.OrderStatus" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Your Order Status</title>
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
                <h1 class="align-middle mt-3">Welcome!</h1>
                <h3 class="align-middle">You can see your order status here! For any enquiries, please call a waiter to assist you</h3>
            </div>
            <div class="bordered mt-5">
                <div>
                    <label class="control-label" for="pnl1">Order Items:</label>
                    <asp:Panel class="container align-self-center FormBox" ID="pnl1" runat="server"></asp:Panel>
                </div>
                <div>
                    <label class="control-label mt-2" for="pnlstatus">Status:</label>
                    <asp:Panel class="container align-self-center FormBox" ID="pnlstatus" runat="server"></asp:Panel>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
