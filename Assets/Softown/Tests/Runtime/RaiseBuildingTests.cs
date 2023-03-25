using FluentAssertions;
using NUnit.Framework;
using Softown.Runtime.Infrastructure;
using UnityEngine;

namespace Softown.Tests.Runtime
{
    public class RaiseBuildingTests
    {
        Building sut;

        [SetUp]
        public void Setup()
        {
            var building = new GameObject("", typeof(Building));
            sut = building.GetComponent<Building>();
        }

        [Test]
        public void Building_Cannot_HaveScale0()
        {
            sut.transform.localScale.y.Should().BePositive();
        }

        [Test]
        public void Raise_ABuilding_With_2Floors()
        {
            sut.Raise(blueprint: new(2, 1));

            sut.transform.localScale.y.Should().Be(2);
        }

        [Test]
        public void Building_Cannot_HaveFoundations_WithScale0()
        {
            sut.transform.localScale.x.Should().BePositive();
            sut.transform.localScale.z.Should().BePositive();
        }

        [Test]
        public void Raise_ABuilding_With_ADifferent_Foundation()
        {
            sut.Raise(blueprint: new(1, 2));

            sut.transform.localScale.x.Should().Be(2);
            sut.transform.localScale.z.Should().Be(2);
        }
    }
}