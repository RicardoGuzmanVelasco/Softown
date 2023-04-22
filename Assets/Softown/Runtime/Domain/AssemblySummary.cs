using System.Reflection;

namespace Softown.Runtime.Domain
{
    public readonly struct AssemblySummary
    {
        public string Name { get; }
        
        public NamespaceSummary GlobalNamespace { get; }

        public AssemblySummary(Assembly assembly)
        {
            Name = assembly.GetName().Name;
            var types = assembly.GetTypes()
                .ExcludeUnityMonoScripts()
                .ExcludeNoSummarizableTypes();
            
            GlobalNamespace = new(Namespace.Global, types);
        }
        
        public static AssemblySummary Empty => new();
    }
}