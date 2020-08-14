<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="Gridbeyond.DashBoard" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>  
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.1.0/js/bootstrap.min.js"></script> 
        <style>
    * {
      box-sizing: border-box;
    }

    .column {
      float: left;
      width: 50%;
      padding: 10px;
      height: 100px;
    }

     .column2 {
      float: left;
      width: 50%;
      padding: 10px;
      height: 100px;
    }

    .row:after {
      content: "";
      display: table;
      clear: both;
    }

    .para {
      display: block;
      margin-top: 1em;
      margin-bottom: 1em;
      margin-left: 0;
      margin-right: 0;
      color:darkgreen;
    }
    </style>
    <div class="short-div"> 
      <div class="row">
        <div class="column" style="background-color:white;">
    
            <asp:Label ID ="LabelMAX" Width ="150px" Font-Size="Medium" Text="Max Price" CssClass="col-md-2 control-label"  runat ="server"></asp:Label>	
            <asp:TextBox ID="TextMAX" Width="100px"  CssClass="form-control" runat="server"></asp:TextBox><br /><br />

        </div>

        <div class="column2" style="background-color:white;">
                         
            <asp:Label ID ="LabelMIN" Width ="150px" Font-Size="Medium" Text="Min Price" CssClass="col-md-2 control-label"  runat ="server"></asp:Label>	
            <asp:TextBox ID="TextBoxMIN" Width="100px"  CssClass="form-control" runat="server"></asp:TextBox><br /><br /> 

        </div>
      </div>
    </div>

    <div class="short-div"> 
      <div class="row">
        <div class="column" style="background-color:white;">
    
            <asp:Label ID ="LabelMaxDate" Width ="150px" Font-Size="Medium" Text="Max Value Date" CssClass="col-md-2 control-label"  runat ="server"></asp:Label>	
            <asp:TextBox ID="TextBoxMaxvaluDate" Width="300px"  CssClass="form-control" runat="server"></asp:TextBox><br /><br />

        </div>

        <div class="column2" style="background-color:white;">
                         
            <asp:Label ID ="LabelMaxValue" Width ="150px" Font-Size="Medium" Text="MaxValue Time Range / Value" CssClass="col-md-2 control-label"  runat ="server"></asp:Label>	
            <asp:TextBox ID="TextBoxMaxValuTime" Width="300px"  CssClass="form-control" runat="server"></asp:TextBox><br /><br /> 

        </div>
      </div>
    </div>

    <div>
        <asp:Chart ID="Chart1" runat="server" Width="1200" Height ="600">  
            <Titles>  
                <asp:Title Text="Daily Price Average"></asp:Title>  
            </Titles>  
            <Series>  
                <asp:Series Name="Series1" ChartArea="ChartArea1"></asp:Series>  
            </Series>  
            <ChartAreas>   
                <asp:ChartArea Name="ChartArea1" BackGradientStyle="TopBottom" Area3DStyle-Enable3D="True">  
                    <area3dstyle Rotation="10" perspective="10" Inclination="15" IsRightAngleAxes="False" wallwidth="0" IsClustered="False"></area3dstyle>
                    <AxisX Interval="1" Title="Date" IsLabelAutoFit="true" LabelAutoFitStyle ="LabelsAngleStep90"> <MajorGrid Enabled ="False" /></AxisX>  
                    <AxisY Title="Average"> <MajorGrid Enabled ="False" /></AxisY>  
                </asp:ChartArea>  
            </ChartAreas>  
        </asp:Chart>  
     </div>

    <div>
        <asp:Label ID ="lblMessage" Font-Size="Large" Text="Select Date" CssClass="col-md-2 control-label"  runat ="server"></asp:Label>	
        <asp:DropDownList ID="ddlDate" Width="300px" AutoPostBack="true"  CssClass="form-control" runat="server"  OnSelectedIndexChanged="ddlDate_SelectedIndexChanged" ></asp:DropDownList>
       
        <asp:Chart ID="Chart2" runat="server" Width="1200" Height ="600">  
            <Titles>  
                <asp:Title Text=""></asp:Title>  
            </Titles>  
            <Series>  
                <asp:Series Name="Series1" ChartArea="ChartArea1"></asp:Series>  
            </Series>  
            <ChartAreas>   
                <asp:ChartArea Name="ChartArea1" BackGradientStyle="TopBottom" Area3DStyle-Enable3D="True">  
                    <area3dstyle Rotation="10" perspective="10" Inclination="15" IsRightAngleAxes="False" wallwidth="0" IsClustered="False"></area3dstyle>
                    <AxisX Interval="1" Title="Time" IsLabelAutoFit="true" LabelAutoFitStyle ="LabelsAngleStep90"> <MajorGrid Enabled ="False" /></AxisX>  
                    <AxisY Title="Price"> <MajorGrid Enabled ="False" /></AxisY>  
                </asp:ChartArea>  
            </ChartAreas>  
        </asp:Chart>  
     </div>
</asp:Content>
