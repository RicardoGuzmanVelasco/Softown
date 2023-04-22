using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NUnit.Framework;

namespace Softown.Runtime.Domain
{
    public readonly struct District : IEnumerable<Blueprint>
    {
        readonly IReadOnlyCollection<Blueprint> blueprints;
        public int Buildings => blueprints.Count;

        public District([NotNull] IEnumerable<Blueprint> blueprints)
        {
            Assert.IsNotEmpty(blueprints);
            Assert.IsFalse(blueprints.Any(b => b.Equals(Blueprint.Blank)));
            
            this.blueprints = new List<Blueprint>(blueprints);
            
            Assert.IsTrue(Buildings > 0);
        }
        
        public IEnumerator<Blueprint> GetEnumerator() => blueprints.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}