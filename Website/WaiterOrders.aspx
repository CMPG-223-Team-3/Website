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
            width: 321px;
        }
        .auto-style4 {
            width: 321px;
            height: 151px;
        }
        .auto-style7 {
            width: 321px;
            text-align: left;
        }
        .auto-style13 {
            text-align: left;
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
            </tr>
            <tr>
                <td class="auto-style4">
                    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" Width="1198px">
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Total :&nbsp;
                    <asp:Label ID="lblTotal1" runat="server" Text="."></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style7">
                    &nbsp;<asp:Button ID="UpdateOrder1" runat="server" OnClick="Button1_Click" Text="Update Order" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnDelivered1" runat="server" OnClick="btnDelivered1_Click" Text="Delivered" />
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <br />
                    <br />
                    <asp:Button ID="btnLogout" runat="server" OnClick="btnLogout_Click" Text="Logout" />
                </td>
            </tr>
        </table>
        <div>
        </div>
    </form>
    <p class="auto-style13">
        &nbsp;</p>
</body>
</html>
