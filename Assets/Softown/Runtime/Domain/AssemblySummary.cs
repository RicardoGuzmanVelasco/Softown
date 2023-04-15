using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Softown.Runtime.Domain
{
    public readonly struct AssemblySummary : IEnumerable<ClassSummary>
    {
        public string Name { get; }
        public NamespaceSummary GlobalNamespace { get; }

        public static AssemblySummary Empty => new();

        public AssemblySummary(Assembly assembly)
        {
            Name = assembly.GetName().Name;
            var types = assembly.GetTypes()
                .ExcludeUnityMonoScripts()
                .ExcludeNoSummarizableTypes();
            
            GlobalNamespace = new(NamespaceSummary.GlobalNamespace, types);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<ClassSummary> GetEnumerator()
        {
            return GlobalNamespace.GetEnumerator();
        }
    }
}