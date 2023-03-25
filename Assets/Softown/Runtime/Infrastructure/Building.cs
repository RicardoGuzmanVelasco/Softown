using Softown.Runtime.Domain;
using UnityEngine;

namespace Softown.Runtime.Infrastructure
{
    public class Building : MonoBehaviour
    {
        public int Floors => (int)transform.localScale.y;
        public int FoundationsWidth => ((int)transform.localScale.x + (int)transform.localScale.z) / 2;

        public void Raise(Blueprint blueprint)
        {
            transform.localScale = new(blueprint.FoundationsWidth, blueprint.Floors, blueprint.FoundationsWidth);
            transform.position += Vector3.up * (blueprint.Floors / 2f);
        }
    }
}