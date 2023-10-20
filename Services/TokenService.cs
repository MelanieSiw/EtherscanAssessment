using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using EtherscanAssessment.Entities.Data;
using MySql.Data.MySqlClient;
using static EtherscanAssessment.Entities.TokenModel;
using static Dapper.SqlMapper;
using System.Data;

namespace EtherscanAssessment.Services
{
    public class TokenService
    {
        public int Create(Token token, string connectionString)
        {
            var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                var query = @"
                    INSERT INTO token
                    (symbol, name, total_supply, contract_address, total_holders, price)
                    VALUES (@symbol, @name, @totalSupply, @contractAddress, @totalHolders, @price);";
                MySqlCommand command = new MySqlCommand(query);
                command.Connection = connection;
                command.Parameters.AddWithValue("@symbol", token.Symbol);
                command.Parameters.AddWithValue("@name", token.Name);
                command.Parameters.AddWithValue("@totalSupply", token.TotalSupply);
                command.Parameters.AddWithValue("@contractAddress", token.ContractAddress);
                command.Parameters.AddWithValue("@totalHolders", token.TotalHolders);
                command.Parameters.AddWithValue("@price", token.Price);

                return command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<TokenDBModel> GetList(string connectionString)
        {
            var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                var query = $@"
                    SELECT id, symbol, name, total_supply as TotalSupply, contract_address as ContractAddress, total_holders as TotalHolders, price, 
                    total_supply / (SELECT SUM(total_supply) from token) * 100 as TotalSupplyPercentage
                    FROM token";
                return connection.Query<TokenDBModel>(query).ToList();
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                connection.CloseAsync();
            }
        }

        public DataTable GetTableList(string connectionString)
        {
            var tableList = GetList(connectionString);

            DataTable table = new DataTable();

            table.Columns.Add("ID", typeof(string));
            table.Columns.Add("Symbol", typeof(string));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Total Supply", typeof(string));
            table.Columns.Add("Contract Address", typeof(string));
            table.Columns.Add("Total Holders", typeof(string));
            table.Columns.Add("Price", typeof(string));
            table.Columns.Add("Total Supply (%)", typeof(string));

            foreach (var tokens in tableList)
            {
                table.Rows.Add(tokens.Id, tokens.Symbol, tokens.Name, tokens.TotalSupply, tokens.ContractAddress, tokens.TotalHolders, tokens.Price, tokens.TotalSupplyPercentage);
            }

            return table;
        }

        public async Task<List<Token>> GetAll(string connectionString)
        {
            var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                var query = $@"
                    SELECT *
                    FROM token";

                return connection.Query<Token>(query).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        public async Task<List<TokenDBModel>> GetAllTokenForPeiChart(string connectionString)
        {
            var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                var query = $@"
                    SELECT id, symbol, name, total_supply as TotalSupply, contract_address as ContractAddress, total_holders as TotalHolders, price, 
                    total_supply / (SELECT SUM(total_supply) from token) * 100 as TotalSupplyPercentage
                    FROM token";

                return connection.Query<TokenDBModel>(query).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        public TokenDBModel GetTokenBySymbol (string symbol, string connectionString)
        {
            var connection = new MySqlConnection(connectionString);
            try
            {
                var query = $@"
                SELECT id, symbol, name, total_supply as TotalSupply, contract_address as ContractAddress, total_holders as TotalHolders, price
                FROM token
                WHERE symbol = @Symbol";

                return connection.QuerySingle<TokenDBModel>(query, new {Symbol = symbol} );
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                connection.CloseAsync();
            }
        }
        public TokenDBModel GetTokenByID(int id, string connectionString)
        {
            var connection = new MySqlConnection(connectionString);
            try
            {
                var query = $@"
                SELECT id, symbol, name, total_supply as TotalSupply, contract_address as ContractAddress, total_holders as TotalHolders, price
                FROM token
                WHERE id = {id}";

                return connection.QuerySingle<TokenDBModel>(query);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                connection.CloseAsync();
            }
        }


        public async Task<PaginateModal<List<TokenDBModel>>> GetPaginationList(int pageNo, int pageSize, string connectionString)
        {
            var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                var query = $@"
                    SELECT id, symbol, name, total_supply as TotalSupply, contract_address as ContractAddress, total_holders as TotalHolders, price, 
                    total_supply / (SELECT SUM(total_supply) from token) * 100 as TotalSupplyPercentage
                    FROM token
                    LIMIT {pageSize * (pageNo - 1)}, {pageSize}";

                var result = connection.Query<TokenDBModel>(query).ToList();
                var resultTotalItem = connection.QuerySingle<long>("SELECT COUNT(1) FROM token");
                var resultTotalPage = (long)Math.Ceiling((decimal)resultTotalItem / pageSize);

                var paginateResult = new PaginateModal<List<TokenDBModel>>
                {
                    Data = result,
                    TotalPage = resultTotalPage,
                    TotalItem = resultTotalItem,
                    PageNo = pageNo,
                    PageSize = pageSize
                };

                return paginateResult;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        public int Update (Token token, string connectionString)
        {
            var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                var query = $@"
                    UPDATE etherscan_test.token
                    SET symbol = @symbol, name = @name, total_supply = @totalSupply, contract_address = @contractAddress, total_holders = @totalHolders
                    WHERE id = {token.Id}";
                MySqlCommand command = new MySqlCommand(query);
                command.Connection = connection;
                command.Parameters.AddWithValue("@symbol", token.Symbol);
                command.Parameters.AddWithValue("@name", token.Name);
                command.Parameters.AddWithValue("@totalSupply", token.TotalSupply);
                command.Parameters.AddWithValue("@contractAddress", token.ContractAddress);
                command.Parameters.AddWithValue("@totalHolders", token.TotalHolders);

                return command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw;
            }
            finally
            {
                connection.CloseAsync();
            }
        }

        public static int UpdatePricing (int id, decimal price, string connectionString)
        {
            var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                var query = $@"
                    UPDATE  etherscan_test.token
                    SET price = @price
                    WHERE id = {id}";
                MySqlCommand command = new MySqlCommand(query);
                command.Connection = connection;
                command.Parameters.AddWithValue("@price", price);

                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                connection.CloseAsync();
            }
        }
    }
}