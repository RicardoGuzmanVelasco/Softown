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
            var arrange = new FoundationsArranger(inbetween: 1);
            var plot = arrange.LineUp(urbanPlanning.Select(b => Foundation.SquareOf(b.FoundationsWidth)));

            var blueprints = urbanPlanning.ToList();

            foreach(var f in plot.Foundations)
            {
                var selected = blueprints.First(b => b.FoundationsWidth == f.Value.Size.x);
                blueprints.Remove(selected);

                var building = new GameObject(selected.BuildingName, typeof(Building)).GetComponent<Building>();
                building.transform.SetParent(transform);
                building.Raise(selected);

                building.transform.position += new Vector3(f.Key.x, 0, f.Key.y);
            }

            SpawnGroundFor(plot);
        }

        void SpawnGroundFor(Plot plot)
        {
            var ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
            ground.name = "Ground";
            ground.transform.SetParent(transform);
            ground.GetComponent<MeshRenderer>().material.color = Color.green;
            ground.transform.position += new Vector3(plot.Center.x, -0.5f, plot.Center.y);
            ground.transform.localScale = new Vector3(plot.Size.x, 0.1f, plot.Size.y);
        }
    }
}