using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Softown.Runtime.Infrastructure;
using UnityEngine;

namespace Softown.Tests.Runtime
{
    public class RaiseBuildingTests
    {
        Building sut;

        [TearDown]
        public void TearDown()
        {
            Object.FindObjectsOfType<Building>()
                .ToList()
                .ForEach(n => Object.Destroy(n.gameObject));
        }

        [SetUp]
        public void Setup()
        {
            var building = new GameObject("", typeof(Building));
            sut = building.GetComponent<Building>();
        }

        [Test]
        public void Building_Cannot_HaveScale0()
        {
            sut.Floors.Should().BePositive();
        }

        [Test]
        public void Raise_ABuilding_With_2Floors()
        {
            sut.Raise(blueprint: new(2, 1));

            sut.Floors.Should().Be(2);
        }

        [Test]
        public void Building_Cannot_HaveFoundations_WithScale0()
        {
            sut.FoundationsWidth.Should().BePositive();
        }

        [Test]
        public void Raise_ABuilding_With_ADifferent_Foundation()
        {
            sut.Raise(blueprint: new(1, 2));

            sut.FoundationsWidth.Should().Be(2);
        }

        [Test]
        public void Raise_ABuilding_From_FloorLevel()
        {
            sut.Raise(blueprint: new(4, 2));

            sut.WhereIsTheGround.Should().Be(4f / 2f);
        }
    }
}