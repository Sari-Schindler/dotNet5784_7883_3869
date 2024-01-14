using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BO;

internal static class Tools
{
    /// <summary>
    /// override the function toString()
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>

    public static string ToStringProperty<T>(this T obj)
    {
        if (obj == null)
            return string.Empty;

        Type type = typeof(T);
        PropertyInfo[] properties = type.GetProperties();

        string result = $"{type.Name} properties:\n";

        if (obj is IEnumerable<T> enumerable)
        {
            bool isFirstItem = true;

            foreach (var item in enumerable)
            {
                string? itemString = item != null ? item.ToString() : "null";
                result += (isFirstItem ? "" : " ") + itemString;
                isFirstItem = false;
            }
        }
        else
        {
            foreach (PropertyInfo property in properties)
            {
                object? value = property.GetValue(obj);

                string? propertyValue = value != null ? value.ToString() : "null";
                result += $"{property.Name}: {propertyValue}\n";
            }
        }

        return result;
    }
}







