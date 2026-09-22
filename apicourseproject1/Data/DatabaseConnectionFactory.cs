using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace apicourseproject1.Data
{
    public class DatabaseConnectionFactory
    {
        private readonly DbSettings _dbSettings;

        public DatabaseConnectionFactory(IOptions<DbSettings> dbSettings)
        {
            _dbSettings = dbSettings.Value;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_dbSettings.DefaultConnection);
        }
    }
}