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


        public NamespaceSummary(string name, Type candidate)
        {
            Name = name;
            
            var candidates = new[] { candidate };
            //case Name_Match_Exactly_theCandidate
            if(candidates.Single().Namespace == name)
            {
                ClassLeafsChildrenOfThisNamespace = new List<ClassSummary> { new(candidates.First()) };
                NamespacesChildrenOfThisNamespace = new List<NamespaceSummary>();
                return;
            }

            //case IgnoresCandidates_ThatAreIn_aRoot (así que equivalente a  Name_DesNotMatch_anyCandidate).
            if(candidates.Single().Namespace.IsRootOf(name))
            {
                ClassLeafsChildrenOfThisNamespace = new List<ClassSummary>();
                NamespacesChildrenOfThisNamespace = new List<NamespaceSummary>();
                return;
            }

            //case Name_IsRootOf_theCandidate
            if(name.IsSubnamespaceOf(candidates.Single().Namespace))
            {
                ClassLeafsChildrenOfThisNamespace = new List<ClassSummary> { new(candidates.First()) };
                NamespacesChildrenOfThisNamespace = new List<NamespaceSummary>();
                return;
            }

            //case Name_IsRootOf_theCandidate
            if(candidates.Single().IsInSubnamespaceOf(name))
            {
                ClassLeafsChildrenOfThisNamespace = new List<ClassSummary>();
                NamespacesChildrenOfThisNamespace = new List<NamespaceSummary>()
                    { new(candidates.Single().TrunkNamespaceRoot(), candidates) };
                return;
            }

            //case Name_IsInnerNamespace
            if(name.IsInnerNamespaceOf(candidates.Single().Namespace))
            {
                ClassLeafsChildrenOfThisNamespace = new List<ClassSummary>();
                NamespacesChildrenOfThisNamespace = new List<NamespaceSummary>()
                    { new(candidates.Single().Namespace.TrunkUntilDeleteSubnamespace(name), candidates) };
                return;
            }
            
            //case Name_DesNotMatch_anyCandidate
            ClassLeafsChildrenOfThisNamespace = new List<ClassSummary>();
            NamespacesChildrenOfThisNamespace = new List<NamespaceSummary>();
        }

        public NamespaceSummary(string name, IEnumerable<Type> candidates)
        {
            Assert.IsTrue(name is null || name.Any());

            Name = name;

            if(candidates.Count() == 1)
            {
                var ns = new NamespaceSummary(name, candidates.Single());
                NamespacesChildrenOfThisNamespace = ns.NamespacesChildrenOfThisNamespace;
                ClassLeafsChildrenOfThisNamespace = ns.ClassLeafsChildrenOfThisNamespace;
            }
            else
            {
                var x = candidates.Take(1);
                var xs = candidates.Skip(1);
                var xNamespace = new NamespaceSummary(name, x.Single());
                var xsNamespace = new NamespaceSummary(name, xs);
                NamespacesChildrenOfThisNamespace = xNamespace.NamespacesChildrenOfThisNamespace
                    .Union(xsNamespace.NamespacesChildrenOfThisNamespace)
                    .ToList();
                ClassLeafsChildrenOfThisNamespace = xNamespace.ClassLeafsChildrenOfThisNamespace
                    .Union(xsNamespace.ClassLeafsChildrenOfThisNamespace)
                    .ToList();
            }
        }

        public const string GlobalNamespace = null;

        public IEnumerator<ClassSummary> GetEnumerator()
        {
            var classSummaries =
                ClassLeafsChildrenOfThisNamespace.Concat(
                    NamespacesChildrenOfThisNamespace.SelectMany(n => n.ClassLeafsChildrenOfThisNamespace));
            return classSummaries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool Equals(NamespaceSummary other) => Name == other.Name;
        public override bool Equals(object obj) => obj is NamespaceSummary other && Equals(other);
        public override int GetHashCode() => (Name != null ? Name.GetHashCode() : 0);

        public override string ToString() => Name;
    }
}