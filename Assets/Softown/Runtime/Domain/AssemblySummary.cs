using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Softown.Runtime.Domain
{
    public readonly struct AssemblySummary : IEnumerable<ClassSummary>
    {
        public string Name { get; }
        public IReadOnlyCollection<NamespaceSummary> RootNamespaces { get; }

        public int Classes => RootNamespaces.Sum(n => n.Classes);
        public static AssemblySummary Empty => new();

        public AssemblySummary(Assembly assembly)
        {
            Name = assembly.GetName().Name;
            var types = assembly.GetTypes()
                .ExcludeUnityMonoScripts()
                .ExcludeNoSummarizableTypes();

            var onlyRoots = assembly.AllNamespaces()
                .OnlyRoots();
            
            RootNamespaces = onlyRoots
                .Select(r => new NamespaceSummary(r, types)).ToList();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<ClassSummary> GetEnumerator()
        {
            return RootNamespaces.SelectMany(n => n).GetEnumerator();
        }
    }
}