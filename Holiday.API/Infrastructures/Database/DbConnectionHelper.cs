using Microsoft.Data.SqlClient;
using System.Data;

namespace Holiday.API.Infrastructures.Database
{
    public class DbConnectionHelper
    {
        private readonly IConfiguration _configuration;
        public DbConnectionHelper(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public IDbConnection GetConnection => new SqlConnection(_configuration.GetConnectionString("connection1"));
    }
}
