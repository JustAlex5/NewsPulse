using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Dapper;
using CommonExtensionsInterface.Dal;

namespace CommonExtensions.Dal
{
    public class MySqlDal : IMysqlDal
    {
        private readonly string _connectionString;
        public MySqlDal(IConfiguration configuration)
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

        public Endpoint GetEndpointById(int id)
        {
            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                dbConnection.Open();
                string sqlQuery = "SELECT * FROM metadata.service_endpoints where id = @Id";

                // Use parameterized query to prevent SQL injection
                var result = dbConnection.QuerySingleOrDefault<Endpoint>(sqlQuery, new { Id = id });

                if (result == null)
                {
                    // Handle the case where no record is found (optional)
                    // For example, you might throw a custom exception or return a default value
                    throw new KeyNotFoundException($"No connection string found with id {id}");
                }

                return result;
            }
        }

        public List<Endpoint> GetEndpoints()
        {
            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                dbConnection.Open();
                string sqlQuery = "SELECT * FROM metadata.service_endpoints where id = @Id";

                // Use parameterized query to prevent SQL injection
                var result = dbConnection.QuerySingleOrDefault<List<Endpoint>>(sqlQuery);
                if (result == null)
                {
                    // Handle the case where no record is found (optional)
                    // For example, you might throw a custom exception or return a default value
                    throw new KeyNotFoundException($"Endpoints found");
                }
                return result;
            }
        }
    }
}
