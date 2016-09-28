<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Regist.aspx.cs" Inherits="NightShelter.Regist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <script type="text/javascript">
        function showForm() {
            document.getElementById("mainDiv").style.visibility = "visible";
        }
    </script>
    <div id="mainDiv" runat="server" >
        <form id="registForm" runat="server">
            <asp:ScriptManager ID="ScriptManger1" runat="Server" EnablePageMethods="true"></asp:ScriptManager>
            <div>
                <asp:TextBox ID="f_firstName" runat="server"></asp:TextBox>
                <asp:TextBox ID="f_lastName" runat="server"></asp:TextBox>
                <asp:TextBox ID="f_dob" runat="server"></asp:TextBox>
                <asp:TextBox ID="f_paddress" runat="server"></asp:TextBox>
                <asp:Button ID="f_regist" runat="server" value="register client" OnClientClick="return validate();" OnClick="getFormDetails" />
            </div>
        </form>
    </div>
     <script type="text/javascript">
        //document.getElementById("mainDiv").style.visibility = "hidden";
        function validate() {
            return true;
        }
    </script>
</body>
</html>
