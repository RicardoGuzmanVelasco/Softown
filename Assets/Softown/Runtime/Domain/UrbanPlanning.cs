using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Softown.Runtime.Domain
{
    public readonly struct UrbanPlanning : IEnumerable<Blueprint>
    {
        readonly IReadOnlyCollection<District> districts;
        public string Name { get; }

        public UrbanPlanning(string name, [NotNull] IEnumerable<Blueprint> blueprints)
        {
            districts = new[]{new District(blueprints)};
            Name = name;
        }
        
        public int Buildings => districts.First().Count();
        
        public IEnumerator<Blueprint> GetEnumerator() => districts.SelectMany(d => d).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}