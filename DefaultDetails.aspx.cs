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
    public partial class DefaultDetails : Page
    {
        TokenController token = new TokenController(new TokenService());
        protected void Page_Load(object sender, EventArgs e)
        {
            var tokenService = new TokenService();
            
            var resp = tokenService.GetTokenBySymbol(Request.QueryString["id"], ConfigurationManager.ConnectionStrings["localhost"].ConnectionString);

            if (resp != null)
            {
                nameView.Text = resp.Name;
                priceView.Text = "USD " + resp.Price.ToString();
                totalSupplyView.Text = resp.TotalSupply.ToString();
                totalHoldersView.Text = resp.TotalHolders.ToString();
                lblContractAddress.Text = resp.ContractAddress;
            }
            else
            {
                Console.WriteLine("No Token Found.");
            }
        }
    }

}