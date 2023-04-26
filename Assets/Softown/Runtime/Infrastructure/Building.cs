using System;
using DG.Tweening;
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

        Blueprint represented;

        public int Floors => represented.Equals(Blueprint.Blank)
            ? throw new NotSupportedException()
            : Math.Max(1, represented.Floors);

        public Foundation Foundation => transform.localScale.ToFoundation();

        public float WhereIsTheGround => transform.position.y;

        public Vector3 ConcreteDimensions => transform.GetChild(0).localScale;
        
        public void Raise(Blueprint blueprint)
        {
            var cube = GameObject.CreatePrimitive(Cube).transform;
                cube.SetParent(transform);
            cube.GetComponent<MeshRenderer>().material.color = Color.gray;

            represented = blueprint;

            cube.localScale = new(blueprint.FoundationsWidth, blueprint.Floors + Ground.y, blueprint.FoundationsWidth);

            cube.DOScaleY(0, .33f).From().SetEase(Ease.OutBack);
            transform.position += Vector3.up * (blueprint.Floors / 2f);

            gameObject.AddComponent<Rigidbody>().isKinematic = true;

            Assert.IsTrue(Foundation.Size.x > 0);
            Assert.AreEqual(transform.localScale.x, transform.localScale.z, "Ahora mismo solo cimientos cuadrados");
        }

        void OnMouseEnter()
        {
            FindObjectOfType<Tooltip>().Hover(this);
        }
    }
}