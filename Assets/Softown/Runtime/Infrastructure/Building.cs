using Softown.Runtime.Domain;
using UnityEngine;

namespace Softown.Runtime.Infrastructure
{
    public class Building : MonoBehaviour
    {
        public void Raise(Blueprint blueprint)
        {
            transform.localScale = new(blueprint.FoundationsWidth, blueprint.Floors, blueprint.FoundationsWidth);
        }
    }
}