using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    static class Tools
    {
        public static string ToStringProperty<T>(this T t, string suffix = "")
        {
            string str = "";
            foreach (PropertyInfo item in t!.GetType().GetProperties())
            {

                var value = item.GetValue(t, null);
                if (value is string)
                    str += "\n" + suffix + $"{item.Name}: {item.GetValue(t, null)}";
                else
                {
                    if (value is IEnumerable)
                    {
                        str += $"\n{item.Name}: ";
                        foreach (var item2 in (IEnumerable)value)
                            str += item2.ToStringProperty("  ");
                    }
                    else
                        str += "\n" + suffix + $"{item.Name}: {item.GetValue(t, null)}";
                }
            }
            str += "\n";
            return str;
        }

    }
}
