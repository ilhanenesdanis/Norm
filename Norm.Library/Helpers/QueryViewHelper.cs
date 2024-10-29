using System.Collections;
using System.Data.SqlClient;

namespace Norm.Library.Helpers;

public static class QueryViewHelper
{
    public static T GetQueryView<T>(SqlDataReader reader)
    {
        object instance = null;
        IList list = null;
        CreateInstance<T>(ref instance, ref list);

        while (reader.Read())
        {
            object itemInstance = Activator.CreateInstance(typeof(T).IsGenericType ? typeof(T).GetGenericArguments()[0] : typeof(T));
            var properties = itemInstance.GetType().GetProperties();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                string columnName = reader.GetName(i);
                var property = properties.FirstOrDefault(p => p.Name == columnName);
                if (property != null)
                {
                    object value = reader.IsDBNull(i) ? null : reader.GetValue(i);
                    property.SetValue(itemInstance, value);
                }
            }
            if (list != null)
                list.Add(itemInstance);
            else
            {
                instance = itemInstance;
                break;
            }
        }
        return (T)(list ?? instance);
    }

    private static void CreateInstance<T>(ref object instance, ref IList list)
    {
        if (typeof(T).GetInterface(nameof(IEnumerable)) != null && typeof(T) != typeof(string))
            list = (IList)Activator.CreateInstance(typeof(T));
        else
            instance = Activator.CreateInstance(typeof(T));
    }
}