using System;
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
                .ExcludeUnityMonoScripts()
                .ExcludeNoSummarizableTypes()
                .Select(t => new ClassSummary(t)));
        }

        public static object Empty => new();
        public IEnumerator<ClassSummary> GetEnumerator() => ClassSummaries.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
    
    public static class TypeExtensions
    {
        public static IEnumerable<Type> ExcludeUnityMonoScripts(this IEnumerable<Type> types)
        {
            return types.Where(t => t.Namespace != "UnityEngine.MonoBehaviour");
        }
        
        public static IEnumerable<Type> ExcludeNoSummarizableTypes(this IEnumerable<Type> types)
        {
            //Aquí se pueden meter todas las reglas que se vayan descubriendo sobre la marcha.
            return types;
        }
    }
}