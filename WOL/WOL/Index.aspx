<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WOL.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

     <style type="text/css">
        .ListBoxCssClass
        {
            color:GhostWhite;
            background-color:DarkOliveGreen;
            font-family:Courier New;
            font-size:large;
            font-style:italic;
            }
         #TextArea1 {
             height: 75px;
             width: 864px;
         }
    </style>


</head>
<body>
    <form id="form1" runat="server">
        <div>
             <div>
            <asp:Label ID="lblTitle" runat="server" Text="Wol Clients"></asp:Label>
                  </div>
            <asp:ListBox ID="lbWolClients" runat="server" Width="435px" CssClass="ListBoxCssClass" Height="272px" OnSelectedIndexChanged="lbWolClients_SelectedIndexChanged" ></asp:ListBox>
             <asp:Panel ID="Panel1" runat="server" Height="49px">
                 <asp:Button ID="BtnWakeUp" runat="server" Text="Wakeup" OnClick="BtnWakeUp_Click" />
             </asp:Panel>
        </div>
        <asp:Panel ID="Panel2" runat="server" Height="89px">
            <asp:TextBox ID="TBLog" runat="server" Height="80px" ReadOnly="True" TextMode="MultiLine" Width="1114px"></asp:TextBox>
        </asp:Panel>
    </form>
</body>
</html>
