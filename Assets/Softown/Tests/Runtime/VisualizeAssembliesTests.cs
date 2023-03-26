using System.Collections;
using NUnit.Framework;
using Softown.Runtime.Domain;
using Softown.Runtime.Infrastructure;
using UnityEngine;
using UnityEngine.TestTools;

namespace Softown.Tests.Runtime
{
    public class VisualizeAssembliesTests
    {
        [UnityTest, Ignore("Placeholder")]
        public IEnumerator CSharp_System_AsaWhole()
        {
            var sut = new GameObject("", typeof(Neighbourhood)).GetComponent<Neighbourhood>();
            var urbanPlanning = new Architect().Design(new PackageSummary(typeof(string).Assembly));

            sut.Raise(urbanPlanning);
            Debug.Break();
            yield return null;
        }
        
        [UnityTest, Ignore("Placeholder")]
        public IEnumerator UnityEngine()
        {
            var sut = new GameObject("", typeof(Neighbourhood)).GetComponent<Neighbourhood>();
            var urbanPlanning = new Architect().Design(new PackageSummary(typeof(ArrayList).Assembly));

            sut.Raise(urbanPlanning);
            Debug.Break();
            yield return null;
        }
    }
}