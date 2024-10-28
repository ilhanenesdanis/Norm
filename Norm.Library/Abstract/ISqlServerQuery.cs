using Norm.Library.Core.Models;

namespace Norm.Library.Abstract
{
    public interface ISqlServerQuery
    {
        Task<T> QueryAsync<T>(Query query, SqlConnectionModel sqlConnection) where T : class;
    }
}
