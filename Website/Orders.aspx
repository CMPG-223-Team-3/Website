<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="Login_Main.Orders" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            height: 342px;
        }
        .auto-style2 {
            width: 445px;
            font-size: xx-large;
            background-color: #FFFF00;
        }
        .auto-style3 {
            width: 339px;
            background-color: #00FF00;
        }
        .auto-style4 {
            width: 445px;
            height: 286px;
            background-color: #FFFF00;
        }
        .auto-style5 {
            width: 339px;
            height: 286px;
            background-color: #00FF00;
        }
        .auto-style6 {
            height: 286px;
            width: 431px;
            background-color: #FFFF00;
        }
        .auto-style8 {
            width: 339px;
            height: 112px;
            background-color: #00FF00;
        }
        .auto-style9 {
            height: 112px;
            width: 431px;
            background-color: #FFFF00;
            text-align: left;
        }
        .auto-style10 {
            width: 431px;
            background-color: #FFFF00;
        }
        .auto-style11 {
            font-size: xx-large;
        }
        .auto-style12 {
            font-size: large;
        }
        .auto-style13 {
            height: 575px;
        }
        .auto-style14 {
            width: 445px;
            font-size: xx-large;
            background-color: #FFFF00;
            height: 112px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="auto-style13">
            <table class="auto-style1">
                <tr>
                    <td class="auto-style2"><strong><span class="auto-style11">Table </span>
                        <asp:Label ID="Label1" runat="server" CssClass="auto-style11" Text="."></asp:Label>
                        </strong></td>
                    <td class="auto-style3"><strong><span class="auto-style11">Table </span>
                        <asp:Label ID="Label2" runat="server" CssClass="auto-style11" Text="."></asp:Label>
                        </strong></td>
                    <td class="auto-style10"><strong><span class="auto-style11">Table </span>
                        <asp:Label ID="Label3" runat="server" CssClass="auto-style11" Text="."></asp:Label>
                        </strong></td>
                </tr>
                <tr>
                    <td class="auto-style4">
                        <asp:GridView ID="GridView1" runat="server" Height="235px" Width="431px" DataSourceID="SqlDataSource1">
                        </asp:GridView>
                    </td>
                    <td class="auto-style5">
                        <asp:GridView ID="GridView2" runat="server" Height="235px" Width="431px">
                        </asp:GridView>
                    </td>
                    <td class="auto-style6">
                        <asp:GridView ID="GridView3" runat="server" Height="235px" Width="431px">
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style14"><span class="auto-style12"><strong>
                        <br />
                        Total</strong></span><strong><span class="auto-style12"> Change: </span>
                        <asp:Label ID="Label4" runat="server" CssClass="auto-style12" Text="."></asp:Label>
                        <span class="auto-style11">
                        <br />
&nbsp;<br />
                        </span>
                        <asp:Button ID="Button1" runat="server" Text="Delivered" OnClick="Button1_Click" />
                    </td>
                     
                    <td class="auto-style8">
                        <strong><span class="auto-style12">Total Change:
                        </span>
                        <asp:Label ID="Label5" runat="server" Text="." CssClass="auto-style12"></asp:Label>
                        <br />
                        <br />
                        </strong>
                        <br />
                        <asp:Button ID="Button2" runat="server" Text="Delivered" OnClick="Button2_Click" />
                    </td>
                    <td class="auto-style9">
                        <strong><span class="auto-style12">Total Chamge:
                        </span>
                        <asp:Label ID="Label6" runat="server" Text="." CssClass="auto-style12"></asp:Label>
                        <br />
                        <br />
                        </strong>
                        <br />
                        <asp:Button ID="Button3" runat="server" Text="Delivered" OnClick="Button3_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style14">&nbsp;</td>
                     
                    <td class="auto-style8">
                        &nbsp;</td>
                    <td class="auto-style9">
                        <asp:Button ID="btnLogOut" runat="server" OnClick="btnLogOut_Click" Text="Logout" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
