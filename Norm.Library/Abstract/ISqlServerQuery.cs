using Norm.Library.Core.Models;

namespace Norm.Library.Abstract
{
    public interface ISqlServerQuery
    {
        Task<List<T>> QueryAsync<T>(Query query) where T : class;
    }
}
