using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Softown.Runtime.Domain;
using Softown.Runtime.Infrastructure;
using Softown.Tests.TestAPI;
using UnityEngine;

namespace Softown.Tests.Runtime
{
    public class RaiseUrbanPlanningTests
    {
        [TearDown]
        public void TearDown()
        {
            Object.FindObjectsOfType<Neighbourhood>()
                .ToList()
                .ForEach(n => Object.Destroy(n.gameObject));
        }

        [Test]
        public void Raise_aNeighbourhood()
        {
            var sut = new GameObject("", typeof(Neighbourhood)).GetComponent<Neighbourhood>();
            var urbanPlanning = new Architect().Design(new PackageSummary(typeof(TwoMethods).Assembly));

            sut.Raise(urbanPlanning);

            Object.FindObjectsOfType<Building>().Should().HaveCount(5);
        }
        
        [Test]
        public void Buildings_NeverHave_SameCenter()
        {
            var sut = new GameObject("", typeof(Neighbourhood)).GetComponent<Neighbourhood>();
            var urbanPlanning = new Architect().Design(new PackageSummary(typeof(TwoMethods).Assembly));

            Object.FindObjectsOfType<Building>()
                .Select(b => b.transform.position)
                .Should().OnlyHaveUniqueItems();
        }
    }
}