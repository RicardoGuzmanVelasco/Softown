using UnityEngine.Assertions;
using static Softown.Runtime.Domain.ClassSummary;

namespace Softown.Runtime.Domain
{
    public class Architect
    {
        public Blueprint Design(ClassSummary classSummary)
        {
            Assert.IsFalse(classSummary.Equals(Empty));
            
            return new(classSummary.PublicMethods, classSummary.Properties);
        }
    }
}