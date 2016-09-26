function capture(ele) {
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
                
                document.getElementById(ele).value = objBioBSP.TextEncodeFIR;
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
        //document.MainForm.submit();
    }

    return (result);
}