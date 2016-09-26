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
<form action="VerifyResult.aspx" name="MainForm" method="post" onsubmit="return capture('firData')">
<input type="hidden" id="firData" name="firData"/>
    <p>First Name</p>
    <input type="text" name ="fname" value="demo" />
    <p>Last Name</p>
    <input type="text" name ="lname" value ="demo" />
    <p>Date of birth</p>
    <input type="text" name ="dob" value="1994-12-01" />
    <p>Per Address</p>
    <input type="text" name ="pAddress" value="demo" />
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
</center>
    <br />
    <br />
    <br />
    <hr />
</body>
</html>
