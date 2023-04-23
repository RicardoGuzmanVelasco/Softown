using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Softown.Runtime.Domain
{
    public readonly struct UrbanPlanning : IEnumerable<District>
    {
        readonly IReadOnlyCollection<District> neighbourhoods;
        public string Name { get; }

        public UrbanPlanning(string name, [NotNull] IEnumerable<Blueprint> blueprints)
        : this(name, new[]{new District(blueprints)}) { }
        
        public UrbanPlanning(string name, [NotNull] IEnumerable<District> districts)
        {
            this.neighbourhoods = new List<District>(districts);
            Name = name;
        }

        public int Buildings => neighbourhoods.Sum(d => d.Buildings);
        
        public IEnumerator<District> GetEnumerator() => neighbourhoods.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}