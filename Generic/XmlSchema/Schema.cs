using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generic.XmlSchema
{
    public class Schema
    {
        readonly IList<SchemaType> types = new List<SchemaType>();
        readonly IList<SchemaElement> elements = new List<SchemaElement>();

        public SchemaElement AddElement(string name, SchemaType type)
        {
            var result = new SchemaElement(name, type);
            elements.Add(result);
            return result;
        }

        public void AddType(SchemaType type)
        {
            types.Add(type);
        }
    }
}
