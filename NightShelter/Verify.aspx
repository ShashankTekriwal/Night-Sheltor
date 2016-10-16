<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Verify.aspx.cs" Inherits="NightShelter.Verify" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Night Shelter Domain</title>
    <object classid="CLSID:E730808D-30BE-32FE-B057-0B0EA7F79060"
        height="0" width="0">
    </object>
    <script type="text/javascript" src="scripts/capture.js"></script>
    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="bootstrap/js/bootstrap.min.js"></script>
</head>

<body>

    <div class="container">

        <form class="well form-horizontal" action="Regist.aspx " method="post" name="MainForm" id="MainForm" onsubmit="return capture('firData');">
            <fieldset>

                <!-- Form Name -->
                <legend>Night Shelter Registration!</legend>

                <input type="hidden" id="firData" name="firData" />

                <div class="form-group">
                    <label class="col-md-4 control-label">Gender</label>
                    <div class="col-md-4">
                        <div class="radio">
                            <label>
                                <input type="radio" name="gender" value="1" required />
                                Male
                            </label>
                        </div>
                        <div class="radio">
                            <label>
                                <input type="radio" name="gender" value="2" />
                                Female
                            </label>
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label">Location</label>
                    <div class="col-md-4 selectContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-list"></i></span>
                            <select name="location" class="form-control selectpicker" required>
                                <option value="">Please select your location</option>
                                <option value="Loc1">Loc1</option>
                                <option value="Loc2">Loc2</option>
                            </select>
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label">Finger ID</label>
                    <div class="col-md-4 selectContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-list"></i></span>
                            <select name="fingerID" class="form-control selectpicker" required>
                                <option value="">Please Select Finger to be enrolled</option>
                                <option value="1">Right Hand Thumb</option>
                                <option value="2">Right Hand Index Finger</option>
                                <option value="3">Right Hand Middle Finger</option>
                                <option value="4">Right Hand Ring Finger</option>
                                <option value="5">Right Hand Baby Finger</option>
                                <option value="6">Left Hand Thumb</option>
                                <option value="7">Left Hand Index Finger</option>
                                <option value="8">Left Hand Middle Finger</option>
                                <option value="9">Left Hand Ring Finger</option>
                                <option value="10">Left Hand Baby Finger</option>
                            </select>
                        </div>
                    </div>
                </div>



                <!-- Button -->
                <div class="form-group">
                    <label class="col-md-4 control-label"></label>
                    <div class="col-md-4">
                        <button type="submit" class="btn btn-warning">Click here to capture your fingerprint. <span class="glyphicon glyphicon-send"></span></button>
                    </div>
                </div>

            </fieldset>
        </form>
    </div>

    <!-- Modal 1-->
    <div class="modal fade" id="Modal1" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title lead bg-success"><span class="glyphicon glyphicon-ok">Result</span></h4>
                </div>
                <div class="modal-body">
                    <p class="bg-success">Registration Successful!</p>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal 2-->
    <div class="modal fade" id="Modal2" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title lead bg-danger"><span class="glyphicon glyphicon-remove">Result</span></h4>
                </div>
                <div class="modal-body">
                    <p class="bg-danger">Registration Not Successful!</p>
                </div>
            </div>
        </div>
    </div>

    <div id="err" class="container alert-info">
        <h5><%= error %></h5>
    </div>

    <script type="text/javascript">
        $(document).ready(function(){
            var message = "<%= message %>";
            var err = "<%= error %>";

            if(message === "1"){
                $('#Modal1').modal();
            }
            else if (message === "0") {
                $("#Modal2").modal();
            }

            if (err || err.length != 0) {
                $('#err').show();
            }
            else {
                $('#err').hide();
            }
        });
    </script>

</body>

</html>
