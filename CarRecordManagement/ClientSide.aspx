<%@ Page Title="" Language="C#" MasterPageFile="~/Header.Master" AutoEventWireup="true" CodeBehind="ClientSide.aspx.cs" Inherits="sunovaSortingFiltering.ClientSide" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="MasterPagePlaceHolderHead" runat="server">
    <!--
    Programmer  : KalaiPriya
    Date        : 03/03/2017
    Description : This page is to demonstrate the Client Side Sorting using Javascript
-->
    <title>Client Side</title>
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="MasterPagePlaceHolderBody" runat="server">
    <div class="container CS_Body_Div" ng-controller="carsController" ng-app="app">
        <!-- Panel Starts-->
        <div class="panel panel-default">
            <!--Panel Header-->
            <div class="panel-heading">
                <div class="row">
                    <div class="col-xs-3 col-md-3 col-sm-3">
                        <select class="form-control" id="column" ng-selected="mileage" ng-model="selectedcolumn" 
                         ng-options="column for column in ['mileage', 'name', 'model', 'engine', 'color']"></select>   
                    </div>
                    <div class="col-xs-6 col-md-6 col-sm-6" style="text-align: center">
                        <h4 class="CS_Table_Header">Car Details</h4>
                    </div>
                    <div class="col-xs-3 col-md-3 col-sm-3">
                        Search :
                        <input type="text" placeholder="Search" ng-model="SearchText[selectedcolumn]" />
                    </div>
                </div>
            </div>
            <!--Panel Body-->
            <div class="panel-body">
                 <table style="width: 100%" class="table table-striped" id="mydata">
                    <tr>
                        <!-- Header column made clickable by performing sorting , filtering using Angular JS-->
                        <th data-ng-click="sortData('mileage')">Mileage
                            <div ng-class="getSort('mileage')"></div>
                        </th>
                        <th ng-click="sortData('name')">Name
                            <div ng-class="getSort('name')"></div>
                        </th>
                        <th ng-click="sortData('model')">Model
                            <div ng-class="getSort('model')"></div>
                        </th>
                        <th ng-click="sortData('engine')">Engine
                            <div ng-class="getSort('engine')"></div>
                        </th>
                        <th ng-click="sortData('color')">Color
                            <div ng-class="getSort('color')"></div>
                        </th>
                    </tr>

                     <!-- Retrieves the data using Angular Directives -->
                    <tr ng-repeat="car in cars | orderBy :sortColumn:revertColumn | filter:SearchText ">
                        <td>{{car.mileage}}
                        </td>
                        <td>{{car.name}}
                        </td>
                        <td>{{car.model}}
                        </td>
                        <td>{{car.engine}}
                        </td>
                        <td>{{car.color}}
                        </td>
                    </tr>

                </table>
            </div>
        </div>
        <!-- Panel Ends-->
    </div>
    
</asp:Content>

