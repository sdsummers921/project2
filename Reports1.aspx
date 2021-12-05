<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reports1.aspx.cs" Inherits="SGIncReports.Reports1" Async="true"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Shabel Group, Inc Reports</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/StyleSheet1.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.6.0.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>

    <style>
        body {
    background-image: url(Images/blackslate4.jpg);
    background-attachment: fixed;
}
        .logo {
    display: block;
    margin-left: auto;
    margin-right: auto;
    width: 100%
}

        .white {
            background-color: #ffffff;
        }

        .gold {
            background-color: #B79600;
        }

        .char{
            background-color: #222C2E;
        }

        .blue {
    background-image: url(Images/bluebackground2.jpg);
}
        footer {
            color: #FFFFFF;
        }
        .black{
            background-image: url(Images/blackslate4.jpg);
            background-attachment: fixed;
        }

    </style>
    </head>
<body>
    <div class="container" >
    <form id="form1" runat="server" class="char">
    <div class="container" >
    <header class ="center"><img src="Images/SGBanner2.jpg" class="img-responsive img-rounded logo" /></header>
    <main class="container">
         <div class ="row black">
             <div class ="col-sm-12 col-md-2"></div>
             <div class ="col-sm-12 col-md-4">
                 <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged" TabIndex="1" DayStyle-BackColor="White" CssClass="align-self-center" Height="100%" Width="100%">
                     <DayHeaderStyle BackColor="White" />
<DayStyle BackColor="White"></DayStyle>

                     <TitleStyle BackColor="#72A3C2" />
                     <WeekendDayStyle BackColor="#C6E1EC" />
                 </asp:Calendar>
            </div>

            <div class ="col-sm-12 col-md-4">
                <asp:Label ID="CalendarLabel1" runat="server" BackColor="White" BorderColor="#D9AF82" BorderStyle="Outset" BorderWidth="5px" CssClass="align-middle" Height="100%" Width="100%"><br /><br /><p aria-multiline="True" aria-readonly="True" >Please select date to display reports generated on specified date.  If report table does not appear, there are no reports for selected date.  Please select another date or click "Refresh Table" button to reset table.</p><br /><br /></asp:Label>
             </div>
            <div class ="col-sm-12 col-md-2"></div>
         </div>
         <div class="row">
              <asp:Button ID="Button2" runat="server" OnClick="Button1_Click" Text="Refresh Table" BackColor="#222C2E" Font-Bold="True" Font-Size="Large" ForeColor="#D9AF82" TabIndex="3" />
              </div>
        <div class="row blue">
             <div class="col-sm-12 col-md-6"><asp:TextBox ID="TextBox1" runat="server" Height="100%" TextMode="MultiLine" Width="100%" ReadOnly="True" ValidateRequestMode="Disabled" TabIndex="2" Rows="10" BorderStyle="Outset" BorderColor="#D9AF82" BorderWidth="10px"></asp:TextBox>
             </div>
               <div class="col-sm-12 col-md-6 align-self-md-center">
                    <asp:GridView ID="gvDataModel" runat="server" BorderColor="#224A77" BorderStyle="Ridge" CssClass="align-content-around" TabIndex="4" Width="100%" OnSelectedIndexChanged="gvDataModel_SelectedIndexChanged" CellPadding="5" CellSpacing="8" Height="50%" AllowPaging="True" OnPageIndexChanging="gvDataModel_PageIndexChanging" BackColor="White">
                        <Columns>
                             <asp:CommandField HeaderText="Select Row" SelectText="Open Report" ShowSelectButton="True" />
                            </Columns>
                            <HeaderStyle BackColor="#72A3C2" BorderColor="#1D3F66" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <PagerSettings Mode="NumericFirstLast" />
                        <PagerStyle HorizontalAlign="Center" />
                            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100%" />
                            <SelectedRowStyle BackColor="#E3D1BB" />
                            </asp:GridView>
                        </div>
            </div>
            <div class ="row">
                 <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Refresh Table" BackColor="#222C2E" Font-Bold="True" Font-Size="Large" ForeColor="#D9AF82" TabIndex="5" />
            </div>

      </main>
             <footer>
      Copyright &copy; 2021 Shabel Group, Inc.
   </footer> 
    </div>
        </form>
  </div>
</body>
</html>
