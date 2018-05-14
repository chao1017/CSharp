<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Demo.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewpoint" content="width=device-width, initial-scale=1" />
    <link rel="Stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <title>天下支付Demo</title>
    
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-group" style="margin-top:20%;margin-left:10%;">
            <asp:Label runat="server" ID="lblCellNo" Text="手機號碼:"></asp:Label>&nbsp
            <asp:TextBox runat="server" ID="txtboxCellNo"></asp:TextBox>
        </div>
        <div class="form-group" style="margin-left:10%;">
            <asp:Label runat="server" ID="lblAmount" Text="匯款金額:"></asp:Label>&nbsp
            <asp:TextBox runat="server" ID="txtboxAmount"></asp:TextBox>
        </div>
        <div style="margin-left:10%;">
            <asp:Button runat="server" ID="btnSend" Text="送出" OnClientClick="Click();" />
        </div>
    </form>

    <script type="text/javascript">
        function Click()
        {
            var keys = [];
            var values = [];

            var cell_no = document.getElementById("txtboxCellNo").value;
            var amount = document.getElementById("txtboxAmount").value;
            
            keys[0] = "cellno";
            keys[1] = "amount";
            values[0] = cell_no;
            values[1] = amount;

            openWindowWithPost("demo_page.aspx", "web", keys, values);
        }

        function openWindowWithPost(url, name, keys, values) {
    var newWindow = window.open(url, name);
    if (!newWindow) return false;
    var html = "";
    html += "<html><head></head><body><form id='formid' method='post' action='" + url + "'>";
    if (keys && values && (keys.length == values.length))
        for (var i = 0; i < keys.length; i++)
        html += "<input type='hidden' name='" + keys[i] + "' value='" + values[i] + "'/>";
    
    html += "</form><script type='text/javascript'>document.getElementById(\"formid\").submit()<" + "/script></body></html>";
    newWindow.document.write(html);
    return newWindow;
}
    </script>
</body>
</html>
