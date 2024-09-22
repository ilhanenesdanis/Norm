using System.Data.SqlClient;

namespace Norm.Library.Core.Models
{
    public sealed class SqlConnectionModel
    {
        public required string Server { get; set; }
        public required string Database { get; set; }
        public required string UserId { get; set; }
        public required string Password { get; set; }
        public bool IntegratedSecurity { get; set; }
        public string GetConnectionString()
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = Server,
                InitialCatalog = Database,
                UserID = UserId,
                Password = Password,
                IntegratedSecurity = IntegratedSecurity
            };

            return builder.ConnectionString;
        }

    }
}
