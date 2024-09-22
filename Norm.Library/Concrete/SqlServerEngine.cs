using Norm.Library.Abstract;

namespace Norm.Library.Concrete
{
    public class SqlServerEngine : ISqlServerEngine
    {
        public List<T> Execute<T>() where T : class
        {
            throw new NotImplementedException();
        }
    }
}
