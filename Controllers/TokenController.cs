using static EtherscanAssessment.Entities.TokenModel;
using EtherscanAssessment.Entities.Data;
using static EtherscanAssessment.Services.TokenService;
using EtherscanAssessment.Services;
using System.Net;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Http;
using System;
using Microsoft.Extensions.Configuration;

namespace Api.Controllers
{
    [Route("[controller]")]
    public class TokenController
    {
        private IConfiguration _configuration;
        private readonly TokenService _tokenService;
        private readonly string _connection;
        private static int _pageSize = 10;

        public TokenController(TokenService tokenService)
        {
            _tokenService = tokenService;
            _connection = ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;
        }


        [HttpGet]
        [Route("GetPaginateList")]
        public async Task<PaginateModal<List<TokenDBModel>>> GetPaginateList(int pageNo)
        {
            return await _tokenService.GetPaginationList(pageNo, _pageSize, _connection);
        }

        [HttpGet]
        [Route("GetTokenById")]
        public TokenDBModel GetTokenById(string symbol)
        {
            return  _tokenService.GetTokenBySymbol(symbol, _connection);
        }

        [HttpPost]
        public bool InsertToken(Token token)
        {
            try
            {
                _tokenService.Create(token, _connection);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        [HttpPost]
        [Route("UpdateToken")]
        public bool UpdateToken(Token token)
        {
            try
            {
                _tokenService.Update(token, _connection);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Failed to insert token");
                return false;
            }

            return true;
        }
    }
}