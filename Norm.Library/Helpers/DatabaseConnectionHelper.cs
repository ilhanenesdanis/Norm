using System.Data.SqlClient;

namespace Norm.Library.Helpers
{
    public class DatabaseConnectionHelper : IDisposable
    {
        private readonly string _connectionString;
        private SqlConnection _connection;
        public DatabaseConnectionHelper(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new SqlConnection(_connectionString);

        }
        public SqlConnection OpenConnection()
        {
            try
            {
                _connection.Open();
                Console.WriteLine("Connection successfully");

                return _connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }


        public void Dispose()
        {
            if (_connection != null)
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                    Console.WriteLine("Connection Closed");
                }

                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
