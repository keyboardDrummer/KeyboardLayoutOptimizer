using System.Collections.Generic;

namespace Generic.XmlSchema
{
    public class ComplexType : SchemaType
    {
        readonly string name;
        public override string Name
        {
            get { return name; }
        }

        readonly IDictionary<string, XmlPrimitiveType> attributes = new Dictionary<string, XmlPrimitiveType>();

        public ComplexType(string name, SchemaGroupBase groupBase)
        {
            this.name = name;
            GroupBase = groupBase;
        }

        public SchemaGroupBase GroupBase { get; set; }
        public SchemaType BaseType { get; set; }

        public IDictionary<string, XmlPrimitiveType> Attributes
        {
            get { return attributes; }
        }
    }
}