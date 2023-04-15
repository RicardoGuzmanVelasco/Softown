using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Softown.Runtime.Domain
{
    public readonly struct NamespaceSummary : IEnumerable<ClassSummary>
    {
        readonly IEnumerable<ClassSummary> classes;
        
        public string Name { get; }

        public NamespaceSummary(string thisNamespaceName, IEnumerable<Type> candidateTypes)
        {
            Name = thisNamespaceName;

            IEnumerable<Type> chosenCandidates;
            if(thisNamespaceName == GlobalNamespace)
                chosenCandidates = candidateTypes;
            else
                chosenCandidates = candidateTypes.Where(c => c.Namespace == thisNamespaceName);
            
            classes = chosenCandidates.Select(c => new ClassSummary(c));
        }

        public const string GlobalNamespace = null;
        public IEnumerator<ClassSummary> GetEnumerator()
        {
            return classes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool Equals(NamespaceSummary other) => Name == other.Name;
        public override bool Equals(object obj) => obj is NamespaceSummary other && Equals(other);
        public override int GetHashCode() => (Name != null ? Name.GetHashCode() : 0);
        
        public override string ToString() => Name;
    }
}