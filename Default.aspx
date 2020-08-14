<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Gridbeyond._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>File Upload</h1>
        <p class="lead">Please Select a file to Upload To The Database
        </p>
         <table style="width:100%">    
            <tr>    
                <td style="width:30%">    
                   <h2>Select File</h2>
                </td>    
                <td style="width:30%">    
                    <asp:FileUpload ID="FileUploadCSV" runat="server" class="btn btn-primary btn-lg" /> 
                     <asp:RequiredFieldValidator runat="server" Display="Dynamic" ErrorMessage="* Required Field" ControlToValidate="FileUploadCSV">
                    </asp:RequiredFieldValidator>
                </td>    
                <td>    
                </td>    
                <td style="width:40%">    
                    <asp:Button ID="ButtonUpload" class="btn btn-primary btn-lg" runat="server" Text="Upload" OnClick="Buttonupload_Click_FileUpload" />    
                </td>    
            </tr>    
        </table>
    </div>

    <div id="MessageModal" class="modal fade">
	<div class="modal-dialog modal-confirm">
		<div class="modal-content">
			<div class="modal-header">						
				<h4 class="modal-title">Attention !</h4>	
			</div>
			<div class="modal-body">
                <asp:Label ID ="lblMessage" Font-Size="Large" class="label label-default" runat ="server"></asp:Label>		
			</div>
			<div class="modal-footer">
				<button class="btn btn-success btn-block" data-dismiss="modal">OK</button>
			</div>
		</div>
	</div>
</div>
</asp:Content>
