using CacheService.Interfaces;
using CacheService.Models;
using Dapper;
using MySqlConnector;
using System.Data;

namespace CacheService.Dal
{

    public class MetaData : IMetaData
    {
        private readonly string _connectionString;


        public MetaData(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<ConnectionStringModel> GetConnections()
        {
            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                dbConnection.Open();
                string sqlQuery = "SELECT * FROM ConnectionStrings";
                return dbConnection.Query<ConnectionStringModel>(sqlQuery).ToList();

            }

        }

        public ConnectionStringModel GetConnectionById(int id)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Connection string is not initialized.");
            }

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                dbConnection.Open();
                string sqlQuery = "SELECT * FROM ConnectionStrings WHERE id = @Id";

                // Use parameterized query to prevent SQL injection
                var result = dbConnection.QuerySingleOrDefault<ConnectionStringModel>(sqlQuery, new { Id = id });

                if (result == null)
                {
                    // Handle the case where no record is found (optional)
                    // For example, you might throw a custom exception or return a default value
                    throw new KeyNotFoundException($"No connection string found with id {id}");
                }

                return result;
            }
        }
    }
}
