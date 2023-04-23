using System;
using System.Linq;
using System.Threading.Tasks;
using Softown.Runtime.Domain;
using Softown.Runtime.Domain.Plotting;
using UnityEngine;

namespace Softown.Runtime.Infrastructure
{
    public sealed class AllAssemblyClasses : Neighbourhood
    {
        public override async Task Raise(UrbanPlanning urbanPlanning, IProgress<float> progress = null)
        {
            name = urbanPlanning.Name;
            var neighbourhood = urbanPlanning.SelectMany(b => b);
            
            var foundations = neighbourhood.Select(b => Foundation.SquareOf(b.FoundationsWidth));
            var plot = new Plot(new GreedySquareUp(), foundations.ToArray());
            
            var blueprints = neighbourhood.ToList();

            var settlements = plot.Settlements.ToList();
            for(var i = 0; i < settlements.Count; i++)
            {
                await Task.Yield();
                var selected = neighbourhood.First(b => b.FoundationsWidth == settlements[i].Block.Size.x);
                blueprints.Remove(selected);

                var building = new GameObject(selected.BuildingName, typeof(Building)).GetComponent<Building>();
                building.transform.SetParent(transform);
                building.Raise(selected);

                building.transform.position += settlements[i].Center.To3DWithY(0);
                progress?.Report((float)i / settlements.Count);
            }

            SpawnGroundFor(plot);
        }
    }
}