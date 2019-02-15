<%@ Page Title="" Language="C#" MasterPageFile="~/HomeMaster.Master" AutoEventWireup="true" EnableViewState="false" CodeBehind="Home.aspx.cs" Inherits="Property.Home" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">

        .new_pop {
    border: 2px solid 
}
.btn.btn-default.mdl_sbmt_btn {
    background:#02344a !important;
    color:white !important;
    }
.modl_sct > input {
    margin: 0 !important;
    padding: 7px !important;
}
        .gmnoprint img
        {
            max-width: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<asp:ScriptManager runat="server" ID="update"></asp:ScriptManager>--%>
  
    <!-- Trigger the modal with a button -->
  <button id="btnModal"  type="button" style="visibility:hidden"  class="btn btn-info btn-lg"  data-toggle="modal" data-target="#myModalNew" data-backdrop="static" data-keyboard="false">Open Modal</button>
<%--<div id ="div" runat="server" visible="false">--%>
  <!-- Modal -->
  <div class="modal fade" id="myModalNew" role="dialog">
      <div class="modal-backdrop fade in"></div>
    <div class="modal-dialog respon">
    
      <!-- Modal content-->
      <div class="modal-content new_pop">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title modl_hdng">Subscribe Newsletter</h4>
        </div>
        <div class="modal-body modl_bg" >
            <div class="modl_sct">
            <label>Name</label>
            <input type="text" id="txtName" pattern="[a-zA-Z\s]+" title="Enter your name"  value="" runat="server" />
                
                 </div>
         <div class="modl_sct">
             <label>Email</label>
            <input type="text" name="email" id="txtEmailID" pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$" title="Enter your emailID" runat="server" value="" />
            
         </div>
            <div class="modl_sct">
             <label>Phone</label>
            <input type="text"  id="txtPhone"  pattern="[0-9]{10}" title="Enter your mobile number" value="" runat="server"/>
              
            </div>
            <label id="lblError" runat="server"></label>
        </div>
        <div class="modal-footer">
           <button id="btnSubmit" onserverclick="btnSaveUserInfo_Clicked" ValidationGroup="UserInfo" runat="server" class="btn btn-default mdl_sbmt_btn">Submit</button>
            <button type="button" class="btn btn-default mdl_cls_btn" data-dismiss="modal">Close</button>
        </div>
      </div>
      
    </div>
  </div>
    <%--</div>--%>
  


     <script type="text/javascript">
         function openModal() {             
             backdrop: 'static';
             keyboard: false;
             $('#btnModal').click();
             //$('#popup_back_blur').addClass('blur-it');
         }
         $('#btnSubmit').click(function () {
             //$('#popup_back_blur').removeClass('blur-it');
             var name = document.getElementById("txtName").value;
             var emailId = document.getElementById("txtEmailID").value;
             var phone = document.getElementById("txtPhone").value;
             debugger;
             if (name == "" && emailId == "" && phone == "") {
                 alert("please fill all details.");
             }
             else {

             }
         }
             )
         function ValidateMobNumber(txtPhone) {
             var phone = document.getElementById("txtPhone");
             if (phone.value == "") {
                 alert("You didn't enter a phone number.");
                 phone.value = "";
                 phone.focus();
                 return false;
             }
             else if (isNaN(phone.value)) {
                 alert("The phone number contains illegal characters.");
                 phone.value = "";
                 phone.focus();
                 return false;
             }
             else if (!(phone.value.length == 10)) {
                 alert("The phone number is the wrong length. \nPlease enter 10 digit mobile no.");
                 phone.value = "";
                 phone.focus();
                 return false;
             }
         }


 </script>
</asp:Content>
