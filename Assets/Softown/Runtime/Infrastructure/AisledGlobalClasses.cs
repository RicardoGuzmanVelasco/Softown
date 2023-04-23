using System.Collections.Generic;
using System.Linq;
using Softown.Runtime.Domain;

namespace Softown.Runtime.Infrastructure
{
    public sealed class AisledGlobalClasses : Neighbourhood
    {
        protected override IEnumerable<Blueprint> Blueprints(UrbanPlanning urbanPlanning)
        {
            return urbanPlanning.Where(d => Namespace.RepresentsGlobal(d.Name)).SelectMany(b => b);
        }
    }
}