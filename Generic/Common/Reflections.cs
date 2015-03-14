using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using Generic.InputOutput.Printing.Sizable;
using Generic.Containers.Collections.Dictionaries;
using System.Linq;

namespace Generic.Common
{
    public static class Reflections
    {
        public static T DeepCopy<T>(T t)
        {
            return DeepCopyHelper(t, new Dictionary<object, object>());
        }

        private static T DeepCopyHelper<T>(T obj, IDictionary<Object, Object> map)
        {
            var type = obj.GetType();
            var result = (T)FormatterServices.GetUninitializedObject(type);
            map.Add(obj, result);
            foreach (var fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                var value = fieldInfo.GetValue(obj);
                object newValue = null;
                if (value != null)
                {
                    var valueType = value.GetType();
                    var cloneable = value as ICloneable;
                    var array = value as Array;
                    if (value is string)
                        newValue = value;
                    else if (array != null)
                        newValue = array.Cast<object>().ToArray();
                    else if (cloneable != null)
                        newValue = value.Clone();
                    else if (valueType.IsClass)
                        newValue = map.Get(value).Default(() => DeepCopyHelper(value, map));
                    else
                        newValue = value;
                }
                fieldInfo.SetValue(result, newValue);
            }
            return result;
        }

        public static Document Print(Object t)
        {   
            var writer = new DocumentWriter();
            PrintHelper(t, writer, new HashSet<object>());
            return writer.ToDocument();
        }

        private static void PrintHelper(Object t, DocumentWriter writer, ISet<object> map)
        {
            if (map.Contains(t))
            {
                writer.Write("recurse");
                return;
            }

            map.Add(t);
            writer.Write(t.GetType().Name);
            writer.StartBlock(2);
            foreach (var fieldInfo in t.GetType().GetFields())
            {
                writer.Write(fieldInfo.Name + " = ");
                var value = fieldInfo.GetValue(t);
                if (value.GetType().IsByRef)
                    PrintHelper(value, writer, map);
                else
                    writer.Write(value.ToString());
                writer.WriteLine();
            }
            writer.EndBlock();
        }
    }
}