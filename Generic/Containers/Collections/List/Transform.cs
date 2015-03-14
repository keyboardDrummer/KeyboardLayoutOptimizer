using System;
using System.Collections.Generic;
using System.Linq;
using Generic.Containers.Collections.List.Final;

namespace Generic.Containers.Collections.List
{
    public class Transform<TA,TB> : DefaultReadOnlyList<TB>
    {
        private readonly IList<TA> core;
        private readonly Func<TA, TB> f;

        public Transform(IList<TA> core, Func<TA, TB> f)
        {
            this.core = core;
            this.f = f;
        }

        public override int Count
        {
            get { return core.Count; }
        }

        protected override TB Get(int index)
        {
            return f(core[index]);
        }

        public override IEnumerator<TB> GetEnumerator()
        {
            return core.Select(f).GetEnumerator();
        }
    }
}
