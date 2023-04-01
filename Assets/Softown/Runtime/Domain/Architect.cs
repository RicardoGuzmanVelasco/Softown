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

        public UrbanPlanning Design(PackageSummary packageSummary, int skipLast = 0)
        {
            Assert.IsFalse(packageSummary.Equals(PackageSummary.Empty));

            return new(packageSummary.SkipLast(skipLast).Select(Design));
        }
    }
}