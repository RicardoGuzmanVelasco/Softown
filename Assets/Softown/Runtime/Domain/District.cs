using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine.Assertions;

namespace Softown.Runtime.Domain
{
    public readonly struct District : IEnumerable<Blueprint>
    {
        readonly IReadOnlyCollection<Blueprint> blueprints;
        public string Name { get; }
        public int Buildings => blueprints.Count;

        public District([NotNull] IEnumerable<Blueprint> blueprints)
        {
            Assert.IsTrue(blueprints.Any());
            Assert.IsFalse(blueprints.Any(b => b.Equals(Blueprint.Blank)));

            this.blueprints = new List<Blueprint>(blueprints);
            this.Name = "";

            Assert.IsTrue(Buildings > 0);
        }
        
        public District(string name, [NotNull] IEnumerable<Blueprint> blueprints)
        {
            Assert.IsTrue(blueprints.Any());
            Assert.IsFalse(blueprints.Any(b => b.Equals(Blueprint.Blank)));

            this.blueprints = new List<Blueprint>(blueprints);
            this.Name = name;

            Assert.IsTrue(Buildings > 0);
        }

        public IEnumerator<Blueprint> GetEnumerator() => blueprints.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}