using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Softown.Runtime.Domain
{
    internal readonly struct Namespace
    {
        readonly string qualifiedName;
        
        public Namespace(string qualifiedName)
        {
            this.qualifiedName = qualifiedName;
            
            Assert.IsTrue(qualifiedName == null || qualifiedName.Length > 0);
            //No tenemos soporte a que un subnamespace se llame igual que un namespace antecesor suyo.
            Assert.IsTrue(PartsOf(qualifiedName).Distinct().Count() == PartsOf(qualifiedName).Count());
        }
        
        static IEnumerable<string> PartsOf(string namespaceName)
        {
            return namespaceName == null
                ? Enumerable.Empty<string>()
                : namespaceName.Split('.');
        }
        
        public override string ToString() => qualifiedName;
    }
}