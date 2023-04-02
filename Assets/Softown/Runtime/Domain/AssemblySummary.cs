using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Softown.Runtime.Domain
{
    public readonly struct AssemblySummary : IEnumerable<ClassSummary>
    {
        public string Name { get; }
        IReadOnlyCollection<NamespaceSummary> Namespaces { get; }
        
        public int Classes => Namespaces.Sum(n => n.Classes);
        public static AssemblySummary Empty => new();

        public AssemblySummary(Assembly assembly)
        {
            Name = assembly.GetName().Name;
            Namespaces = new []{new NamespaceSummary(new List<ClassSummary>(assembly.GetTypes()
                .ExcludeUnityMonoScripts()
                .ExcludeNoSummarizableTypes()
                .Select(t => new ClassSummary(t))))};
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<ClassSummary> GetEnumerator()
        {
            return Namespaces.SelectMany(n => n).GetEnumerator();
        }
    }
}