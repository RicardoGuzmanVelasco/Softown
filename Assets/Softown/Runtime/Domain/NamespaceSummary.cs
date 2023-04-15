using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace Softown.Runtime.Domain
{
    public readonly struct NamespaceSummary : IEnumerable<ClassSummary>
    {
        public NamespaceSummary(string thisNamespaceName, IEnumerable<Type> candidateTypes)
        {
            throw new NotImplementedException();
        }

        public const string GlobalNamespace = null;
        public string Name { get; }

        public IEnumerator<ClassSummary> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool Equals(NamespaceSummary other) => Name == other.Name;
        public override bool Equals(object obj) => obj is NamespaceSummary other && Equals(other);
        public override int GetHashCode() => (Name != null ? Name.GetHashCode() : 0);

        public override string ToString() => Name;
    }
}