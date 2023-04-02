using System;
using System.Collections.Generic;
using System.Linq;

namespace Softown.Runtime.Domain
{
    public static class TypeExtensions
    {
        public static IEnumerable<Type> ExcludeUnityMonoScripts(this IEnumerable<Type> types)
        {
            return types.Where(t => !t.Name.Contains("MonoScript"));
        }
        
        public static IEnumerable<Type> ExcludeNoSummarizableTypes(this IEnumerable<Type> types)
        {
            //Aquí se pueden meter todas las reglas que se vayan descubriendo sobre la marcha.
            return types;
        }
    }
}