using System;
using Generic;
using Generic.Cloneables;
using Generic.Common;

namespace KeyboardEPCS
{
    [Serializable]  
    public class ConvertedArray<Element> : IGenCloneable1<ConvertedArray<Element>,Element> 
    {
        readonly Element[] data;
        readonly Converter converter;

        public Converter Converter { get { return converter; } }

        public ConvertedArray()
        {
        }

        public ConvertedArray(int size) {converter = new Converter(size);}
        public ConvertedArray(Element[] data) { this.data = data; converter = new Converter(data.Length); }
        public ConvertedArray(Element[] data, Converter conv) { this.data = data; converter = conv; }

        public Element[] Data { get { return data; } }

        public Element this[int index]
        {
            get { return data[converter[index]]; }
            set { data[converter[index]] = value;}
        }

        public ConvertedArray<Element> Clone(Func<Element, Element> cloneElement)
        {
            var result = new ConvertedArray<Element>(General.ArrayCopy(data,cloneElement), Converter.Clone());
            return result;
        }
    }
}
