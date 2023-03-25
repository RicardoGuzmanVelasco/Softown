namespace Softown.Runtime.Domain
{
    public class Architect
    {
        public Blueprint Design(ClassSummary classSummary)
        {
            return new Blueprint(classSummary.PublicMethods, classSummary.Properties);
        }
    }
}