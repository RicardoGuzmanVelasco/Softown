using Softown.Runtime.Domain.Plotting;
using UnityEngine;

namespace Softown.Runtime.Infrastructure
{
    public class Ground : MonoBehaviour
    {
        public void Raise(Plot plot)
        {
            var ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
            ground.transform.SetParent(transform);
            ground.GetComponent<MeshRenderer>().material.color = Color.green * Color.gray;
            ground.transform.position += plot.Center.To3DWithY(-0.5f);
            
            var size = plot.Size.To3DWithY(0.1f) + new Vector3(1, .1f, 1);
            ground.transform.localScale = size;
        }
    }
}