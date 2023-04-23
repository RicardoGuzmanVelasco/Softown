using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace Softown.Runtime.Domain
{
    internal readonly struct Namespace
    {
        public const string Global = null;

        readonly string qualifiedName;
        
        public Namespace(string qualifiedName)
        {
            this.qualifiedName = qualifiedName;
            
            Assert.IsTrue(qualifiedName == null || qualifiedName.Length > 0);
            //No tenemos soporte a que un subnamespace se llame igual que un namespace antecesor suyo.
            Assert.IsTrue(PartsOf(qualifiedName).Distinct().Count() == PartsOf(qualifiedName).Count());
        }
        
        public bool IsGlobal => RepresentsGlobal(qualifiedName);
        public static bool RepresentsGlobal(string qualifiedName) => qualifiedName is Global or nameof(Global);

        public bool Contains(Namespace other)
        {
            return IsPartOf(other) || this.Equals(other);
        }
        
        public bool IsPartOf(Namespace other)
        {
            return IsDirectParentOf(other) || IsAncestorOf(other);
        }
        
        public bool IsDirectParentOf(Namespace other)
        {
            var parts = PartsOf(qualifiedName).ToArray();
            var otherParts = PartsOf(other.qualifiedName).ToArray();
            return parts.Length + 1 == otherParts.Length && parts.SequenceEqual(otherParts.Take(parts.Length));
        }
        
        public bool IsAncestorOf(Namespace other)
        {
            var parts = PartsOf(qualifiedName).ToArray();
            var otherParts = PartsOf(other.qualifiedName).ToArray();
            return parts.Length < otherParts.Length && parts.SequenceEqual(otherParts.Take(parts.Length));
        }

        static IEnumerable<string> PartsOf(string namespaceName)
        {
            return namespaceName == null
                ? Enumerable.Empty<string>()
                : namespaceName.Split('.');
        }
        
        public override string ToString() => qualifiedName ?? nameof(Global);
    }
}