using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace Softown.Runtime.Domain
{
    public readonly struct NamespaceSummary : IEnumerable<ClassSummary>
    {
        public string Name { get; }
        public IReadOnlyCollection<ClassSummary> ClassLeafsChildrenOfThisNamespace { get; }
        public IReadOnlyCollection<NamespaceSummary> NamespacesChildrenOfThisNamespace { get; }

        public NamespaceSummary(string name, IEnumerable<Type> candidates)
        {
            Assert.IsTrue(name is null || name.Any());

            Name = name;

            //case Name_Match_Exactly_theCandidate
            if(candidates.Count() == 1)
                if(candidates.Single().Namespace == name)
                {
                    ClassLeafsChildrenOfThisNamespace = new List<ClassSummary> { new(candidates.First()) };
                    NamespacesChildrenOfThisNamespace = new List<NamespaceSummary>();
                    return;
                }
            
            //case IgnoresCandidates_ThatAreIn_aRoot (así que equivalente a  Name_DesNotMatch_anyCandidate).
            if(candidates.Count() == 1)
                if(candidates.Single().Namespace.IsRootOf(name))
                {
                    ClassLeafsChildrenOfThisNamespace = new List<ClassSummary>();
                    NamespacesChildrenOfThisNamespace = new List<NamespaceSummary>();
                    return;
                }

            //case Name_IsRootOf_theCandidate
            if(candidates.Count() == 1)
                if(name.IsSubnamespaceOf(candidates.Single().Namespace))
                {
                    ClassLeafsChildrenOfThisNamespace = new List<ClassSummary> { new(candidates.First()) };
                    NamespacesChildrenOfThisNamespace = new List<NamespaceSummary>();
                    return;
                }

            //case Name_IsRootOf_theCandidate
            if(candidates.Count() == 1)
                if(candidates.Single().IsInSubnamespaceOf(name))
                {
                    ClassLeafsChildrenOfThisNamespace = new List<ClassSummary>();
                    NamespacesChildrenOfThisNamespace = new List<NamespaceSummary>()
                        { new(candidates.Single().TrunkNamespaceRoot(), candidates) };
                    return;
                }
            
            //case Name_IsInnerNamespace
            if(candidates.Count() == 1)
                if(name.IsInnerNamespaceOf(candidates.Single().Namespace))
                {
                    ClassLeafsChildrenOfThisNamespace = new List<ClassSummary>();
                    NamespacesChildrenOfThisNamespace = new List<NamespaceSummary>()
                        { new(candidates.Single().Namespace.TrunkUntilDeleteSubnamespace(name), candidates) };
                    return;
                }

            //case multiple candidates
            if(candidates.Count() > 1)
            {
                var x = candidates.Take(1);
                var xs = candidates.Skip(1);
                var xNamespace = new NamespaceSummary(name, x);
                var xsNamespace = new NamespaceSummary(name, xs);
                
                NamespacesChildrenOfThisNamespace = xNamespace.NamespacesChildrenOfThisNamespace
                     .Union(xsNamespace.NamespacesChildrenOfThisNamespace)
                     .ToList();
                ClassLeafsChildrenOfThisNamespace = xNamespace.ClassLeafsChildrenOfThisNamespace
                    .Union(xsNamespace.ClassLeafsChildrenOfThisNamespace)
                    .ToList();
                return;
            }

            //case Name_DesNotMatch_anyCandidate
            ClassLeafsChildrenOfThisNamespace = new List<ClassSummary>();
            NamespacesChildrenOfThisNamespace = new List<NamespaceSummary>();
        }
        
        public const string GlobalNamespace = null;

        public IEnumerator<ClassSummary> GetEnumerator()
        {
            var classSummaries = ClassLeafsChildrenOfThisNamespace.Concat(NamespacesChildrenOfThisNamespace.SelectMany(n => n.ClassLeafsChildrenOfThisNamespace));
            return classSummaries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public bool Equals(NamespaceSummary other) => Name == other.Name;
        public override bool Equals(object obj) => obj is NamespaceSummary other && Equals(other);
        public override int GetHashCode() => (Name != null ? Name.GetHashCode() : 0);

        public override string ToString() => Name;
    }
}