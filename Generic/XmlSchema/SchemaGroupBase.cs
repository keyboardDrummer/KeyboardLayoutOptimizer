using System.Collections.Generic;
using System.Linq;

namespace Generic.XmlSchema
{
    public abstract class SchemaGroupBase : SchemaParticle
    {

        readonly IList<SchemaParticle> particles;

        protected SchemaGroupBase(params SchemaParticle[] particles)
        {
            this.particles = new List<SchemaParticle>(particles);
        }

        protected SchemaGroupBase()
        {
            particles = new List<SchemaParticle>();
        }

        public IList<SchemaParticle> Particles
        {
            get { return particles; }
        }
    }
}