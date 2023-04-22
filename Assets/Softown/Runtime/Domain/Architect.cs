using System.Linq;
using UnityEngine.Assertions;

namespace Softown.Runtime.Domain
{
    public class Architect
    {
        public Blueprint Design(ClassSummary classSummary)
        {
            Assert.IsFalse(classSummary.Equals(ClassSummary.Empty));

            return new(classSummary.Name, classSummary.PublicMethods, classSummary.Properties);
        }

        public UrbanPlanning Design(AssemblySummary assemblySummary, int skipLast = 0)
        {
            Assert.IsFalse(assemblySummary.Equals(AssemblySummary.Empty));

            var classesByNamespaces = assemblySummary.GlobalNamespace.AllChildrenClasses
                .GroupBy(c => c.fullNamespace)
                .ToDictionary(g => g.Key, g => g.ToArray());
            var districts = classesByNamespaces.Select(kvp => new District(kvp.Key.ToString(), kvp.Value.Select(Design)));
            
            return new
            (
                assemblySummary.Name,
                districts
            );
        }
    }
}