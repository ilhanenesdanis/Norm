using Norm.Library.Abstract;
using Norm.Library.Core.Models;
using Norm.Library.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace Norm.Library.Concrete
{
    public class SqlServerQuery : ISqlServerQuery
    {
        public async Task<List<T>> QueryAsync<T>(Query query, SqlConnectionModel sqlConnection) where T : class
        {
            QueryValidation(query);

            using (var dbHelper = new DatabaseConnectionHelper(sqlConnection.GetConnectionString()))
            {
                SqlConnection connection = dbHelper.OpenConnection();

                if (connection != null)
                {
                    using (SqlCommand cmd = new SqlCommand("select FirstName,LastName,Phone,Id  from Users", connection))
                    using(SqlDataReader reader=cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            var test = reader.GetFieldType(0);
                            await Console.Out.WriteLineAsync(reader.GetString(0));
                            await Console.Out.WriteLineAsync(reader.GetString(1));
                            await Console.Out.WriteLineAsync(reader.GetString(2));
                        }

                }

            }


            return new List<T>();

        }

        private static void QueryValidation(Query query)
        {
            var isValid = ModelValidationHelper.IsValid(query);
            if (!isValid.Item1)
            {
                var errorMessage = string.Join(", ", isValid.Item2.Select(x => x.ErrorMessage));
                throw new ValidationException($"Query validation failed: {errorMessage}");
            }
        }
    }
}
