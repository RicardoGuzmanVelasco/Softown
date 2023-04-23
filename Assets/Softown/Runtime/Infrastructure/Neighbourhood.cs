using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Softown.Runtime.Domain;
using Softown.Runtime.Domain.Plotting;
using UnityEngine;

namespace Softown.Runtime.Infrastructure
{
    public abstract class Neighbourhood : MonoBehaviour
    {
        protected abstract IEnumerable<Blueprint> Blueprints(UrbanPlanning urbanPlanning);
        
        public async Task Raise(UrbanPlanning urbanPlanning, IProgress<float> progress = null)
        {
            name = urbanPlanning.Name;
            var neighbourhood = Blueprints(urbanPlanning);
            
            var foundations = neighbourhood.Select(b => Foundation.SquareOf(b.FoundationsWidth));
            var plot = new Plot(new GreedySquareUp(), foundations.ToArray());
            await SpawnGroundFor(plot);

            var settlements = plot.Settlements.ToList();
            for(var i = 0; i < settlements.Count; i++)
            {
                await Task.Yield();
                destroyCancellationToken.ThrowIfCancellationRequested();
                
                var selected = neighbourhood.First(b => b.FoundationsWidth == settlements[i].Block.Size.x);

                var building = new GameObject(selected.BuildingName, typeof(Building)).GetComponent<Building>();
                building.transform.SetParent(transform);
                building.Raise(selected);

                building.transform.position += settlements[i].Center.To3DWithY(0);
                progress?.Report((float)i / settlements.Count);
            }
        }

        async Task SpawnGroundFor(Plot plot)
        {
            var ground = new GameObject("Ground");
            await ground.AddComponent<Ground>().Raise(plot);
            ground.transform.SetParent(transform);
        }
    }
}