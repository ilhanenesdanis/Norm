using Norm.Library.Abstract;
using Norm.Library.Core.Models;
using Norm.Library.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Norm.Library.Concrete
{
    public class SqlServerQuery : ISqlServerQuery
    {
        public async Task<List<T>> QueryAsync<T>(Query query) where T : class
        {
            QueryValidation(query);



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
