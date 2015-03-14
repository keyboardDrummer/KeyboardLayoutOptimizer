namespace Generic.XmlSchema
{
    public class SimpleType : SchemaType
    {
        public SimpleType(XmlPrimitiveType type)
        {
            Type = type;
        }

        public XmlPrimitiveType Type { get; set; }

        public override string Name
        {
            get { return Type.ToString(); }
        }
    }
}