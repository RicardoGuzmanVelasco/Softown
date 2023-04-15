using System;
using System.Collections.Generic;
using System.Linq;

namespace Softown.Runtime.Domain
{
    public sealed record NamespaceSummary 
    {
        readonly Namespace qualified;
        readonly ClassSummary[] allClases;
        readonly NamespaceSummary[] directChildrenNamespaces;
        
        public IEnumerable<ClassSummary> AllChildrenClasses => allClases;
        public IEnumerable<ClassSummary> OnlyLeafClasses => allClases.Where(c => c.Namespace == Name);

        public string Name => qualified.ToString();

        public NamespaceSummary(string namespaceName, IEnumerable<Type> candidateTypes)
        {
            qualified = new(namespaceName);

            allClases = FittingCandidates(qualified, candidateTypes).ToArray();
            
            var childrenNamespaces = allClases
                .Select(c => c.fullNamespace)
                .Where(n => qualified.IsDirectParentOf(n))
                .Distinct()
                .Select(n => new NamespaceSummary(n.ToString(), candidateTypes))
                .ToArray();
            directChildrenNamespaces = childrenNamespaces;
        }

        public bool Equals(NamespaceSummary other) => other != null && Name == other.Name;
        public override int GetHashCode() => Name != null ? Name.GetHashCode() : 0;
        
        public override string ToString() => Name;
        
        static IEnumerable<ClassSummary> FittingCandidates(Namespace parent, IEnumerable<Type> candidateTypes)
        {
            var allCandidates = candidateTypes.Select(c => new ClassSummary(c));
            
            return parent.IsGlobal
                ? allCandidates
                : allCandidates.Where(c => parent.Contains(c.fullNamespace));
        }
    }
}