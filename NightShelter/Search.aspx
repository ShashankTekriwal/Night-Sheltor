<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="NightShelter.Search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search</title>
    <script type="text/javascript" src="scripts/capture.js"></script>
    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="bootstrap/js/bootstrap.min.js"></script>
    <style>
        .table-borderless tbody tr td, .table-borderless tbody tr th, .table-borderless thead tr th {
            border: none;
        }
    </style>
</head>
<body>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#contentDiv').hide();
        });
        function clearForm() {
            $(document).ready(function () {
                $('#resultDiv').html("");
                $("input[name='f_gender']").removeAttr('checked');
                $("#f_fingerID")[0].selectedIndex = 0;
                $('#contentDiv').hide();
            });
        }
        var details;
        function showDetails(det) {
            //alert("Heloo");
            var obj = JSON.parse(det);
            details = obj;
            //alert(obj.dob);
            $(document).ready(function () {
                $('#contentDiv').show()
                $('#t_firstName').html(obj.firstName);
                $('#t_lastName').html(obj.lastName);
                $('#t_gender').html(obj.gender);
                $('#t_dob').html(obj.dob);
                $('#t_paddress').html(obj.paddress);
                $('#t_fingerID').html(obj.fingerID);

                var innerText = "";
                for (var key in obj.places) {
                    if (obj.places.hasOwnProperty(key)) {
                        innerText = innerText + "<tr><td>" + key + "</td><td>" + obj.places[key]+ "</td><tr>";
                    }
                }
                $('#dynamicTable').html(innerText);
            });
        }
    </script>

    <div class="container" runat="server">

        <form class="well form-horizontal" name="MainForm" id="MainForm" runat="server">
            <asp:ScriptManager ID="ScriptManger1" runat="Server" EnablePageMethods="true"></asp:ScriptManager>
            <fieldset>
                <legend>Night Shelter Search Utility</legend>


                <div class="form-group">
                    <label class="col-md-4 control-label">Gender</label>
                    <div class="col-md-4">
                        <div class="radio">
                            <label>
                                <asp:RadioButtonList ID="f_gender" class="radiobuttonlist" name="gender" runat="server">
                                    <asp:ListItem class="radiobuttonlist" Text="Male" Value="1" />
                                    <asp:ListItem class="radiobuttonlist" Text="Female" Value="2" />
                                </asp:RadioButtonList>

                            </label>
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-4 control-label">Finger ID</label>
                    <div class="col-md-4 selectContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-list"></i></span>

                            <asp:DropDownList runat="server" ID="f_fingerID" name="fingerID" class="form-control selectpicker">
                                <asp:ListItem id="defItem" Value="" Text="Please Select Finger to be searched" />
                                <asp:ListItem Value="1" Text="Right Hand Thumb" />
                                <asp:ListItem Value="2" Text="Right Hand Index Finger" />
                                <asp:ListItem Value="3" Text="Right Hand Middle Finger" />
                                <asp:ListItem Value="4" Text="Right Hand Ring Finger" />
                                <asp:ListItem Value="5" Text="Right Hand Baby Finger" />
                                <asp:ListItem Value="6" Text="Left Hand Thumb" />
                                <asp:ListItem Value="7" Text="Left Hand Index Finger" />
                                <asp:ListItem Value="8" Text="Left Hand Middle Finger" />
                                <asp:ListItem Value="9" Text="Left Hand Ring Finger" />
                                <asp:ListItem Value="10" Text="Left Hand Baby Finger" />
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <asp:HiddenField ID="f_firData" runat="server" />
                <div class="form-group">
                    <label class="col-md-4 control-label"></label>
                    <div class="col-md-4">
                        <asp:Button ID="f_capture" Text="Search FingerPrint" runat="server" OnClientClick="return capture('f_firData');" OnClick="search" class="btn btn-warning"></asp:Button>
                        <button value="Clear" class="btn btn-default" onclick="clearForm();">Clear</button>
                    </div>
                </div>
            </fieldset>
        </form>
    </div>

    <div class="container">
        <div runat="server">
            <p id="resultDiv" runat="server"></p>
        </div>
        <div id="contentDiv" runat="server">
            <table id="fixedTable" class="table table-borderless">
                <tr>
                    <th>First Name</th>
                    <td id="t_firstName"></td>
                    <th>Last Name</th>
                    <td id="t_lastName"></td>
                </tr
                <tr>
                    <th>Gender</th>
                    <td id="t_gender"></td>
                    <th>Date of Birth</th>
                    <td id="t_dob"></td>
                </tr>
                <tr>
                    <th>Permanent Address</th>
                    <td id="t_paddress"></td>
                    <th>Finger Detail</th>
                    <td id="t_fingerID"></td>
                </tr>
            </table>
            <table class="table">
                <tr class="active">
                    <th>Date and Time</th>
                    <th>Night Shelter Location</th>
                </tr>
                <tbody id="dynamicTable" style="overflow-y:auto" ></tbody>
            </table>
        </div>
    </div>

</body>
</html>
