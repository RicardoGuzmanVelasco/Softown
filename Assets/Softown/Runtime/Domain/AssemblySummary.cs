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

            RootNamespaces = assembly.AllNamespaces()
                .OnlyRoots()
                .Select(r => new NamespaceSummary(r, types)).ToList();

            //
            // RootNamespaces = new[]
            // {
            //     new NamespaceSummary(new List<ClassSummary>(assembly.GetTypes()
            //         .ExcludeUnityMonoScripts()
            //         .ExcludeNoSummarizableTypes()
            //         .Select(t => new ClassSummary(t))))
            // };
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