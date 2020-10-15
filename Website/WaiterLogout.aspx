<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WaiterLogout.aspx.cs" Inherits="Website.WaiterLogout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="auto-style1">
                <tr>
                    <td class="auto-style2">Lout to end working shift</td>
                </tr>
                <tr>
                    <td class="auto-style2">Enter shift starting time:&nbsp;
                        <asp:TextBox ID="txtBeginS" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Enter shift ending time:&nbsp;
                        <asp:TextBox ID="txtEndS" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Button ID="btnFinalLogout" runat="server" OnClick="btnFinalLogout_Click" Text="Logout" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
