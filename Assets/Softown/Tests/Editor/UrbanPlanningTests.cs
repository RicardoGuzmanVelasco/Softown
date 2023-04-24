using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Softown.Runtime.Domain;

namespace Softown.Tests.Editor
{
    public class UrbanPlanningTests
    {
        [Test]
        public void Obtain_BuildingsAmount()
        {
            new UrbanPlanning("Whatever", 20.Blueprints())
                .Buildings
                .Should().Be(20);

            new UrbanPlanning("Whatever", new District[] { new(20.Blueprints()) })
                .Buildings
                .Should().Be(20);
        }

        [Test]
        public void Obtain_BuildingsAmount_Of2Districts()
        {
            new UrbanPlanning("Whatever", new District[]
                {
                    new(20.Blueprints("FirstDistrict")),
                    new(20.Blueprints("SecondDistrict"))
                })
                .Buildings
                .Should().Be(40);
        }
    }

    public static class BlueprintExtensions
    {
        public static IEnumerable<Blueprint> Blueprints(this int howMany, string name = "Blueprint")
        {
            var blueprints = new List<Blueprint>();
            for(var i = 1; i <= howMany; i++)
                blueprints.Add(new($"{name}{i}", 1, 1));
            return blueprints;
        }
    }
}