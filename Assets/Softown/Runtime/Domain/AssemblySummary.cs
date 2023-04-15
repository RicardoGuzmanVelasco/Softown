using System.Collections.Generic;
using System.Reflection;

namespace Softown.Runtime.Domain
{
    public readonly struct AssemblySummary
    {
        readonly NamespaceSummary globalNamespace;
        public string Name { get; }
        
        public IEnumerable<ClassSummary> AllContainedClasses => globalNamespace.AllChildrenClasses;

        public AssemblySummary(Assembly assembly)
        {
            Name = assembly.GetName().Name;
            var types = assembly.GetTypes()
                .ExcludeUnityMonoScripts()
                .ExcludeNoSummarizableTypes();
            
            globalNamespace = new(Namespace.Global, types);
        }
        
        public static AssemblySummary Empty => new();
    }
}