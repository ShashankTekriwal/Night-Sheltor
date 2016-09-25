<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Verify.aspx.cs" Inherits="NightShelter.Verify" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Night Shelter Domain</title>
    <object classid="CLSID:E730808D-30BE-32FE-B057-0B0EA7F79060"
        height="0" width="0">
    </object>
    <script type="text/javascript" language='javascript'>
        function capture() {
            var err;
            var result = false;

            // Check ID is not NULL
            //if (document.MainForm.UserID.value == '') {
            //    alert('Please enter user id !');
            //    return (false);
            //}

            try // Exception handling
            {
                DEVICE_AUTO_DETECT = 255;

                var objBioBSP = new ActiveXObject('BioBSPCOMM.BioBSP');

                // Open device. [AUTO_DETECT]
                // You must open device before capture.
                objBioBSP.Open(DEVICE_AUTO_DETECT);

                err = objBioBSP.ErrorDescription; // Get error code	
                if (err != " ")		// Device open failed
                {
                    alert('Device open failed !');
                }
                else {
                    // Capture user's fingerprint.
                    objBioBSP.Capture();
                    err = objBioBSP.ErrorDescription; // Get error code

                    if (err != " ")		// Capture failed
                    {
                        alert('Capture failed ! Error Number : [' + err + ']');
                    }
                    else	// Capture success
                    {
                        // Get text encoded FIR data from NBioBSP module.
                        document.MainForm.firData.value = objBioBSP.TextEncodeFIR;
                        alert('Capture success !');
                        result = true;
                    }

                    // Close device. [AUTO_DETECT]
                    objBioBSP.Close(DEVICE_AUTO_DETECT);
                }
                objBioBSP = 0;
            }
            catch (e) {
                alert(e.message);
                return (false);
            }

            if (result) {
                // Submit main form
                document.MainForm.submit();
            }

            return (result);
        }
    </script>
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
<form action="VerifyResult.aspx" name="MainForm" method="post" onsubmit="javascript:return false;">
<input type="hidden" name="firData"/>
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
<input type="button" onclick="capture()" value="Click here to verify with your fingerprint"/>
</p>
</form>
</center>
    <br />
    <br />
    <br />
    <hr />
</body>
</html>
