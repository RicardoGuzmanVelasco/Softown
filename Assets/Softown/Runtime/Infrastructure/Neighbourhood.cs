using Softown.Runtime.Domain;
using UnityEngine;

namespace Softown.Runtime.Infrastructure
{
    public class Neighbourhood : MonoBehaviour
    {
        public void Raise(UrbanPlanning urbanPlanning)
        {
            var lastX = 0;
            foreach (var blueprint in urbanPlanning)
            {
                var building = new GameObject("", typeof(Building)).GetComponent<Building>();
                building.transform.SetParent(transform);
                building.Raise(blueprint);
                
                building.transform.position += Vector3.right * lastX;
                lastX += building.FoundationsWidth;
            }
        }
    }
}