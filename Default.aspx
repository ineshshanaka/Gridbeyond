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
                    <asp:FileUpload ID="FileUpload1" runat="server" class="btn btn-primary btn-lg" />    
                </td>    
                <td>    
                </td>    
                <td style="width:40%">    
                    <asp:Button ID="Button1" class="btn btn-primary btn-lg" runat="server" Text="Upload" OnClick="Button1_Click_FileUpload" />    
                </td>    
            </tr>    
        </table>
    </div>
</asp:Content>
