using System;
using System.Collections.Generic;

namespace KeyboardEPCS
{
    [Serializable]
    public class Named<DataType, Child>
        where Child : Named<DataType, Child>, new()
    {
        String name;
        DataType value;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public DataType Value
        {
            get { return value; }
            set { this.value = value; }
        }

        static public Child Create(String name, DataType value)
        {
            var result = new Child();
            result.Name = name;
            result.Value = value;
            return result;
        }

        static public String[] GetNames(List<Child> input)
        {
            var result = new String[input.Count];
            var i = 0;
            foreach (var s in input)
            {
                result[i] = s.Name;
                i++;
            }
            return result;
        }
    }
}
