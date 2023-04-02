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

            return new(assemblySummary.SkipLast(skipLast).Select(Design));
        }
    }
}