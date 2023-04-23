using System.Threading.Tasks;
using DG.Tweening;
using Softown.Runtime.Domain.Plotting;
using UnityEngine;

namespace Softown.Runtime.Infrastructure
{
    public class Ground : MonoBehaviour
    {
        public Task Raise(Plot plot)
        {
            var ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
            ground.transform.SetParent(transform);
            ground.GetComponent<MeshRenderer>().material.color = Color.green * Color.gray;
            transform.position += plot.Center.To3DWithY(-0.5f);

            var padding = new Vector3(1, 0, 1);
            var size = plot.Size.To3DWithY(0.1f) + padding;
            
            ground.transform.localScale = size;
            return ground.transform
                .DOScale(0, .33f)
                .From()
                .SetEase(Ease.OutBack)
                .AsyncWaitForCompletion();
        }
    }
}