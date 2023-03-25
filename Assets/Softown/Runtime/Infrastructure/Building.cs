using NUnit.Framework;
using Softown.Runtime.Domain;
using UnityEngine;
using static UnityEngine.PrimitiveType;

namespace Softown.Runtime.Infrastructure
{
    public class Building : MonoBehaviour
    {
        public int Floors => (int)transform.localScale.y;
        public int FoundationsWidth => ((int)transform.localScale.x + (int)transform.localScale.z) / 2;
        public float WhereIsTheGround => transform.position.y;

        public void Raise(Blueprint blueprint)
        {
            GameObject.CreatePrimitive(Cube).transform.SetParent(transform);
            
            //Se suma la escala porque se considera que el suelo es un piso inicial.
            transform.localScale += new Vector3(blueprint.FoundationsWidth, blueprint.Floors, blueprint.FoundationsWidth);
            transform.position += Vector3.up * (blueprint.Floors / 2f);
            
            Assert.IsTrue(FoundationsWidth > 0);
            Assert.IsTrue(Floors > 0);
        }
    }
}