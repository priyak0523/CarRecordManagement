<%@ Page Title="" Language="C#" MasterPageFile="~/Header.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="sunovaSortingFiltering.HomePage" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="MasterPagePlaceHolderHead" runat="server">
    <!-- 
    Programmer  : KalaiPriya
    Date        : 03/03/2017
    Description : This page will act as Home page
-->
    <title>Home Page</title>

</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="MasterPagePlaceHolderBody" runat="server">
    <div style="margin:0px auto;width:600px;font-size:large;line-height:40px;">
        <ul style="list-style:none;">
            <li>Developed the project using ASP .NET, C#, Angular JS, Bootstrap</li>
            <li>Retreived the XML from URL and displayed the data in Grid View</li>
            <li>Performed data sorting and filtering on both server side and client side </li>
        </ul>
       
    </div>
    <div style="text-align:center;">
        <figure>
            <img class="fblogo" src="/Assets/images/Server.jpg" alt="Sample Photo" data-toggle="modal" data-target="#serverModal" />
         <img class="fblogo" src="/Assets/images/Client.jpg" alt="Sample Photo" data-toggle="modal" data-target="#clientModal" />
            <figcaption>Please click on the image</figcaption>
        </figure>
        
    </div> 

      <!--Client Modal -->
  <div class="modal fade" id="clientModal" role="dialog">
    <div class="modal-dialog">
    
      <!-- Client Modal content-->
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title">Client Side Sorting and Filtering</h4>
        </div>
        <div class="modal-body">
            <ul>
                <li>Performed the action using Angular JS</li>
                <li>Sorted and Filtered the data on the client side without causing any kind of postback</li>
            </ul>
        </div>
        <div class="modal-footer">  
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
  

      <!--Server Modal -->
  <div class="modal fade" id="serverModal" role="dialog">
    <div class="modal-dialog">
    
      <!-- Server Modal content-->
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title">Server Side Sorting and Filtering</h4>
        </div>
        <div class="modal-body">
            <ul>
                <li>Performed the action on server side</li>
                <li>Whenever the user clicks on the header column or filter image , table data gets sorted or filtered respectively.</li>
            </ul>
        </div>
        <div class="modal-footer">  
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>


   
     
   
</asp:Content>

