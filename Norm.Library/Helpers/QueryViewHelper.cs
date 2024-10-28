using System.Data.SqlClient;

namespace Norm.Library.Helpers;

public static class QueryViewHelper
{
    public static T GetQueryView<T>(SqlDataReader reader)
    {
        if (!reader.Read())
            throw new Exception("No query view found");

        T instance = (T)Activator.CreateInstance(typeof(T));
        var properties = instance.GetType().GetProperties();

        while (reader.Read())
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                string columnName = reader.GetName(i);
                var property = properties.FirstOrDefault(p => p.Name == columnName);
                if (property != null)
                {
                    object value = reader.IsDBNull(i) ? null : reader.GetValue(i);
                    property.SetValue(instance, value);
                }
            }
        }
        
        return instance;
    }
}