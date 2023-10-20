using Api.Controllers;
using EtherscanAssessment.Entities.Data;
using EtherscanAssessment.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using static EtherscanAssessment.Entities.TokenModel;

namespace EtherscanAssessment
{
    public partial class Default : Page
    {
        TokenController token = new TokenController(new TokenService());
        protected void Page_Load(object sender, EventArgs e)
        {
            var paginateList = token.GetPaginateList(1).Result;
            List<decimal> totalSupplies = new List<decimal>();

            var rowList = new List<TableRow>();

            foreach (var item in paginateList.Data)
            {
                var row = new TableRow();

                var nameCell = new TableCell();
                nameCell.Text = item.Name;
                row.Cells.Add(nameCell);

                var symbolCell = new TableCell();
                var detailLink = new HyperLink();
                detailLink.Text = item.Symbol;
                detailLink.NavigateUrl = "DefaultDetails.aspx?id=" + item.Symbol;
                symbolCell.Controls.Add(detailLink);
                row.Cells.Add(symbolCell);

                var contractAddressCell = new TableCell();
                contractAddressCell.Text = item.ContractAddress;
                row.Cells.Add(contractAddressCell);

                var totalSupplyCell = new TableCell();
                totalSupplyCell.Text = item.TotalSupply.ToString();
                row.Cells.Add(totalSupplyCell);

                var totalHoldersCell = new TableCell();
                totalHoldersCell.Text = item.TotalHolders.ToString();
                row.Cells.Add(totalHoldersCell);

                var totalSupplyPercentageCell = new TableCell();
                totalSupplyPercentageCell.Text = item.TotalSupplyPercentage.ToString();
                row.Cells.Add(totalSupplyPercentageCell);

                var editButtonCell = new TableCell();
                var editButton = new Button();
                editButton.Text = "Edit";
                editButton.CommandName = "Edit";
                editButton.CssClass = "btn btn-primary";
                editButton.UseSubmitBehavior = false;

                editButton.CommandArgument = item.Id.ToString();
                editButton.OnClientClick = $"editTBody({item.Id}); return false;";
                editButtonCell.Controls.Add(editButton);
                row.Cells.Add(editButtonCell);

                rowList.Add(row);
                
            }
            tokenTable.Rows.AddRange(rowList.ToArray());
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Token model = new Token
            {

                Name = name.Text,
                Symbol = symbol.Text,
                ContractAddress = contractAddress.Text,
                TotalSupply = Convert.ToInt32(totalSupply.Text),
                TotalHolders = Convert.ToInt32(totalHolders.Text),
            };

            bool submit = token.InsertToken(model);
            if (submit)
            {
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            // Clear the input fields (textboxes)
            name.Text = string.Empty;
            symbol.Text = string.Empty;
            contractAddress.Text = string.Empty;
            totalSupply.Text = string.Empty;
            totalHolders.Text = string.Empty;
        }

        public void EditButton_Click(object sender, EventArgs e)
        {
            Button editButton = (Button)sender;
            string itemID = editButton.CommandArgument;
        }

        [System.Web.Services.WebMethod()]
        public static TokenDBModel GetTokenById(int id)
        {
            var tokenService = new TokenService();
            var a =  tokenService.GetTokenByID(id, ConfigurationManager.ConnectionStrings["localhost"].ConnectionString);
            return a;
        }

        [System.Web.Services.WebMethod()]
        public static TokenDBModel UpdateToken(Token token)
        {
            var tokenService = new TokenService();
            tokenService.Update(token, ConfigurationManager.ConnectionStrings["localhost"].ConnectionString);

            var result = new TokenDBModel();
            return result;
        }

        [System.Web.Services.WebMethod()]
        public async static Task<List<TokenDBModel>> GetTokenListForChart()
        {
            var tokenService = new TokenService();
            return await tokenService.GetAllTokenForPeiChart(ConfigurationManager.ConnectionStrings["localhost"].ConnectionString);
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            string name = editName.Text;
            string symbol = editSymbol.Text;
            string contractAddress = editContractAddress.Text;
            string totalSupply = editTotalSupply.Text;
            string totalHolders = editTotalHolders.Text;

            Token model = new Token
            {

                Name = editName.Text,
                Symbol = editSymbol.Text,
                ContractAddress = editContractAddress.Text,
                TotalSupply = Convert.ToInt32(editTotalSupply.Text),
                TotalHolders = Convert.ToInt32(editTotalHolders.Text),
            };

            bool submit = token.UpdateToken(model);
            if (submit) Console.WriteLine("Success");
        }

        protected void DownloadToCSV_Click(object sender, EventArgs e)
        {
            var tokenService = new TokenService();
            var data = tokenService.GetList((ConfigurationManager.ConnectionStrings["localhost"].ConnectionString));

            if (data != null)
            {
                var csv = new System.Text.StringBuilder();
                csv.AppendLine("Name, Price, TotalSupply, TotalHolders, TotalSupplyPercentage");

                foreach (var token in data)
                {
                    csv.AppendLine($"{token.Name},{token.Price},{token.TotalSupply},{token.TotalHolders},{token.TotalSupplyPercentage}");
                }

                // Set the content type and headers for the response
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=data.csv");
                Response.Charset = "";
                Response.ContentType = "text/csv";

                // Write the CSV content to the response
                Response.Output.Write(csv);
                Response.Flush();
                Response.End();
            }
        }
    }

}