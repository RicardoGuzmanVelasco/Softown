using Softown.Runtime.Domain;
using UnityEngine;

namespace Softown.Runtime.Infrastructure
{
    public class Building : MonoBehaviour
    {
        public void Raise(int floors, int foundationsWidth)
        {
            transform.localScale = new Vector3(foundationsWidth, floors, foundationsWidth);
        }

        public void Raise(int floors)
        {
            Raise(floors, 1);
        }

        public void Raise(Blueprint blueprint)
        {
            Raise(blueprint.floors, blueprint.foundationsWidth);
        }
    }
}