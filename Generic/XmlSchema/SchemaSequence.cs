using System.Xml;

namespace Generic.XmlSchema
{
    public class SchemaSequence : SchemaGroupBase
    {
        public SchemaSequence(params SchemaParticle[] particles)
            : base(particles)
        {
        }

        public override void Write(XmlWriter writer)
        {
            writer.WriteStartElement("sequence", NAMESPACE);
            foreach (var particles in Particles)
                particles.Write(writer);
            writer.WriteEndElement();
        }
    }
}