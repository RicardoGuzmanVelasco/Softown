using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Softown.Runtime.Domain
{
    public readonly struct AssemblySummary : IEnumerable<ClassSummary>
    {
        public string Name { get; }
        [Description("Esto es temporal, luego irá todo colgando de GlobalNamespace")]
        public NamespaceSummary GlobalNamespace { get; }
        public IReadOnlyCollection<NamespaceSummary> NamespacesChildrenOfGlobal { get; }

        public int Classes => NamespacesChildrenOfGlobal.Sum(n => n.Count());
        public static AssemblySummary Empty => new();

        public AssemblySummary(Assembly assembly)
        {
            Name = assembly.GetName().Name;
            var types = assembly.GetTypes()
                .ExcludeUnityMonoScripts()
                .ExcludeNoSummarizableTypes();

            var onlyChildrenOfGlobal = assembly.AllNamespaces().OnlyChildrenOfGlobal();
            NamespacesChildrenOfGlobal = onlyChildrenOfGlobal
                .Select(r => new NamespaceSummary(r, types)).ToList();
            
            GlobalNamespace = new(NamespaceSummary.GlobalNamespace, types);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<ClassSummary> GetEnumerator()
        {
            return GlobalNamespace.Concat(NamespacesChildrenOfGlobal.SelectMany(n => n)).GetEnumerator();
        }
    }
}