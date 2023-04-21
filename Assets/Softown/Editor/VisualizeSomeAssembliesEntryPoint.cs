using System;
using System.Threading.Tasks;
using Softown.Runtime.Domain;
using Softown.Runtime.Infrastructure;
using UnityEngine;

namespace Softown.Editor
{
    public class VisualizeSomeAssembliesEntryPoint
    {
        [UnityEditor.MenuItem("Softown/Raise/C#")]
        static void RaiseCSharp()
        {
            var assembly = typeof(string).Assembly;
            Raise(assembly);
        }
        
        [UnityEditor.MenuItem("Softown/Raise/UnityEngine")]
        static void RaiseUnityEngine()
        {
            var assembly = typeof(UnityEngine.MonoBehaviour).Assembly;
            Raise(assembly);
        }
        
        [UnityEditor.MenuItem("Softown/Raise/UnityEditor")]
        static void RaiseUnityEditor()
        {
            var assembly = typeof(UnityEditor.Editor).Assembly;
            Raise(assembly);
        }
        
        [UnityEditor.MenuItem("Softown/Raise/Softown")]
        static void RaiseSoftown()
        {
            var assembly = typeof(AssemblySummary).Assembly;
            Raise(assembly);
        }
        
        static async void Raise(System.Reflection.Assembly assembly)
        {
            UnityEditor.EditorApplication.EnterPlaymode();
            await Task.Delay(TimeSpan.FromSeconds(.5f));
            
            var summary = new AssemblySummary(assembly);
            SpawnVisualizer(summary);
        }

        static void SpawnVisualizer(AssemblySummary summary)
        {
            new GameObject("", typeof(AssemblyVisualizer))
                .GetComponent<AssemblyVisualizer>()
                .Visualize(summary);
        }
    }
}