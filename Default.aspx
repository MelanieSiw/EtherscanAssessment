<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EtherscanAssessment.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-6">
            <div class="card mx-2" style="min-height: 300px">
                <div class="card-header mb-2">Save / Update Token</div>
                <form class="card-body" id="tokenForm" >
                    <div class="row mb-2  mx-2">
                        <label for="inputName" class="col-sm-3 col-form-label">Name</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="name" runat="server" class="form-control" />
                        </div>
                    </div>
                
                    <div class="row mb-2  mx-2">
                        <label for="inputSymbol" class="col-sm-3 col-form-label">Symbol</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="symbol" runat="server" class="form-control" />
                        </div>
                    </div>
                
                    <div class="row mb-2 mx-2">
                        <label for="inputContractAddress" class="col-sm-3 col-form-label">Contract Address</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="contractAddress" runat="server" class="form-control" />
                        </div>
                    </div>
                
                    <div class="row mb-2 mx-2">
                        <label for="inputTotalSupply" class="col-sm-3 col-form-label">TotalSupply</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="totalSupply" runat="server" class="form-control" onkeypress="return isNumberKey(event)"/>
                        </div>
                    </div>
                
                    <div class="row mb-2 mx-2">
                        <label for="inputTotalHolders" class="col-sm-3 col-form-label">Total Holders</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="totalHolders" runat="server" class="form-control" onkeypress="return isNumberKey(event)"/>
                        </div>
                    </div>

                    <div class="d-flex justify-content-center mb-2">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary me-1"  OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-warning ms-1"  OnClick="btnReset_Click" />
                    </div>

                </form>
            </div>

        </div>
        <div class="col-6">
            <div class="card">
                <div class="card-header">Token Statistics by Token Supply</div>
                <div  class="card-body" style="height: 300px">
                    <div class="chartjs-size-monitor" style="position: absolute; left: 0px; top: 0px; right: 0px; bottom: 0px; overflow: hidden; pointer-events: none; visibility: hidden; z-index: -1;">
                        <div class="chartjs-size-monitor-expand" style="position:absolute;left:0;top:0;right:0;bottom:0;overflow:hidden;pointer-events:none;visibility:hidden;z-index:-1;">
                            <div style="position:absolute;width:1000000px;height:1000000px;left:0;top:0"></div>
                        </div>
                        <div class="chartjs-size-monitor-shrink" style="position:absolute;left:0;top:0;right:0;bottom:0;overflow:hidden;pointer-events:none;visibility:hidden;z-index:-1;">
                            <div style="position:absolute;width:200%;height:200%;left:0; top:0"></div>
                        </div>
                    </div> 
                    <div id="chartLocation"> 

                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:Button ID="btnDownload" runat="server" Text="Export as CSV" CssClass="btn btn-primary me-1"  OnClick="DownloadToCSV_Click" />

    <asp:Table ID="tokenTable" runat="server" CssClass="table">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell>Name</asp:TableHeaderCell>
            <asp:TableHeaderCell>Symbol</asp:TableHeaderCell>
            <asp:TableHeaderCell>Contract Address</asp:TableHeaderCell>
            <asp:TableHeaderCell>Total Supply</asp:TableHeaderCell>
            <asp:TableHeaderCell>Total Holders</asp:TableHeaderCell>
            <asp:TableHeaderCell>Total Supply (%)</asp:TableHeaderCell>
            <asp:TableHeaderCell></asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>

        <!-- Edit Modal -->
    <div class="modal fade" id="editModal" tabindex="-1" aria-labelledby="editModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="editModalLabel">Edit Modal</h5>
                    <button type="button" class="close modalCloseBtn" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <form class="card-body" id="tokenEditForm">
                        <div class="row mb-2">
                            <label for="inputName" class="col-sm-3 col-form-label">Name</label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="editName" runat="server" class="form-control" />
                            </div>
                        </div>

                        <div class="row mb-2">
                            <label for="inputSymbol" class="col-sm-3 col-form-label">Symbol</label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="editSymbol" runat="server" class="form-control" />
                            </div>
                        </div>

                        <div class="row mb-2">
                            <label for="inputContractAddress" class="col-sm-3 col-form-label">Contract Address</label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="editContractAddress" runat="server" class="form-control" />
                            </div>
                        </div>

                        <div class="row mb-2">
                            <label for="inputTotalSupply" class="col-sm-3 col-form-label">TotalSupply</label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="editTotalSupply" runat="server" class="form-control" />
                            </div>
                        </div>

                        <div class="row mb-2">
                            <label for="inputTotalHolders" class="col-sm-3 col-form-label">Total Holders</label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="editTotalHolders" runat="server" class="form-control" />
                            </div>
                        </div>
                    
                        <div class="row mb-2" id="editPriceRow">
                            <label for="inputTotalHolders" class="col-sm-3 col-form-label">Price</label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="editPrice" runat="server" class="form-control" />
                            </div>
                        </div>
                    
                        <asp:TextBox ID="editId" runat="server" class="form-control" style="display: none;" />
      
                    </form>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnCloseModal" runat="server" Text="Close" CssClass="btn btn-primary me-1"  />
                     <button id="formEditSubmit" type="button" onclick="submitEditForm()" class="btn btn-primary">Save changes</button>
                </div>

            </div>
        </div>
    </div>

    <script src='https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.1.4/Chart.bundle.min.js'></script>

    <script>
        let chart = null;

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;

            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }

            return true;
        }

        function editTBody(id) {
            $('#editModal').modal('toggle');
            var formData = { id: id };
            $.ajax({
                type: "POST",
                url: "https://localhost:44399/Default.aspx/GetTokenById",
                data: JSON.stringify(formData),
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    console.log(data);
                    console.log(data.d);
                    var response = data.d;
                    console.log(response.Name);
                    $('#<%= editName.ClientID %>').val(response.Name);
                       $('#<%= editSymbol.ClientID %>').val(response.Symbol);
                       $('#<%= editContractAddress.ClientID %>').val(response.ContractAddress);
                       $('#<%= editTotalSupply.ClientID %>').val(response.TotalSupply);
                       $('#<%= editTotalHolders.ClientID %>').val(response.TotalHolders);
                       $('#<%= editId.ClientID %>').val(response.Id);

                       $("#<%= editName.ClientID %>").prop('disabled', false);
                       $("#<%= editSymbol.ClientID %>").prop('disabled', false);
                       $("#<%= editContractAddress.ClientID %>").prop('disabled', false);
                  $("#<%= editTotalSupply.ClientID %>").prop('disabled', false);
                  $("#<%= editTotalHolders.ClientID %>").prop('disabled', false);
                  $('#formEditSubmit').show();
                  $('#editPriceRow').hide();
                  $('#editModalLabel').text("Edit Token");

              },
              error: function (xhr, ajaxOptions, thrownError) {
                  alert(xhr.status);
                  alert(thrownError);
              }
          });
      }

      function submitEditForm() {
          let data = {
              "token": {
                  "id": $('#<%= editId.ClientID %>').val(),
                  "symbol": $('#<%= editSymbol.ClientID %>').val(),
                  "name": $('#<%= editName.ClientID %>').val(),
                  "totalSupply": $('#<%= editTotalSupply.ClientID %>').val(),
                  "contractAddress": $('#<%= editContractAddress.ClientID %>').val(),
                  "totalHolders": $('#<%= editTotalHolders.ClientID %>').val()
              }
          };

          $.ajax({
              type: "POST",
              url: "https://localhost:44399/Default.aspx/UpdateToken",
              data: JSON.stringify(data),
              contentType: "application/json; charset=utf-8",
              success: function () {
                  $("#editModal").modal('hide');
                  alert("Details updated successfully!");
                  location.reload()
              },
              error: function (xhr, ajaxOptions, thrownError) {
                  alert(xhr.status);
                  alert(thrownError);
              }
          });
      }

        $(document).ready(function () {
            let chartLabels = [];
            let chartDataSets = [];

            $.ajax({
                type: "POST",
                url: "https://localhost:44399/Default.aspx/GetTokenListForChart",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    console.log(response.d.Result);
                    for (let i = 0; i < response.d.Result.length;  i++) {
                        chartLabels.push(response.d.Result[i].Name);
                        chartDataSets.push(response.d.Result[i].TotalSupply);
                    }

                    $("#chartLocation").empty();
                    $("#chartLocation").append(`<canvas id="chart-line" width="299" height="130" class="chartjs-render-monitor" style="display: block; width: 299px; height: 130px;"></canvas>`);
                    var ctx = $("#chart-line");
                    console.log(chartDataSets)
                    chart = new Chart(ctx, {
                        type: 'doughnut',
                        data: {
                            labels: chartLabels,
                            datasets: [{
                                data: chartDataSets,
                                backgroundColor: ["" +
                                    "#F7B7A3",
                                    "#EA5F89",
                                    "#9B3192",
                                    "#9B3192",
                                    "#57167E",
                                    "#2B0B3F",
                                    "#FFC154",
                                    "#47B39C",
                                    "#47B39C"
                                ]
                            }]
                        }
                    });
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });
        })
 
    </script>
</asp:Content>


