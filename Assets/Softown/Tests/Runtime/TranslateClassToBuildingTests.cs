using FluentAssertions;
using NUnit.Framework;
using Softown.Runtime.Domain;
using Softown.Runtime.Infrastructure;
using UnityEngine;

namespace Softown.Tests.Runtime
{
    public class TranslateClassToBuildingTests
    {
        [Test]
        public void Raise_ABuilding_From_AClass()
        {
            var classSummary = new ClassSummary(typeof(TwoMethods_AndOneProperty));
            var building = new GameObject("", typeof(Building)).GetComponent<Building>();

            var blueprint = new Architect().Design(classSummary);
            building.Raise(blueprint: blueprint);

            building.transform.localScale.Should().Be(new Vector3(1, 2, 1));
        }

        public class TwoMethods_AndOneProperty
        {
            public int Property_1 { get; set; }
            public void Method_1() { }
            public void Method_2() { }
        }
    }
}