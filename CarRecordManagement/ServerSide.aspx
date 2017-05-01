<%@ Page Title="" Language="C#" MasterPageFile="~/Header.Master" AutoEventWireup="true" CodeBehind="ServerSide.aspx.cs" Inherits="sunovaSortingFiltering.ServerSide" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="MasterPagePlaceHolderHead" runat="server">
    <!-- 
    Programmer  : KalaiPriya
    Date        : 03/03/2017
    Description : This page is to demonstrate the Server Side(Full Psot Back) Sorting & Filtering using C# 
-->
    <title>Server Side</title>
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="MasterPagePlaceHolderBody" runat="server">
    <div class="container body_ServerSide_Div">
        <!--Start of Panel-->
        <div class="panel panel-default">
            <!--Start of Panel header-->
            <div class="panel-heading">
                <div class="row">
                    <div class="col-xs-3 col-md-3 col-sm-3">
                        <!--Filter Button-->
                        <a href="#" data-toggle="modal" data-target="#myModal" id="ModalTirggered">
                            <i class="fa fa-filter SS_FilterIconFont" aria-hidden="true"></i>
                        </a>
                    </div>
                    <div class="col-xs-6 col-md-6 col-sm-6 SS_Table_Header">
                        <!--Table Header Details-->
                        <h4>Car Details</h4>
                    </div>
                    <div class="col-xs-3 col-md-3 col-sm-3">
                        <!--Reset Button-->
                        <div class="SS_Table_Reset">
                            <asp:Button ID="Button" runat="server" Text="Reset Records" class="btn btn-default" type="submit" OnClick="LoadGridView" />
                        </div>
                    </div>
                </div>
            </div>
            <!--End of Panel header-->

            <!--Start of Panel Body-->
            <div class="panel-body">
                <asp:GridView ID="serverSide_GridView" runat="server" class="table table-striped" AllowSorting="True" OnSorting="tableSorting">
                </asp:GridView>
            </div>
            <!--End of Panel Body-->
        </div>
        <!--End of Panel-->

        <!-- Modal -->
        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-body">
                        Please select the Column to be Filtered:
                            <div class="dropdown">
                                <!-- Dropdown in modal-->
                                <asp:DropDownList ID="filter_DropDown" runat="server" InitialValue="Unselectable Item" class="btn btn-default dropdown-toggle">
                                    <asp:ListItem Text="Select" Value="0" disabled="disabled" />
                                    <asp:ListItem Text="mileage" Value="5" />
                                    <asp:ListItem Text="name" Value="1" />
                                    <asp:ListItem Text="color" Value="2" />
                                    <asp:ListItem Text="engine" Value="3" />
                                    <asp:ListItem Text="model" Value="4" />
                                </asp:DropDownList>
                            </div>
                        <p id="validation_Dropdown">Please select a value</p>
                        <br />

                        Please Enter Column Value To Be Filtered:
                        <div class="form-group">
                            <!-- Filter value in modal-->
                            <asp:TextBox ID="filterValue_TextBox" class="form-control" runat="server" Width="150px"></asp:TextBox>
                            <p id="validation_Textbox">Please Enter a value</p>
                        </div>
                        <!-- !!!!Highly Important!!!! -->
                        <!--Trigger the Csharp function-->
                        <asp:Button ID="filter" runat="server" Text="Button" OnClick="filter_Click" style="display:inline" />
                        <!-- !!!!Highly Important!!!! -->
                        <button type="button" id="filterButton" class="btn btn-default" width="85px">Submit</button>
                    </div>
                    <div class="modal-footer">
                        <!--Modal close-->
                        <button type="button" id="modalCloseButton" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {

            //This Method is used for Client Side validation
            $("#filterButton").click(function () {

                var filter_DropDown_Value_JS = document.getElementById('MasterPagePlaceHolderBody_filter_DropDown').value;
                var filterValue_TextBox_JS = document.getElementById('MasterPagePlaceHolderBody_filterValue_TextBox').value;
                var dropdown;
                var textbox;

                if (filter_DropDown_Value_JS == 0) {
                    $("#validation_Dropdown").css("display", "inline");
                    $("#validation_Dropdown").css("color", "red");
                }
                else {
                    $("#validation_Dropdown").css("display", "none");
                    dropdown = true;
                }

                if (filterValue_TextBox_JS == null || filterValue_TextBox_JS == '') {
                    $("#validation_Textbox").css("display", "inline");
                    $("#validation_Textbox").css("color", "red");
                }
                else {
                    $("#validation_Textbox").css("display", "none");
                    textbox = true;
                }

                if (dropdown == true && textbox == true) {
                    triggering_CsharpFunction();
                }
            });

            //This method is used to trigger the CSharp function, when it is being called from Modal
            function triggering_CsharpFunction() {
                $("input[id$='filter'").click();
            }

            //Remove the validation in modal
            $("#ModalTirggered").click(function () {
                $("#validation_Textbox").css("display", "none");
                $("#validation_Dropdown").css("display", "none");
            });

            //This method is used to close the modal forcefully , whne the close button is clicked
            $("#modalCloseButton").click(function () {
                $('#myModal').modal('hide');
            });

        });
    </script>
</asp:Content>


