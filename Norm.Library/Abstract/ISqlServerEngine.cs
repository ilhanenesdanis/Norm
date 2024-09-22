namespace Norm.Library.Abstract
{
    public interface ISqlServerEngine<T> where T : class
    {
        List<T> Execute<T>();
    }
}
