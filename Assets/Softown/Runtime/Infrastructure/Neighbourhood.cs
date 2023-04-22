using System.Linq;
using Softown.Runtime.Domain;
using Softown.Runtime.Domain.Plotting;
using UnityEngine;

namespace Softown.Runtime.Infrastructure
{
    public class Neighbourhood : MonoBehaviour
    {
        public void Raise(UrbanPlanning urbanPlanning)
        {
            name = urbanPlanning.Name;
            
            var foundations = urbanPlanning.Select(b => Foundation.SquareOf(b.FoundationsWidth));
            var plot = new Plot(new GreedySquareUp(), foundations.ToArray());
            
            var blueprints = urbanPlanning.ToList();

            foreach(var f in plot.Settlements)
            {
                var selected = blueprints.First(b => b.FoundationsWidth == f.Block.Size.x);
                blueprints.Remove(selected);

                var building = new GameObject(selected.BuildingName, typeof(Building)).GetComponent<Building>();
                building.transform.SetParent(transform);
                building.Raise(selected);

                building.transform.position += f.Center.To3DWithY(0);
            }

            SpawnGroundFor(plot);
        }

        void SpawnGroundFor(Plot plot)
        {
            var ground = new GameObject("Ground");
            ground.AddComponent<Ground>().Raise(plot);
            ground.transform.SetParent(transform);
        }
    }
}