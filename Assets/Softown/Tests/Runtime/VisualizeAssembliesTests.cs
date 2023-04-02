using System.Collections;
using System.Reflection;
using NUnit.Framework;
using Softown.Runtime.Domain;
using Softown.Runtime.Infrastructure;
using UnityEngine;
using UnityEngine.TestTools;

namespace Softown.Tests.Runtime
{
    public class VisualizeAssembliesTests
    {
        [UnityTest]
        public IEnumerator CSharp_System_AsaWhole()
        {
            yield return Skip_aClass_EachUnpause(new AssemblySummary(typeof(string).Assembly));
        }

        [UnityTest]
        public IEnumerator UnityEngine()
        {
            yield return Skip_aClass_EachUnpause(new (typeof(MonoBehaviour).Assembly));
        }

        static IEnumerator Skip_aClass_EachUnpause(AssemblySummary assembly)
        {
            var sut = new GameObject("", typeof(Neighbourhood)).GetComponent<Neighbourhood>();

            var s = 0;
            while(s <= assembly.Classes)
            {
                var urbanPlanning = new Architect().Design(assembly, s++);
                sut.Raise(urbanPlanning);
                Debug.Break();
                yield return null;
                Object.Destroy(sut.gameObject);
                sut = new GameObject("", typeof(Neighbourhood)).GetComponent<Neighbourhood>();
            }
        }
    }
}