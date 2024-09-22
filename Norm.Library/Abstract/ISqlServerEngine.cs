namespace Norm.Library.Abstract
{
    public interface ISqlServerEngine
    {
        List<T> Execute<T>() where T : class;
    }
}
