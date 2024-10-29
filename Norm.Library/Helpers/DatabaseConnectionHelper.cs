using System.Data.SqlClient;

namespace Norm.Library.Helpers
{
    public class DatabaseConnectionHelper : IDisposable, IAsyncDisposable
    {
        private readonly string _connectionString;
        private SqlConnection _connection;
        public DatabaseConnectionHelper(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new SqlConnection(_connectionString);

        }
        public async Task<SqlConnection> OpenConnectionAsync()
        {
            try
            {
                await _connection.OpenAsync();
                Console.WriteLine("Connection successfully");

                return _connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection error: {ex.Message}");
                throw;
            }
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
                Console.WriteLine($"Connection error: {ex.Message}");
                throw;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_connection != null && disposing)
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                    Console.WriteLine("Connection closed");
                }

                _connection.Dispose();
                _connection = null;
            }
        }
        public async ValueTask DisposeAsync()
        {
            if (_connection != null)
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    await _connection.CloseAsync();
                    Console.WriteLine("Connection closed asynchronously");
                }

                await _connection.DisposeAsync();
                _connection = null;
            }
            GC.SuppressFinalize(this);
        }
    }
}
