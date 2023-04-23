using System.Linq;
using System.Threading.Tasks;
using Softown.Runtime.Domain;
using UnityEngine;

namespace Softown.Runtime.Infrastructure
{
    public class AssemblyVisualizer : MonoBehaviour
    {
        TaskCompletionSource<bool> skipOrdered = new();
        
        public void Visualize(AssemblySummary summary)
        {
            VisualizeSkippingOneAtATime(summary);
        }

        async void VisualizeSkippingOneAtATime(AssemblySummary summary)
        {
            for(var skipped = 0; skipped < summary.GlobalNamespace.AllChildrenClasses.Count(); skipped++)
            {
                Debug.Log($"Starting to raise {summary.Name} skipping {skipped} classes");
                await Task.Yield();

                var sut = new GameObject("", typeof(AisledGlobalClasses)).GetComponent<Neighbourhood>();

                var urbanPlanning = new Architect().Design(summary, skipped);
                sut.Raise(urbanPlanning);

                await skipOrdered.Task;
                skipOrdered = new();
                Object.Destroy(sut.gameObject);
            }
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                skipOrdered.SetResult(true);
        }
    }
}