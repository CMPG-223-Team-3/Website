<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WaiterOrders.aspx.cs" Inherits="Website.WaiterOrders" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 361px;
        }
        .auto-style3 {
            width: 343px;
        }
        .auto-style4 {
            width: 361px;
            height: 151px;
        }
        .auto-style5 {
            width: 343px;
            height: 151px;
        }
        .auto-style6 {
            height: 151px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">Table:&nbsp;
                    <asp:Label ID="lblT1" runat="server" Text="."></asp:Label>
                </td>
                <td class="auto-style3">Table:&nbsp;
                    <asp:Label ID="lblT2" runat="server" Text="."></asp:Label>
                </td>
                <td>Table:&nbsp;
                    <asp:Label ID="lblT3" runat="server" Text="."></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">
                    <asp:GridView ID="GridView1" runat="server">
                    </asp:GridView>
                </td>
                <td class="auto-style5">
                    <asp:GridView ID="GridView2" runat="server">
                    </asp:GridView>
                </td>
                <td class="auto-style6">
                    <asp:GridView ID="GridView3" runat="server">
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Total :&nbsp;
                    <asp:Label ID="lblTotal1" runat="server" Text="."></asp:Label>
                </td>
                <td class="auto-style3">Total :&nbsp;
                    <asp:Label ID="lblTotal2" runat="server" Text="."></asp:Label>
                </td>
                <td>Total :&nbsp;
                    <asp:Label ID="lblTotal3" runat="server" Text="."></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Button ID="btnDelivered1" runat="server" OnClick="btnDelivered1_Click" Text="Delivered" />
                </td>
                <td class="auto-style3">
                    <asp:Button ID="btnDelivered2" runat="server" OnClick="btnDelivered2_Click" Text="Delivered" />
                </td>
                <td>
                    <asp:Button ID="btnDelivered3" runat="server" OnClick="btnDelivered3_Click" Text="Delivered" />
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <br />
                    <br />
                    <asp:Button ID="btnLogout" runat="server" OnClick="btnLogout_Click" Text="Logout" />
                </td>
                <td class="auto-style3">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <div>
        </div>
    </form>
</body>
</html>
