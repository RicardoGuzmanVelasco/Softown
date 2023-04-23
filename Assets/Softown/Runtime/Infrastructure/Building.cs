using Softown.Runtime.Domain;
using Softown.Runtime.Domain.Plotting;
using UnityEngine;
using UnityEngine.Assertions;
using static UnityEngine.PrimitiveType;

namespace Softown.Runtime.Infrastructure
{
    public class Building : MonoBehaviour
    {
        public static readonly Vector3 Ground = Vector3.one;

        public int Floors => (int)transform.localScale.y;

        public Foundation Foundation => transform.localScale.ToFoundation();

        public float WhereIsTheGround => transform.position.y;

        public Blueprint Blueprint { get; private set; }

        public void Raise(Blueprint blueprint)
        {
            GameObject.CreatePrimitive(Cube).transform.SetParent(transform);

            Blueprint = blueprint;

            transform.localScale =
                new Vector3(blueprint.FoundationsWidth, blueprint.Floors + Ground.y, blueprint.FoundationsWidth);
            transform.position += Vector3.up * (blueprint.Floors / 2f);

            gameObject.AddComponent<Rigidbody>().isKinematic = true;

            Assert.IsTrue(Foundation.Size.x > 0);
            Assert.AreEqual(transform.localScale.x, transform.localScale.z, "Ahora mismo solo cimientos cuadrados");
            Assert.IsTrue(Floors > 0);
        }

        void OnMouseEnter()
        {
            FindObjectOfType<Tooltip>().Hover(this);
        }
    }
}