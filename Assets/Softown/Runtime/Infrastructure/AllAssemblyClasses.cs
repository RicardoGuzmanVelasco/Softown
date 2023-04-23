using System.Collections.Generic;
using System.Linq;
using Softown.Runtime.Domain;

namespace Softown.Runtime.Infrastructure
{
    public sealed class AllAssemblyClasses : Neighbourhood
    {
        protected override IEnumerable<Blueprint> Blueprints(UrbanPlanning urbanPlanning)
        {
            return urbanPlanning.SelectMany(b => b);
        }
    }
}