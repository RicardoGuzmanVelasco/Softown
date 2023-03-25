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
            var @class = new ClassSummary(typeof(TwoMethods_AndOneProperty));
            var building = new GameObject("", typeof(Building)).GetComponent<Building>();
            var architect = new Architect();
            
            var blueprint = architect.Design(@class);
            building.Raise(blueprint);

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