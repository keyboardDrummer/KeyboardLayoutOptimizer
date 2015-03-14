using System.Xml;

namespace Generic.XmlSchema
{
    public class SchemaElement : SchemaParticle
    {
        string Name { get; set; }
        SchemaType Type { get; set; }

        public SchemaElement(string name, SchemaType type)
        {
            Name = name;
            Type = type;
        }

        public override void Write(XmlWriter writer)
        {
            writer.WriteStartElement("element", NAMESPACE);
            writer.WriteAttributeString("type", Type.Name);
            base.WriterMinMax(writer);
            writer.WriteEndElement();
        }
    }
}
