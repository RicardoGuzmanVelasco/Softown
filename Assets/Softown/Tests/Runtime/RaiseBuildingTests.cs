using Softown.Runtime.Infrastructure;

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

            sut.Floors.Should().Be(2 + (int)Building.Ground.magnitude);
        }

        [Test]
        public void Building_Cannot_HaveFoundations_WithScale0()
        {
            sut.Foundation.X.Should().BePositive();
        }

        [Test]
        public void Raise_ABuilding_With_ADifferent_Foundation()
        {
            sut.Raise(blueprint: new(1, 2));

            sut.Foundation.X.Should().Be(2 + (int)Building.Ground.magnitude);
        }

        [Test]
        public void Raise_ABuilding_From_FloorLevel()
        {
            sut.Raise(blueprint: new(4, 2));

            sut.WhereIsTheGround.Should().Be(4f / 2f);
        }
    }
}