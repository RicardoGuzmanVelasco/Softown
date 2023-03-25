using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Softown.Runtime.Domain
{
    public readonly struct PackageSummary : IEnumerable<ClassSummary>
    {
        public string Name { get; }
        IReadOnlyCollection<ClassSummary> ClassSummaries { get; } 
        public int Classes => ClassSummaries.Count;

        public PackageSummary(Assembly assembly)
        {
            Name = assembly.GetName().Name;
            ClassSummaries = new List<ClassSummary>(assembly.GetTypes()
                .Where(t => !t.Name.Contains("MonoScript"))
                .Select(t => new ClassSummary(t)));
        }
        
        public static object Empty => new();
        public IEnumerator<ClassSummary> GetEnumerator() => ClassSummaries.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}