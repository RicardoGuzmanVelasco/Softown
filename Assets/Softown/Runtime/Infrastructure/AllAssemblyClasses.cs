using System.Linq;
using Softown.Runtime.Domain;
using Softown.Runtime.Domain.Plotting;
using UnityEngine;

namespace Softown.Runtime.Infrastructure
{
    public sealed class AllAssemblyClasses : Neighbourhood
    {
        public override void Raise(UrbanPlanning urbanPlanning)
        {
            name = urbanPlanning.Name;
            var neighbourhood = urbanPlanning.SelectMany(b => b);
            
            var foundations = neighbourhood.Select(b => Foundation.SquareOf(b.FoundationsWidth));
            var plot = new Plot(new GreedySquareUp(), foundations.ToArray());
            
            var blueprints = neighbourhood.ToList();

            foreach(var f in plot.Settlements)
            {
                var selected = neighbourhood.First(b => b.FoundationsWidth == f.Block.Size.x);
                blueprints.Remove(selected);

                var building = new GameObject(selected.BuildingName, typeof(Building)).GetComponent<Building>();
                building.transform.SetParent(transform);
                building.Raise(selected);

                building.transform.position += f.Center.To3DWithY(0);
            }

            SpawnGroundFor(plot);
        }
    }
    
    public sealed class AisledGlobalClasses : Neighbourhood
    {
        public override void Raise(UrbanPlanning urbanPlanning)
        {
            name = urbanPlanning.Name;
            var neighbourhood = urbanPlanning.First();
            
            var foundations = neighbourhood.Select(b => Foundation.SquareOf(b.FoundationsWidth));
            var plot = new Plot(new GreedySquareUp(), foundations.ToArray());
            
            var blueprints = neighbourhood.ToList();

            foreach(var f in plot.Settlements)
            {
                var selected = neighbourhood.First(b => b.FoundationsWidth == f.Block.Size.x);
                blueprints.Remove(selected);

                var building = new GameObject(selected.BuildingName, typeof(Building)).GetComponent<Building>();
                building.transform.SetParent(transform);
                building.Raise(selected);

                building.transform.position += f.Center.To3DWithY(0);
            }

            SpawnGroundFor(plot);
        }
    }
}