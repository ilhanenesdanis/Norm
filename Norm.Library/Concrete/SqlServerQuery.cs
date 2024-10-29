using Norm.Library.Abstract;
using Norm.Library.Core.Models;
using Norm.Library.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace Norm.Library.Concrete
{
    public class SqlServerQuery : ISqlServerQuery
    {
        public async Task<T> QueryAsync<T>(Query query, SqlConnectionModel sqlConnection) where T : class
        {
            QueryValidation(query);

            using (var dbHelper = new DatabaseConnectionHelper(sqlConnection.GetConnectionString()))
            {
                await using (SqlConnection connection = await dbHelper.OpenConnectionAsync())
                {
                    if (connection == null)
                        return default;

                    using (SqlCommand cmd = new SqlCommand($"select  {GetColumns(query.Column, query.Columns)}  from {query.TableName}", connection))
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        return QueryViewHelper.GetQueryView<T>(reader);
                }
            }
        }

        private string GetColumns(string? queryColumn, string[]? queryColumns)
        {
            if (string.IsNullOrWhiteSpace(queryColumn) && (queryColumns == null || !queryColumns.Any()))
                throw new ArgumentException("At least one column must be specified.");

            if (!string.IsNullOrWhiteSpace(queryColumn) && queryColumns != null && queryColumns.Any())
                throw new ArgumentException("Only one of 'queryColumn' or 'queryColumns' should be provided.");

            if (!string.IsNullOrWhiteSpace(queryColumn))
                return queryColumn;

            return string.Join(",", queryColumns);
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