using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Softown.Runtime.Domain;
using Softown.Runtime.Infrastructure;
using Softown.Tests.TestAPI;
using UnityEngine;
using static Softown.Tests.Runtime.TestApi;

namespace Softown.Tests.Runtime
{
    public class TranslateClassToBuildingTests
    {
        [Test]
        public async Task Raise_ABuilding_From_AClass()
        {
            var classSummary = new ClassSummary(typeof(TwoMethods_AndOneProperty));
            var building = new GameObject("", typeof(Building)).GetComponent<Building>();

            var blueprint = new Architect().Design(classSummary);
            building.Raise(blueprint: blueprint);

            await EnoughForRaiseAnyBuilding;

            building.ConcreteDimensions.Should().Be(Building.Ground + new Vector3(1, 2, 1));
        }
    }
}