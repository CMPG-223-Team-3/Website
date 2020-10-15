<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WaiterLogin.aspx.cs" Inherits="Website.WaiterLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style2 {
            width: 100%;
            height: 192px;
        }
        .auto-style3 {
            text-align: center;
            width: 343px;
        }
        .auto-style5 {
            text-align: right;
            width: 379px;
        }
        .auto-style6 {
            width: 379px;
        }
        .auto-style7 {
            width: 343px;
        }
        .auto-style8 {
            height: 215px;
        }
        .auto-style9 {
            width: 343px;
            text-align: left;
        }
    </style>
</head>
<body style="height: 270px">
    <form id="form1" runat="server" class="auto-style8">
        <table class="auto-style2">
            <tr>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style3">Login Page</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5">User Name: </td>
                <td class="auto-style7">
                    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldNAME" runat="server" ControlToValidate="txtUserName" ErrorMessage="Please enter your User name" ForeColor="Red" Visible="False"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">Password: </td>
                <td class="auto-style7">
                    <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldPASS" runat="server" ControlToValidate="txtPassword" ErrorMessage="Please enter your Password" ForeColor="Red" Visible="False"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style9">
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="User name or Password is incorect" ForeColor="Red" Visible="False"></asp:CustomValidator>
                    <br />
                    <br />
                    <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnRegister" runat="server" OnClick="btnRegister_Click" Text="Register" />
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </form>
</body>
</html>
