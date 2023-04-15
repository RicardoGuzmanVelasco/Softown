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

        internal enum CtorCase
        {
            None,
            Name_Match_Exactly_theCandidate,
            IgnoresCandidates_ThatAreIn_aRoot,
            Name_IsSubnamespace_OfCandidateNamespace,
            CandidateNamespace_IsSubnamespaceOf_Name,
            Name_IsInnerNamespace_OfCandidadteNamespace,
            Name_DesNotMatch_anyCandidate
        }

        public NamespaceSummary(string name, Type candidate)
        {
            Name = name;

            var candidates = new[] { candidate };
            var thisCase = ThisCase(name, candidates); 

            switch(thisCase)
            {
                case CtorCase.Name_Match_Exactly_theCandidate:
                    ClassLeafsChildrenOfThisNamespace = new List<ClassSummary> { new(candidates.First()) };
                    NamespacesChildrenOfThisNamespace = new List<NamespaceSummary>();
                    break;
                case CtorCase.IgnoresCandidates_ThatAreIn_aRoot:
                    ClassLeafsChildrenOfThisNamespace = new List<ClassSummary>();
                    NamespacesChildrenOfThisNamespace = new List<NamespaceSummary>();
                    break;
                case CtorCase.Name_IsSubnamespace_OfCandidateNamespace:
                    ClassLeafsChildrenOfThisNamespace = new List<ClassSummary> { new(candidates.First()) };
                    NamespacesChildrenOfThisNamespace = new List<NamespaceSummary>();
                    break;
                case CtorCase.CandidateNamespace_IsSubnamespaceOf_Name:
                    ClassLeafsChildrenOfThisNamespace = new List<ClassSummary>();
                    NamespacesChildrenOfThisNamespace = new List<NamespaceSummary>()
                        { new(candidates.Single().TrunkNamespaceRoot(), candidates) };
                    break;
                case CtorCase.Name_IsInnerNamespace_OfCandidadteNamespace:
                    ClassLeafsChildrenOfThisNamespace = new List<ClassSummary>();
                    NamespacesChildrenOfThisNamespace = new List<NamespaceSummary>
                        { new(candidates.First().Namespace, candidates.First()) };
                    break;
                case CtorCase.Name_DesNotMatch_anyCandidate:
                    ClassLeafsChildrenOfThisNamespace = new List<ClassSummary>();
                    NamespacesChildrenOfThisNamespace = new List<NamespaceSummary>();
                    break;
                case CtorCase.None:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        internal static CtorCase ThisCase(string name, Type[] candidates)
        {
            CtorCase thisCase;
            if(candidates.Single().Namespace == name)
                thisCase = CtorCase.Name_Match_Exactly_theCandidate;
            else if(candidates.Single().Namespace.IsRootOf(name))
                thisCase = CtorCase.IgnoresCandidates_ThatAreIn_aRoot;
            else if(name.IsSubnamespaceOf(candidates.Single().Namespace))
                thisCase = CtorCase.Name_IsSubnamespace_OfCandidateNamespace;
            else if(candidates.Single().IsInSubnamespaceOf(name))
                thisCase = CtorCase.CandidateNamespace_IsSubnamespaceOf_Name;
            else if(name.IsInnerNamespaceOf(candidates.Single().Namespace))
                thisCase = CtorCase.Name_IsInnerNamespace_OfCandidadteNamespace;
            else
                thisCase = CtorCase.Name_DesNotMatch_anyCandidate;
            return thisCase;
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