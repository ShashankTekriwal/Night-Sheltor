<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Verify.aspx.cs" Inherits="NightShelter.Verify" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Night Shelter Domain</title>
    <object classid="CLSID:E730808D-30BE-32FE-B057-0B0EA7F79060"
        height="0" width="0">
    </object>
    <script type="text/javascript" src="scripts/capture.js"></script>
</head>
<body>
    <br />
    <br />
    <center>
<font size="5"><b>Fingerprint Verification</b></font>
<hr/>
<p>
<font color="#800000"><b>Verification</b></font>
</p>
<br/>
<br/>
<br/>
<form action="Regist.aspx" name="MainForm" method="post" onsubmit="return capture('firData');">
    <input type="hidden" id="firData" name="firData"/>
    <p>Gender</p>
    <input type="text" name ="gender" value ="1" />
    <p>Location</p>
    <input type="text" name ="location" value="demo" />
    <p>Finder ID</p>
    <input type="text" name ="fingerID" value="1" />

<p>
<input type="submit" value="Click here to verify with your fingerprint"/>
</p>
</form>

<div id ="resDiv" runat="server">
    <%= message %>
</div>
<div id="errDiv" runat="server">
    <%= error %>
</div>
</center>
    <br />
    <br />
    <br />
    <hr />
</body>
</html>
