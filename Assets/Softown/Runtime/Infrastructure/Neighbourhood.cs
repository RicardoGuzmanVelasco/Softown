using System;
using System.Threading.Tasks;
using Softown.Runtime.Domain;
using Softown.Runtime.Domain.Plotting;
using UnityEngine;

namespace Softown.Runtime.Infrastructure
{
    public abstract class Neighbourhood : MonoBehaviour
    {
        public abstract Task Raise(UrbanPlanning urbanPlanning, IProgress<float> progress = null);

        protected void SpawnGroundFor(Plot plot)
        {
            var ground = new GameObject("Ground");
            ground.AddComponent<Ground>().Raise(plot);
            ground.transform.SetParent(transform);
        }
    }
}