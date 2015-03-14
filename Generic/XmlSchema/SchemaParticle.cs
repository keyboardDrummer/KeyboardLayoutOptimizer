using System.Xml;

namespace Generic.XmlSchema
{
    public abstract class SchemaParticle
    {
        public const string NAMESPACE = "xs";
        public int? MaxOccurs { get; set; }
        public int MinOccurs { get; set;}

        public abstract void Write(XmlWriter writer);

        protected void WriterMinMax(XmlWriter writer)
        {
            writer.WriteAttributeString("minOccurs", MinOccurs.ToString());
            writer.WriteAttributeString("maxOccurs", MaxOccurs.HasValue ? MaxOccurs.ToString() : (-1).ToString());
        }
    }
}