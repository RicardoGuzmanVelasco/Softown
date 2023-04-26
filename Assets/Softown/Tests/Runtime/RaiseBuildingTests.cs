using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Softown.Runtime.Domain;
using Softown.Runtime.Infrastructure;
using UnityEngine;
using static Softown.Tests.Runtime.TestApi;

namespace Softown.Tests.Runtime
{
    public class RaiseBuildingTests
    {
        Building sut;

        [TearDown]
        public void DestroyAllBuildings()
        {
            Object.FindObjectsOfType<Building>()
                .ToList()
                .ForEach(n => Object.Destroy(n.gameObject));
        }

        [SetUp]
        public void CreateFreshBuildingGameObject()
        {
            var building = new GameObject("", typeof(Building));
            sut = building.GetComponent<Building>();
        }

        [Test]
        public void Building_Cannot_HaveFoundations_WithScale0()
        {
            sut.Foundation.Size.x.Should().BePositive();
        }

        [Test]
        public async Task Building_Cannot_HaveScale0_ButBlankHas()
        {
            sut.Raise(blueprint: new(1, 1));
            sut.Floors.Should().BePositive();

            await EnoughForRaiseAnyBuilding;
            
            Blueprint.Blank.Floors.Should().Be(0);
        }

        [Test]
        public async Task Raise_ABuilding_With_2Floors()
        {
            sut.Raise(blueprint: new(2, 1));

            await EnoughForRaiseAnyBuilding;

            sut.Floors.Should().Be(2 + (int)Building.Ground.magnitude);
        }
        
        [Test]
        public async Task Raise_ABuilding_With_ADifferent_Foundation()
        {
            sut.Raise(blueprint: new(1, 2));
            
            await EnoughForRaiseAnyBuilding;

            sut.Foundation.Size.x.Should().Be(2 + (int)Building.Ground.magnitude);
        }

        [Test]
        public async Task Raise_ABuilding_From_FloorLevel()
        {
            sut.Raise(blueprint: new(4, 2));

            await EnoughForRaiseAnyBuilding;

            sut.WhereIsTheGround.Should().Be(4f / 2f);
        }
    }
}