<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Regist.aspx.cs" Inherits="NightShelter.Regist" %>

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
</html>--%>


<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Regist.aspx.cs" Inherits="NightShelter.Regist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Night Shelter Domain</title>
    <script type="text/javascript" src="scripts/capture.js"></script>

    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css" />
    <script type="text/javascript" src="bootstrap/js/bootstrap.min.js"></script>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <script type="text/javascript" src="http://ajax.aspnetcdn.com/ajax/jquery.validate/1.15.0/jquery.validate.min.js"></script> 
    <script type="text/javascript" src="http://ajax.aspnetcdn.com/ajax/jquery.validate/1.15.0/additional-methods.min.js"></script>

    <script type="text/javascript">
        function showForm() {
            document.getElementById("mainDiv").style.visibility = "visible";
        }
    </script>
</head>
<body>

    <div id="mainDiv" runat="server" class="container">

        <form id="registForm" autocomplete="off" class="well form-horizontal" runat="server">
            <asp:ScriptManager ID="ScriptManger1" runat="Server" EnablePageMethods="true"></asp:ScriptManager>

            <fieldset>

                <!-- Form Name -->
                <legend>New User Registration Details</legend>

                <!-- Text input-->

                <div class="form-group">
                    <label class="col-md-4 control-label">First Name</label>
                    <div class="col-md-4 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <asp:TextBox ID="f_firstName" class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label">Last Name</label>
                    <div class="col-md-4 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <asp:TextBox ID="f_lastName" class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label">Date of Birth</label>
                    <div class="col-md-4 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                            <asp:TextBox type="text" ID="f_dob" class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label">Permanent Address</label>
                    <div class="col-md-4 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <asp:TextBox ID="f_paddress" class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label"></label>
                    <div class="col-md-4">
                        <asp:Button ID="f_regist" Text="Register User" runat="server" OnClientClick="return validate();" OnClick="getFormDetails" class="btn btn-warning"></asp:Button>
                    </div>
                </div>
            </fieldset>
        </form>
    </div>
    <script type="text/javascript">
        $("#f_dob").datepicker({
            dateFormat: "yy-mm-dd",
            changeMonth: true,
            changeYear: true,
            yearRange: "1950:2010"
        });
        function validate() {
            var flag = true;

            return flag;
        }
        $(document).ready(function () {
            $.validator.addMethod("address", function (value, element) {
                return this.optional(element) || /^[a-z0-9/\-., "\s]+$/i.test(value);
            }, "Only Basic Punctuation -,./ allowed.");
            $('#registForm').validate({
                rules: {
                    f_firstName: {
                        required: true,
                        minlength: 2,
                        lettersonly: true
                    },
                    f_lastName: {
                        minlength: true,
                        lettersonly: true
                    },
                    f_paddress: {
                        required: true,
                        address:true
                    }
                }
            });
        });
    </script>
</body>
</html>
