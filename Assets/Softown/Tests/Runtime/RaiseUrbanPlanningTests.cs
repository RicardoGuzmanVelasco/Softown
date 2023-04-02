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
                .ForEach(n => Object.DestroyImmediate(n.gameObject));
        }

        [Test]
        public void Raise_aNeighbourhood()
        {
            var sut = new GameObject("", typeof(Neighbourhood)).GetComponent<Neighbourhood>();
            var urbanPlanning = new Architect().Design(new AssemblySummary(typeof(TwoMethods).Assembly));

            sut.Raise(urbanPlanning);

            Object.FindObjectsOfType<Building>().Should().HaveCount(5);
        }

        [Test]
        public void Buildings_NeverHave_SameCenter()
        {
            var sut = new GameObject("", typeof(Neighbourhood)).GetComponent<Neighbourhood>();
            var urbanPlanning = new Architect().Design(new AssemblySummary(typeof(TwoMethods).Assembly));

            sut.Raise(urbanPlanning);

            Object.FindObjectsOfType<Building>()
                .Select(b => b.transform.position.XZ())
                .Should().OnlyHaveUniqueItems()
                .And.NotBeEmpty();
        }

        [Test]
        public void Space_BetweenBuildings_AreTheSame()
        {
            var sut = new GameObject("", typeof(Neighbourhood)).GetComponent<Neighbourhood>();
            var urbanPlanning = new Architect().Design(new AssemblySummary(typeof(TwoMethods).Assembly));

            sut.Raise(urbanPlanning);

            Object.FindObjectsOfType<Building>()
                .Select(b => b.transform.position.x)
                .Should().OnlyHaveUniqueItems()
                .And.NotBeEmpty();
        }
    }
}