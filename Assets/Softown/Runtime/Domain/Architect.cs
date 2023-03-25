using System.Linq;
using UnityEngine.Assertions;

namespace Softown.Runtime.Domain
{
    public class Architect
    {
        public Blueprint Design(ClassSummary classSummary)
        {
            Assert.IsFalse(classSummary.Equals(ClassSummary.Empty));
            
            return new(classSummary.PublicMethods, classSummary.Properties);
        }
        
        public UrbanPlanning Design(PackageSummary packageSummary)
        {
            Assert.IsFalse(packageSummary.Equals(PackageSummary.Empty));
            
            return new(packageSummary.Select(Design));
        }
    }
}