using System;
using System.Linq;
using System.Threading.Tasks;
using Softown.Runtime.Domain;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Softown.Runtime.Infrastructure
{
    public class AssemblyVisualizer : MonoBehaviour
    {
        enum NeighbourhoodStrategy { AllAssemblyClasses, AisledGlobalClasses }
        [SerializeField] NeighbourhoodStrategy strategy;
        
        enum AutoVisualize {None, CSharp, UnityEngine, UnityEditor, Softown, Testown }
        [SerializeField] AutoVisualize autoVisualize;

        TaskCompletionSource<bool> skipOrdered = new();
        
        Type Strategy => strategy switch
        {
            NeighbourhoodStrategy.AllAssemblyClasses => typeof(AllAssemblyClasses),
            NeighbourhoodStrategy.AisledGlobalClasses => typeof(AisledGlobalClasses),
            _ => throw new ArgumentOutOfRangeException()
        };
        
        Type AutoVisualizeType => autoVisualize switch
        {
            AutoVisualize.CSharp => typeof(string),
            AutoVisualize.UnityEngine => typeof(MonoBehaviour),
            AutoVisualize.UnityEditor => typeof(MenuItem),
            AutoVisualize.Softown => typeof(AssemblyVisualizer),
            AutoVisualize.Testown => typeof(NoNamespace),
            _ => throw new ArgumentOutOfRangeException()
        };

        void Awake()
        {
            if(autoVisualize != AutoVisualize.None)
                Visualize(new(AutoVisualizeType.Assembly));
        }

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

                var sut = new GameObject("", Strategy).GetComponent<Neighbourhood>();

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