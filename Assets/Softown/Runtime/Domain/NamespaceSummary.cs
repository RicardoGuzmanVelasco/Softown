using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace Softown.Runtime.Domain
{
    public sealed record NamespaceSummary
    {
        readonly Namespace qualified;
        readonly ClassSummary[] allClases;
        readonly NamespaceSummary[] directChildrenNamespaces;
        
        public IEnumerable<ClassSummary> AllChildrenClasses => allClases;
        //public IEnumerable<ClassSummary> AllChildrenClasses => OnlyLeafClasses.Concat(directChildrenNamespaces.SelectMany(c => c.OnlyLeafClasses));
        public IEnumerable<ClassSummary> OnlyLeafClasses => allClases.Where(c => c.Namespace == Name);
        
        public IEnumerable<NamespaceSummary> DirectChildrenNamespaces => directChildrenNamespaces;
        public IEnumerable<NamespaceSummary> AllChildrenNamespaces => directChildrenNamespaces.Union(directChildrenNamespaces.SelectMany(c => c.AllChildrenNamespaces));

        public string Name => qualified.ToString();

        public NamespaceSummary(string namespaceName, IEnumerable<Type> candidateTypes)
        {
            qualified = new(namespaceName);

            allClases = FittingCandidates(qualified, candidateTypes).ToArray();

            directChildrenNamespaces = allClases
                .Select(c => c.fullNamespace)
                .Where(n => qualified.IsDirectParentOf(n))
                .Distinct()
                .Select(n => new NamespaceSummary(n.ToString(), candidateTypes))
                .ToArray();
            
            Assert.IsFalse(directChildrenNamespaces.Any(n => n.qualified.Equals(qualified)));
            Assert.IsTrue(directChildrenNamespaces.All(n => qualified.IsDirectParentOf(n.qualified)));
            Assert.IsTrue(AllChildrenNamespaces.All(n => qualified.IsAncestorOf(n.qualified)));
            Assert.IsTrue(AllChildrenClasses.All(c => qualified.Contains(c.fullNamespace)));
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