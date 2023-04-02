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
        IReadOnlyCollection<ClassSummary> ClassLeafsChildrenOfThisNamespace { get; }
        public IReadOnlyCollection<NamespaceSummary> NamespacesChildrenOfThisNamespace { get; }
        public int Classes => ClassLeafsChildrenOfThisNamespace.Count;

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

            //case OneSubnamespace_withaClass_ButAlso_Directly_aChildClass
            if(candidates.Count() == 2)
            {
                NamespacesChildrenOfThisNamespace = new NamespaceSummary(name, candidates.Take(1)).NamespacesChildrenOfThisNamespace
                     .Concat(new NamespaceSummary(name, candidates.Skip(1)).NamespacesChildrenOfThisNamespace)
                     .ToList();
                ClassLeafsChildrenOfThisNamespace = new NamespaceSummary(name, candidates.Take(1)).ClassLeafsChildrenOfThisNamespace
                    .Concat(new NamespaceSummary(name, candidates.Skip(1)).ClassLeafsChildrenOfThisNamespace)
                    .ToList();
                return;
            }

            //case Name_DesNotMatch_anyCandidate
            ClassLeafsChildrenOfThisNamespace = new List<ClassSummary>();
            NamespacesChildrenOfThisNamespace = new List<NamespaceSummary>();
        }

        static bool IsEligible(string name, Type type)
        {
            //global:: case.
            if(type.Namespace is null)
                return name is null;

            return type.Namespace.StartsWith(name);
        }

        public IEnumerator<ClassSummary> GetEnumerator()
        {
            var classSummaries = ClassLeafsChildrenOfThisNamespace.Concat(NamespacesChildrenOfThisNamespace.SelectMany(n => n.ClassLeafsChildrenOfThisNamespace));
            return classSummaries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public static string GlobalNamespace => null;

        public override string ToString() => Name;
    }
}