using Softown.Runtime.Domain;
using Softown.Runtime.Domain.Plotting;
using UnityEngine;
using System.Linq;

namespace Softown.Runtime.Infrastructure
{
    public class Neighbourhood : MonoBehaviour
    {
        public void Raise(UrbanPlanning urbanPlanning)
        {
            var foundations = urbanPlanning.Select(b => Foundation.SquareOf(b.FoundationsWidth));
            var plot = new Plot(new GreedySquareUp(), foundations.ToArray());
            
            var blueprints = urbanPlanning.ToList();

            foreach(var f in plot.SettledFoundations)
            {
                var selected = blueprints.First(b => b.FoundationsWidth == f.Block.Size.x);
                blueprints.Remove(selected);

                var building = new GameObject(selected.BuildingName, typeof(Building)).GetComponent<Building>();
                building.transform.SetParent(transform);
                building.Raise(selected);

                building.transform.position += f.At.To3DWithY(0);
            }

            SpawnGroundFor(plot);
        }

        void SpawnGroundFor(Plot plot)
        {
            var ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
            ground.name = "Ground";
            ground.transform.SetParent(transform);
            ground.GetComponent<MeshRenderer>().material.color = Color.green * Color.gray;
            ground.transform.position += plot.Center.To3DWithY(-0.5f);
            ground.transform.localScale = plot.Center.To3DWithY(0.1f);
        }
    }
}