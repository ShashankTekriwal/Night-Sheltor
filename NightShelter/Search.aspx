<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="NightShelter.Search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search</title>
    <script type="text/javascript" src="scripts/capture.js"></script>
</head>
<body>

    <div runat="server">
        <script type="text/javascript">
            var details;
            function showDetails(det) {
                console.log("hello");
                details = det;
                console.log(details);
            }
        </script>
        <form name="MainForm" runat="server">
            <asp:ScriptManager ID="ScriptManger1" runat="Server" EnablePageMethods="true"></asp:ScriptManager>
            Gender :
            <asp:TextBox ID="f_gender" runat="server"></asp:TextBox>
            FingerID :
            <asp:TextBox ID="f_fingerID" runat="server"></asp:TextBox>
            <asp:HiddenField ID="f_firData" runat="server" />
            <asp:Button ID="f_capture" runat="server" value="Capture Finger Print" OnClientClick="return capture('f_firData');" OnClick="search" />
        </form>
    </div>

    <div id="resultDiv" runat="server">
    </div>

    <div id="contentDiv" runat="server">
    </div>
</body>
</html>
