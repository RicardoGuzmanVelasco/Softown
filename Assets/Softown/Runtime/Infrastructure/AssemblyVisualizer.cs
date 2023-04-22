using System.Collections;
using System.Linq;
using Softown.Runtime.Domain;
using UnityEngine;

namespace Softown.Runtime.Infrastructure
{
    public class AssemblyVisualizer : MonoBehaviour
    {
        bool skipOrdered = false;
        
        public void Visualize(AssemblySummary summary)
        {
            StartCoroutine(VisualizeSkippingOneAtATime(summary));
        }

        IEnumerator VisualizeSkippingOneAtATime(AssemblySummary summary)
        {
            for(var skipped = 0; skipped < summary.GlobalNamespace.AllChildrenClasses.Count(); skipped++)
            {
                var sut = new GameObject("", typeof(Neighbourhood)).GetComponent<Neighbourhood>();

                var urbanPlanning = new Architect().Design(summary, skipped);
                sut.Raise(urbanPlanning);

                yield return new WaitUntil(() => skipOrdered); //TODO: usar unitask y un tcs.
                skipOrdered = false;
                Object.Destroy(sut.gameObject);
            }
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                skipOrdered = true;
        }
    }
}