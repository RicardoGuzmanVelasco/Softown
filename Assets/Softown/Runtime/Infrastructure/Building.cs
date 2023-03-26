using NUnit.Framework;
using Softown.Runtime.Domain;
using Softown.Runtime.Domain.Plotting;
using UnityEngine;
using static UnityEngine.PrimitiveType;

namespace Softown.Runtime.Infrastructure
{
    public class Building : MonoBehaviour
    {
        public static readonly Vector3 Ground = Vector3.one;

        public int Floors => (int)transform.localScale.y;
        public Foundation Foundation => new((int)transform.localScale.x, (int)transform.localScale.z);
        public float WhereIsTheGround => transform.position.y;

        public void Raise(Blueprint blueprint)
        {
            GameObject.CreatePrimitive(Cube).transform.SetParent(transform);

            transform.localScale =
                Ground + new Vector3(blueprint.FoundationsWidth, blueprint.Floors, blueprint.FoundationsWidth);
            transform.position += Vector3.up * (blueprint.Floors / 2f);

            Assert.IsTrue(Foundation.X > 0);
            Assert.AreEqual(transform.localScale.x, transform.localScale.z);
            Assert.IsTrue(Floors > 0);
        }
    }
}