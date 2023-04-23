using TMPro;
using UnityEngine;

namespace Softown.Runtime.Infrastructure
{
    public class Tooltip : MonoBehaviour
    {
        public void Progress(float progress)
        {
            GetComponentInChildren<TMP_Text>().text = $"{progress:P0}";
        }
        
        public void Hover(Building theBuilding)
        {
            GetComponentInChildren<TMP_Text>().text = theBuilding.name;
        }
        
        public void Clean() => GetComponentInChildren<TMP_Text>().text = "";
    }
}