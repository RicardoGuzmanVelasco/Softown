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
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.SetParent(transform);
            cube.GetComponent<MeshRenderer>().material.color = Color.gray * Color.green;
            transform.position += plot.Center.To3DWithY(-0.5f);

            var padding = new Vector3(1, 0, 1);
            var size = plot.Size.To3DWithY(0.1f) + padding;
            
            cube.transform.localScale = size;
            return cube.transform
                .DOScale(0, .33f)
                .From()
                .SetEase(Ease.OutBack)
                .AsyncWaitForCompletion();
        }
    }
}