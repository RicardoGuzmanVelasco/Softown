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

            IEnumerable<Type> chosenCandidates;
            if(namespaceName == GlobalNamespace)
                chosenCandidates = candidateTypes;
            else
                chosenCandidates = candidateTypes.Where(c => c.Namespace == namespaceName);
            
            allClases = chosenCandidates.Select(c => new ClassSummary(c)).ToArray();
            
        }

        public const string GlobalNamespace = null;

        public bool Equals(NamespaceSummary other) => other != null && Name == other.Name;
        public override int GetHashCode() => Name != null ? Name.GetHashCode() : 0;
        
        public override string ToString() => Name;
    }
}