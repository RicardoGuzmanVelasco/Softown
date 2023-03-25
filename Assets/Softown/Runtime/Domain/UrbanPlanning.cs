using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NUnit.Framework;
using static Softown.Runtime.Domain.Blueprint;

namespace Softown.Runtime.Domain
{
    public readonly struct UrbanPlanning : IEnumerable<Blueprint>
    {
        readonly IReadOnlyCollection<Blueprint> blueprints;

        public UrbanPlanning([NotNull] IEnumerable<Blueprint> blueprints)
        {
            Assert.IsNotEmpty(blueprints);
            Assert.IsFalse(blueprints.Any(b => b.Equals(Blank)));
            
            this.blueprints = new List<Blueprint>(blueprints);
            Assert.IsTrue(Buildings > 0);
        }

        public int Buildings => blueprints.Count;
        
        public IEnumerator<Blueprint> GetEnumerator() => blueprints.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}