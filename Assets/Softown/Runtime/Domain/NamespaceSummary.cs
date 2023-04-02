﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace Softown.Runtime.Domain
{
    public readonly struct NamespaceSummary : IEnumerable<ClassSummary>
    {
        public string Name { get; }
        IReadOnlyCollection<ClassSummary> ClassSummaries => Namespaces.SelectMany(n => n).ToList();
        public IReadOnlyCollection<NamespaceSummary> Namespaces { get; }
        public int Classes => ClassSummaries.Count;

        public NamespaceSummary(string name, IEnumerable<Type> candidates)
        {
            Assert.IsTrue(name is null || name.Any());
            
            Name = name;
            Namespaces = new List<NamespaceSummary>();
        }

        static bool IsEligible(string name, Type type)
        {
            //global:: case.
            if(type.Namespace is null)
                return name is null;

            return type.Namespace.StartsWith(name);
        }
        
        public IEnumerator<ClassSummary> GetEnumerator() => ClassSummaries.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public static string GlobalNamespace => null;
    }
}