using EtherscanAssessment.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static EtherscanAssessment.Entities.TokenModel;

namespace EtherscanAssessment
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod()]
        public static TokenDBModel GetTokenById(int id)
        {
            var tokenService = new TokenService();
            return tokenService.GetTokenByID(id, ConfigurationManager.ConnectionStrings["localhost"].ConnectionString);
        }
    }
}