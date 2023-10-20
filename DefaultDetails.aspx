<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DefaultDetails.aspx.cs" Inherits="EtherscanAssessment.DefaultDetails" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <div class="row">
        <div class="col-12">
            <div class="card mx-2" style="min-height: 300px">
                <div class="card-header mb-2"><strong><asp:Label ID="lblContractAddress" runat="server"></strong></asp:Label></div>
                    <div class="row mb-2  mx-2">
                        <strong>Name:</strong>  <asp:TextBox ID="nameView" runat="server" class="form-control" Enabled="false" />
                    </div>
                    <div class="row mb-2  mx-2">
                        <strong>Price:</strong> <asp:TextBox ID="priceView" runat="server" class="form-control" Enabled="false" />
                    </div>
                    <div class="row mb-2  mx-2">
                        <strong>Total Supply:</strong> <asp:TextBox ID="totalSupplyView" runat="server" class="form-control" Enabled="false" />
                    </div>
                    <div class="row mb-2  mx-2">
                        <strong>Total Holders:</strong> <asp:TextBox ID="totalHoldersView" runat="server" class="form-control" Enabled="false" />
                    </div>
                </div>
            </div>
        </div>

    <script>
    </script>
</asp:Content>


