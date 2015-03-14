using System;
using System.Collections.Generic;
using Generic.Cloneables;

namespace KeyboardEPCS
{
    [Serializable]  
    public class Converter : IGenCloneable<Converter>
    {
        readonly List<Byte> data;

        public Converter() { data = new List<Byte>(); }
        public Converter(int size) { data = new List<Byte>(size); for (Byte i = 0; i < size; i++) { data.Add(i); } } 

        public Converter Clone()
        {
            var result = new Converter();
            foreach (Byte i in data)
            {
                result.data.Add(i);
            }
            return result;
        }

        public int Length { get { return data.Count; } }

        public void Remove(int i)
        {
            data.RemoveAt(this[i]);
        }

        public void Add(Byte i)
        {
            data.Add(i);
        }

        public int this[int index]
        {
            get 
            {
                int i = 0;
                for (; data[i] != index; i++)
                {
                }
                return i; 
            }
        }
    }
}
